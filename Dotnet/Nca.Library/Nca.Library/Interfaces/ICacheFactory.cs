using System.Collections.Generic;
using System.Text;

namespace Nca.Library.Interfaces
{
    public interface ICacheFactory
    {
        ICache<T> CreateCache<T>();
    }
}
