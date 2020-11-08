using DataLayer;
using DataLayer.Classes;
using System;

namespace BizzLayer
{
    public class RegisterService
    {
        DbGetters dbGetters = new DbGetters();
        DbSetters dbSetters = new DbSetters();

        public void updateUser(Users user)
        {
            dbSetters.updateUser(user);
        }

        public void updatePersonalData(PersonalData personalData)
        {
            dbSetters.updatePersonalData(personalData);
        }

        public void createUser(Users user)
        {
            dbSetters.createUser(user);
        }

        public long createPersonalData(PersonalData personalData)
        {
            return dbSetters.createPersonalData(personalData);
        }
    }
}
