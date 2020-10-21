using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DbGetters
    {
        public void getPersonalDataById(int id)
        {
            using (var context = new RoomRentEntities())
            {
                var personal = context.PersonalData
                    .Where(c => c.id == id)
                    .FirstOrDefault();
            }
        }

        public PersonalData getPersonalDataByPhone(long phoneNumber)
        {
            PersonalData personal = new PersonalData();
            using (var context = new RoomRentEntities())
            {
                personal = context.PersonalData
                    .Where(c => c.phone_number == phoneNumber)
                    .FirstOrDefault();
            }

            return personal;
        }

        public Users getUserByHash(string hash)
        {
            Users user = new Users();
            using (var context = new RoomRentEntities())
            {
                user = context.Users
                    .Where(c => c.password == hash)
                    .FirstOrDefault();
            }

            return user;
        }

        public string getSalt(string username)
        {
            Users user = new Users();
            using (var context = new RoomRentEntities())
            {
                user = context.Users
                    .Where(c => c.username == username)
                    .FirstOrDefault();
            }

            return user.salt;
        }

        public List<Rents> getUserRents(long userId)
        {
            List<Rents> rents = new List<Rents>();
            using (var context = new RoomRentEntities())
            {
                rents = context.Rents
                    .Where(c => c.user_id == userId)
                    .ToList();
            }

            return rents;
        }

        public List<Addresses> getUserAddresses(long userId)
        {
            List<long> userAddressesIds = new List<long>();
            List<Addresses> addresses = new List<Addresses>();
            using (var context = new RoomRentEntities())
            {
                userAddressesIds = context.UserAddresses
                    .Where(c => c.usr_id == userId)
                    .Select(c => c.addr_id)
                    .ToList();
                addresses = context.Addresses.Where(c => userAddressesIds.Contains(c.id)).ToList();
            }

            return addresses;
        }
    }
}
