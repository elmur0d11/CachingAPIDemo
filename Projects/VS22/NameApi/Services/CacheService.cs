using System.Runtime.Caching;

namespace NameApi.Services
{
    public class CacheService : ICacheService
    {
        #region Getting ObjectCache
        private ObjectCache _memoryCache = MemoryCache.Default;
        #endregion

        #region 1 Function for get data from cache
        public T GetData<T>(string key)
        {
            try
            {
                //There's used Get. Get is special function located in ObjectCache
                //For use Get into the Get we give a key
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region 2 Function for remove data from cache
        public object RemoveData(string key)
        {
            var res = true;
            try
            {
                //cheking cache is not null or empty
                if (!string.IsNullOrEmpty(key))
                {
                    //There's used Remove function. Remove is special function located in ObjectCache
                    //For use Remove into the Remove we give a key
                    var result = _memoryCache.Remove(key);
                }
                else
                    res = false;

                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region 3 Function for add data into cache
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var res = true;

            try
            {
                //cheking key is not null or empty
                if (!string.IsNullOrEmpty(key))
                    //There's used Set function. Set is special function located in ObjectCache
                    //For use Set into the Set we give a key, value and expiration-time
                    _memoryCache.Set(key, value, expirationTime);
                else
                    res = false;
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
