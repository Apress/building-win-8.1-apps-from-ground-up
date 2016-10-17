using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW8AGU.Samples.CSharp.FakeServiceReference
{
    public partial class FakeServiceClient : System.ServiceModel.ClientBase<BW8AGU.Samples.CSharp.FakeServiceReference.IFakeService>,
        BW8AGU.Samples.CSharp.FakeServiceReference.IFakeService
    {
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint,
            System.ServiceModel.Description.ClientCredentials clientCredentials)
        {
            if (serviceEndpoint.Name ==
                FakeServiceReference.FakeServiceClient
                    .EndpointConfiguration.BasicHttpBinding_IFakeService.ToString())
            {
                serviceEndpoint.Binding.SendTimeout = new System.TimeSpan(0, 1, 0);
            }
        }
    }

}
