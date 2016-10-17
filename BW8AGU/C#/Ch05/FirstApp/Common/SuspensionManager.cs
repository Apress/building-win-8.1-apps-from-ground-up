using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FirstApp.Common
{
    internal sealed class SuspensionManager
    {
        public static async Task SaveAsync()
        {
            try
            {
                //Save everything in here
            }
            catch (Exception e)
            {
                throw new SuspensionManagerException(e);
            }
        }

        public static async Task RestoreAsync()
        {
            try
            {
                //Restore everything in here
            }
            catch (Exception e)
            {
                throw new SuspensionManagerException(e);
            }
        }
    }

    public class SuspensionManagerException : Exception
    {
        public SuspensionManagerException()
        {
        }

        public SuspensionManagerException(Exception e)
            : base("SuspensionManager failed", e)
        {

        }
    }
}
