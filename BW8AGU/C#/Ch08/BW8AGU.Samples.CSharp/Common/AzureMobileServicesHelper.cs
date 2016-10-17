using BW8AGU.Samples.CSharp.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW8AGU.Samples.CSharp.Common
{
    public static class AzureMobileServicesHelper
    {
        public async static void InsertDataFromAzureMobileSvc(SimpleAppointment item)
        {
            await App.bw8agu_mobileserviceClient.GetTable<SimpleAppointment>()
                .InsertAsync(item);
        }

        public async static Task<MobileServiceCollection<SimpleAppointment, SimpleAppointment>> GetDataFromAzureMobileSvc()
        {
            try
            {
                IMobileServiceTable<SimpleAppointment> table =
                    App.bw8agu_mobileserviceClient
                    .GetTable<SimpleAppointment>();

                return await table.ToCollectionAsync<SimpleAppointment>();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public static void UpdateDataFromAzureMobileSvc(SimpleAppointment item)
        {
            try
            {
                IMobileServiceTable<SimpleAppointment> table = App.bw8agu_mobileserviceClient.GetTable<SimpleAppointment>();

                table.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
