using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BW8AGU.Samples.CSharp.Common
{
    public static class TempDataHelper
    {
        public async static void SaveTempData(string data)
        {
            StorageFile tempFile = await ApplicationData.Current.TemporaryFolder.CreateFileAsync("temporary.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(tempFile, data);
        }

        public async static Task<String> GetTempData()
        {
            try
            {
                StorageFile tempFile = await ApplicationData.Current.TemporaryFolder.GetFileAsync("temporary.txt");
                String content = await FileIO.ReadTextAsync(tempFile);
                return content;
            }
            catch
            {
                throw new Exception("Data not found!");
            }
        }
    }
}
