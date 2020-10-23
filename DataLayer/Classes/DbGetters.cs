using DataLayer.Classes;
using Room_Renting.WS;
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

        public List<Rents> getUserNotRatedRents(long userId)
        {
            List<Rents> rents = new List<Rents>();
            using (var context = new RoomRentEntities())
            {
                rents = context.Rents
                    .Where(c => c.user_id == userId && !c.rated)
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

        public Users getUser(long userId)
        {
            Users user = new Users();
            using (var context = new RoomRentEntities())
            {
                user = context.Users
                    .Where(c => c.id == userId)
                    .FirstOrDefault();
            }

            return user;
        }

        public List<long> getRentsByDate(FlatSearchCriteria criteria)
        {
            List<long> rents = new List<long>();
            using (var context = new RoomRentEntities())
            {
                rents = context.Rents
                    .Where(c => (c.startDate < criteria.startDate && c.endDate > criteria.startDate) || (c.startDate < criteria.endDate && c.endDate > criteria.endDate) || (c.startDate > criteria.startDate && c.endDate < criteria.endDate))
                    .Select(c => c.address_id)
                    .ToList();
            }

            return rents;
        }

        public List<Addresses> getFreeAddresses(List<long> rentIds, FlatSearchCriteria criteria)
        {
            List<Addresses> addresses = new List<Addresses>();
            using (var context = new RoomRentEntities())
            {
                var temp = context.Addresses
                    .Where(c => !rentIds.Contains(c.id));

                if (!criteria.city.Equals(""))
                {
                    temp = temp.Where(c => c.city.Contains(criteria.city));
                }

                addresses = temp.ToList();

            }

            return addresses;
        }

        public List<UserAddresses> getUserAddressList(List<long> addressIds, FlatSearchCriteria criteria)
        {
            List<UserAddresses> addresses = new List<UserAddresses>();
            using (var context = new RoomRentEntities())
            {
                var temp = context.UserAddresses
                    .Where(c => addressIds.Contains(c.addr_id))
                    .Where(c => c.kitchen == criteria.kitchen)
                    .Where(c => c.parking == criteria.parking)
                    .Where(c => c.animals == criteria.animals);

                if (criteria.bedCount != null)
                {
                    temp = temp.Where(c => c.bed_count >= criteria.bedCount);
                }
                if (criteria.price != null)
                {
                    if (criteria.over)
                    {
                        temp = temp.Where(c => c.price >= criteria.price);
                    }
                    else
                    {
                        temp = temp.Where(c => c.price <= criteria.price);
                    }
                }

                addresses = temp.ToList();
            }

            return addresses;
        }
    }
}
