using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FakeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IFakeService
    {

        [OperationContract]
        SimpleAppointment GetSimpleAppointment();
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class SimpleAppointment
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }
    }
}
