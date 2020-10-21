using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;

namespace BizzLayer.Windows
{
    public class HistoryService
    {
        DbGetters dbGetters = new DbGetters();

        public List<Rents> getUserRents(long userId)
        {
            return dbGetters.getUserRents(userId);
        }
    }
}
