using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nca.Library.Interfaces
{
    public interface IMentionService
    {
        Task<IEnumerable<string>> DetectAsync(string html);
    }
}
