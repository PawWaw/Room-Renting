using DataLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizzLayer.Windows
{
    public class CalendarService
    {
        DbGetters dbGetters = new DbGetters();

        public List<Addresses> getUserAddresses(long userId)
        {
            return dbGetters.getUserAddresses(userId);
        }

        public List<Rents> getUserFutureRents(long userId)
        {
            return dbGetters.getUserFutureRents(userId);
        }

        public string getAddress(long addressId)
        {
            return dbGetters.getAddress(addressId);
        }

        public string getPerson(long userId)
        {
            return dbGetters.getPerson(userId);
        }

        public List<Rents> getRenterFutureRents(List<long> ids)
        {
            return dbGetters.getRenterFutureRents(ids);
        }
    }
}
