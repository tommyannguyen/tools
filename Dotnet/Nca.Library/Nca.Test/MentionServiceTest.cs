using Nca.Library.Interfaces;
using Nca.Library.Services;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace Nca.Test
{
    public class MentionServiceTest
    {
        [Test]
        public async Task TestRegularExpression()
        {
            IMentionService mentionService = new MentionService();


            var html = File.ReadAllText("Data\\test.html");
            var users = await mentionService.DetectAsync(html);
            Assert.True(true);
        }
    }
}
