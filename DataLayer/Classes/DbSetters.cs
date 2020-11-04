using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class DbSetters
    {

        public void isRated(long id, int value)
        {
            using (var context = new RoomRentEntities())
            {
                var personal = context.Rents
                    .Where(c => c.id == id)
                    .FirstOrDefault();
                if (personal != null)
                {
                    personal.isRated = true;
                    personal.rate = value;
                    context.SaveChanges();
                }
            }
        }

        public void addRent(Rents address)
        {
            using (var context = new RoomRentEntities())
            {
                context.Rents.Add(address);
                context.SaveChanges();
            }
        }

        public void rateAddress(int rate, long id)
        {
            using (var context = new RoomRentEntities())
            {
                var address = context.UserAddresses
                    .Where(c => c.addr_id == id)
                    .FirstOrDefault();
                if (address != null)
                {   
                    if (address.rateCount != null)
                    {
                        double? temp = (address.rateCount * address.rate + rate) / (address.rateCount + 1);
                        address.rateCount += 1;
                        address.rate = temp;
                    }
                    else
                    {
                        address.rateCount = 1;
                        address.rate = rate;
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
