using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BW8AGU.Samples.CSharp.DataModel
{
    public class SimpleAppointment
    {
        [SQLite.PrimaryKey]
        [SQLite.AutoIncrement]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
