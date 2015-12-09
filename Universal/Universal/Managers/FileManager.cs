using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using static Universal.Managers.ExceptionManager;

namespace Universal.Managers
{
    public static class FileManager
    {
        public static string AppPrefix = @"ms-appx:///";

        public static async Task<StorageFile> GetApplicationFile(string relativepathfile)
        {
            try{
                var path = Path.Combine(AppPrefix, relativepathfile);
                return await StorageFile.GetFileFromApplicationUriAsync(new Uri(path));
            }
            catch (Exception ex)
            {
                Launch(ex);
                return null;
            }
        }

        public static async Task<String> ReadFile(StorageFile storageFile)
        {
            try {
                return await FileIO.ReadTextAsync(storageFile);
            }
            catch (Exception ex)
            {
                Launch(ex);
                return null;
            }
        }

        public static async Task<String> GetTextfromApplicationFile(string relativepathfile)
        {
            try {
                var storageFile = await GetApplicationFile(relativepathfile);
                return await ReadFile(storageFile);
            }
            catch (Exception ex)
            {
                Launch(ex);
                return null;
            }
        }

        public static async Task<T> GetObjectfromApplicationFile<T>(string relativepathfile)
        {
            try {
                var stringContent = await GetTextfromApplicationFile(relativepathfile);
                return await Task.Run(() => { return JsonConvert.DeserializeObject<T>(stringContent); });
            }
            catch (Exception ex)
            {
                Launch(ex);
                return default(T);
            }
        }
    };
}
