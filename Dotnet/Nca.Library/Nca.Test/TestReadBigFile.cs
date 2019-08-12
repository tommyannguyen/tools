using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Nca.Test
{
    public class TestReadBigFile
    {
        [Test]
        public void GenerateBigFileJsonStart()
        {
            var start = "[";
            var end = "]";
            var oranges = "{\"oranges\": [{\"field1\": \"value1\",\"field2\": \"value2\"},{\"field1\": \"value3\",\"field2\": \"value4\"}]},";
            var bananas = "{\"bananas\": [{\"field1\": \"value1\",\"field2\": \"value2\"},{\"field1\": \"value3\",\"field2\": \"value4\"}]},";
            using (StreamWriter outfile = new StreamWriter("D:\\big.json"))
            {
                outfile.Write(start);
                for(var i = 0; i< 1000000000; i++)
                {
                    outfile.Write(oranges);
                    outfile.Write(bananas);
                }
            }
            GenerateBigFileJsonEnd();
        }
        [Test]
        public void GenerateBigFileJsonEnd()
        {
            var end = "]";
            var bananas = "{\"bananas\": [{\"field1\": \"value1\",\"field2\": \"value2\"},{\"field1\": \"value3\",\"field2\": \"value4\"}]}";
            using (StreamWriter outfile = new StreamWriter("D:\\big.json",true))
            {
                outfile.Write(bananas);
                outfile.Write(end);
            }
        }

        static int number = 0;
        [Test]
        public void TestReadFile()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Debug.WriteLine($"Start reading");
            var dinamic = new DinamicStreamJsonParser()
            {
                PropertyToType = new Dictionary<string, Type>()
                {
                    { "oranges", Type.GetType(Type.GetType("Nca.Test.Orange").AssemblyQualifiedName)},
                    { "bananas", Type.GetType(Type.GetType("Nca.Test.Banana").AssemblyQualifiedName)},
                },
                ObjectFoundCallback = (object obj) => {
                    Debug.WriteLine(number++);
                },
                NewTypeFoundCallback = (object obj, string name) => {
                    Debug.WriteLine($"Found object {obj} with name {name}");
                },
                StreamReader = new StreamReader("D:\\big.json"),
            };
            dinamic.Parse();

            watch.Stop();
            var elapsedMs = TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds);
            Debug.WriteLine(elapsedMs.ToString());
        }

    }
    public class Orange
    {
        public string field1 { get; set; }
        public string field2 { get; set; }
    }

    public class Banana
    {
        public string field3 { get; set; }
        public string field4 { get; set; }
    }

    public class DinamicStreamJsonParser
    {
        public delegate void ObjectFound(object obj);
        public delegate void ObjectFoundWithName(object obj, string name);

        public StreamReader StreamReader;
        public Dictionary<string, Type> PropertyToType;
        public ObjectFound ObjectFoundCallback;
        public ObjectFoundWithName NewTypeFoundCallback = null;

        public void Parse()
        {
            JsonTextReader reader = new JsonTextReader(StreamReader);
            reader.SupportMultipleContent = true;

            var serializer = new JsonSerializer();

            string currentFound = "";
            string oldFound = "";
            int startObject = 0;

            while (reader.Read())
            {

                if (reader.TokenType == JsonToken.StartObject)
                {
                    startObject++;
                }
                if (reader.TokenType == JsonToken.EndObject)
                {
                    startObject--;
                }
                if (startObject == 1)
                {
                    if (reader.TokenType == JsonToken.PropertyName &&
                        PropertyToType.ContainsKey((string)reader.Value)) currentFound = (string)reader.Value;
                }

                if (startObject == 2 && reader.TokenType == JsonToken.StartObject)
                {

                    if (PropertyToType.ContainsKey(currentFound))
                    {

                        var type = PropertyToType[currentFound];

                        var o = typeof(DinamicStreamJsonParser)
                            .GetMethod("Deserialize")
                            .MakeGenericMethod(type)
                            .Invoke(this, new object[] { reader, serializer });

                        if (NewTypeFoundCallback != null && oldFound != currentFound)
                            NewTypeFoundCallback(o, currentFound);

                        ObjectFoundCallback(o);

                        oldFound = currentFound;

                    }

                    startObject--;
                }

            }

        }

        public T Deserialize<T>(JsonTextReader reader, JsonSerializer serializer)
        {
            return serializer.Deserialize<T>(reader);
        }

    }
}
