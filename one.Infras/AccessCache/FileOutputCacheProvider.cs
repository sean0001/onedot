using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Configuration;



namespace one.Infras.AccessCache
{
   


    public class FileCacheProvider : OutputCacheProvider
    {
        //private const string CacheDirectory = @"C:\SampleCache";

        private static readonly string CacheDirectory = ConfigurationManager.AppSettings["FileCachePath"];

        public override object Add(string key, object entry, DateTime utcExpiry)
        {
            string filePath = GetFilePathFromKey(key);
            var cachedItem = GetCachedItem(filePath);

            if (cachedItem != null && cachedItem.UtcExpiry.ToUniversalTime() <= DateTime.UtcNow)
            {
                Remove(key);
            }
            else if (cachedItem != null)
            {
                return cachedItem.Item;
            }

            SaveCachedItem(new CachedItem()
            {
                Item = entry,
                UtcExpiry = utcExpiry
            }, filePath);

            return entry;
        }

        public override object Get(string key)
        {
            string filePath = GetFilePathFromKey(key);
            var cachedItem = GetCachedItem(filePath);
            if (cachedItem != null)
            {
                if (cachedItem.UtcExpiry.ToUniversalTime() <= DateTime.UtcNow)
                {
                    Remove(key);
                }
                else
                {
                    return cachedItem.Item;
                }
            }

            return null;
        }

        public override void Remove(string key)
        {
            string filePath = GetFilePathFromKey(key);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public override void Set(string key, object entry, DateTime utcExpiry)
        {
            string filePath = GetFilePathFromKey(key);
            var cachedItem = new CachedItem() { Item = entry, UtcExpiry = utcExpiry };
            SaveCachedItem(cachedItem, filePath);
        }

        private CachedItem GetCachedItem(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            using (var stream = File.OpenRead(filePath))
            {
                var binaryFormatter = new BinaryFormatter();
                return binaryFormatter.Deserialize(stream) as CachedItem;
            }
        }

        private void SaveCachedItem(CachedItem cachedItem, string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var stream = File.OpenWrite(filePath))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, cachedItem);
            }
        }

        private string GetFilePathFromKey(string key)
        {
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
                key = key.Replace(invalidChar, '_');

            return Path.Combine(CacheDirectory, key);
        }
    }

}
