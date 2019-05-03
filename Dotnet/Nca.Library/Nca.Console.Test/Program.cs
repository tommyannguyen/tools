using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Nca.Console.Test
{
    public class Link
    {
       public IEnumerable<SubLink> subLinks { get; set; }
    }
    public class SubLink
    {
        public Uri url { get; set; }
    }
    public class Item
    {
        public string Name { get; set; }
        public IEnumerable<Link> Links { get; set; } = new List<Link>();
    }
    class Program
    {
        static void Main(string[] args)
        {
            var xx = GetItems();
            foreach(var item in xx)
            {
                foreach(var link in item.Links)
                {
                    foreach (var x in link.subLinks)
                    {
                        x.url = new Uri("http://google.com.vn");
                    }
                }
            }
            
        }

        private static IEnumerable<Item> GetItems()
        {

            var items = new List<Item>()
            {
                new Item{ Name = "1", Links = new List<Link>(){
                    new Link() {
                        subLinks = new List<SubLink>(){
                            new SubLink()
                        }
                    },
                    new Link(){  subLinks = new List<SubLink>(){
                            new SubLink()
                        }},
                    new Link(){ subLinks = new List<SubLink>(){
                            new SubLink()
                        } },
                } },
                new Item{ Name = "2",  Links = new List<Link>(){
                    new Link() {
                        subLinks = new List<SubLink>(){
                            new SubLink()
                        }
                    },
                    new Link(){  subLinks = new List<SubLink>(){
                            new SubLink()
                        }},
                    new Link(){ subLinks = new List<SubLink>(){
                            new SubLink()
                        } },
                }},
            };
            return  items.AsReadOnly();
        }
    }

}
