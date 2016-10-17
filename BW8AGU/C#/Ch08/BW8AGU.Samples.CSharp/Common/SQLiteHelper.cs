using BW8AGU.Samples.CSharp.DataModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW8AGU.Samples.CSharp.Common
{
    public static class SQLiteHelper
    {
        public async static void CreateDatabase()
        {
            var storagePath = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = await storagePath.TryGetItemAsync("db.sqlite");

            if (file == null)
            {
                var dbPath = System.IO.Path.Combine(
                    Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

                try
                {
                    using (var db = new SQLiteConnection(dbPath))
                    {
                        db.CreateTable<SimpleAppointment>();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void AddAppointment(SimpleAppointment appointment)
        {
            var dbPath = System.IO.Path.Combine(
                Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            try
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    db.RunInTransaction(() =>
                            {
                                db.Insert(new SimpleAppointment()
                                {
                                    Id = appointment.Id,
                                    Subject = appointment.Subject,
                                    Location = appointment.Location,
                                    StartDate = appointment.StartDate,
                                    EndDate = appointment.EndDate
                                });
                            }
                    );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SimpleAppointment GetAppointment(int id)
        {
            var dbPath = System.IO.Path.Combine(
                Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            try
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    return db.Find<SimpleAppointment>(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
