using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Classes;
using Room_Renting.WS;

namespace BizzLayer.Windows
{
    public class ReviewService
    {
        DbGetters dbGetters = new DbGetters();
        DbSetters dbSetters = new DbSetters();

        public List<WSRents> getUserRents(long userId)
        {
            List<WSRents> ws = new List<WSRents>();
            List<Rents> rents = dbGetters.getUserNotRatedRents(userId);
            List<Addresses> addresses = dbGetters.getUserAddresses(userId);
            for (int i = 0; i < rents.Count; i++)
            {
                WSRents temp = new WSRents();
                temp.startDate = rents[i].startDate;
                temp.endDate = rents[i].endDate;
                temp.addressId = rents[i].address_id;
                temp.id = rents[i].id;
                temp.rate = rents[i].rate;
                for (int j = 0; j < addresses.Count; j++)
                {
                    if (rents[i].address_id == addresses[j].id)
                    {
                        temp.address = addresses[j].city;
                    }
                }
                ws.Add(temp);
            }

            return ws;
        }

        public void rateAddress(int rate, long id)
        { 
            dbSetters.rateAddress(rate, id);
        }
    }
}
