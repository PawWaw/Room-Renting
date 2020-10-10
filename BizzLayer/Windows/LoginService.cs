using DataLayer;
using System;

namespace BizzLayer
{
    public class LoginService
    {

        DbGetters dbGetters = new DbGetters();

        public void findPersonalData(int id)
        {
            dbGetters.getUserById(id);
        }
    }
}
