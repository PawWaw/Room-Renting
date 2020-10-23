using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Classes;

namespace BizzLayer.Windows
{
    public class FlatsService
    {
        DbGetters dbGetters = new DbGetters();

        public List<WSAddresses> getAvailableRooms(FlatSearchCriteria criteria)
        {
            List<WSAddresses> ws = new List<WSAddresses>();
            List<long> rentIds = dbGetters.getRentsByDate(criteria);
            List<Addresses> addresses = dbGetters.getFreeAddresses(rentIds, criteria);
            List<long> addressIds = new List<long>();

            for (int i = 0; i < addresses.Count; i++)
            {
                addressIds.Add(addresses[i].id);
            }

            List<UserAddresses> userAddresses = dbGetters.getUserAddressList(addressIds, criteria);

            for (int j = 0; j < userAddresses.Count; j++)
            {
                WSAddresses temp = new WSAddresses();
                temp.address_id = addresses[j].id;
                temp.address = addresses[j].country + ", " + addresses[j].city + ", " + addresses[j].street + " " + addresses[j].house;
                temp.bed_count = userAddresses[j].bed_count;
                temp.price = userAddresses[j].price;
                temp.rating = userAddresses[j].rate;
                temp.rateCount = userAddresses[j].rateCount;
                ws.Add(temp);
            }

            return ws;
        }
    }
}
