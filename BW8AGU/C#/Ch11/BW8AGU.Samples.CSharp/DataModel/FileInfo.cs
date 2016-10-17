using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW8AGU.Samples.CSharp.DataModel
{
    public class FileInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string IsAvailable { get; set; }
    }

    public class FileInfoCollection
    {
        public ObservableCollection<FileInfo> Items { get; set; }
    }
}
