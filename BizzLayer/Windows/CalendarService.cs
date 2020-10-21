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
    }
}
