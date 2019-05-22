using Nca.Library.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace Nca.Library.Repositories
{
    public class CacheManagerRepository : ICacheFactory
    {
        public Interfaces.ICache<T> CreateCache<T>()
        {
            return new SimpleICache<T>();
        }
    }
}
