using DataLayer;
using System;

namespace BizzLayer
{
    public class LoginService
    {
        public static long userId = 0;
        public static bool isRenter = false;

        DbGetters dbGetters = new DbGetters();

        public PersonalData findPersonalData(int phoneNumber)
        {
            return dbGetters.getPersonalDataByPhone(phoneNumber);
        }

        public string getSalt(string username)
        {
            return dbGetters.getSalt(username);
        }

        public Users getUserByHash(string hash)
        {
            return dbGetters.getUserByHash(hash);
        }
    }
}
