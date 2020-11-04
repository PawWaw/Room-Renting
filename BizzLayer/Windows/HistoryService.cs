using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using Room_Renting.WS;

namespace BizzLayer.Windows
{
    public class HistoryService
    {
        DbGetters dbGetters = new DbGetters();

        public List<WSRents> getUserRents(long userId)
        {
            List<WSRents> ws = new List<WSRents>();
            List<Rents> rents = dbGetters.getUserRents(userId);
            List<Addresses> addresses = dbGetters.getUserAddresses(userId);
            for (int i = 0; i < rents.Count; i++)
            {
                WSRents temp = new WSRents();
                temp.startDate = rents[i].startDate;
                temp.endDate = rents[i].endDate;
                temp.id = rents[i].id;
                temp.rate = rents[i].rate;
                for (int j = 0; j < addresses.Count; j++)
                {
                    if (rents[i].address_id == addresses[j].id)
                    {
                        temp.address = addresses[j].country + ", " + addresses[j].city + ", " + addresses[j].street + " " + addresses[j].house;
                        if (addresses[j].flat != null)
                        {
                            temp.address += "/" + addresses[j].flat;
                        }
                    }
                }
                ws.Add(temp);
            }

            return ws;
        }
    }
}
