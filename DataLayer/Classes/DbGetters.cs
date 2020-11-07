using DataLayer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Users getUserByHash(string username, string hash)
        {
            Users user = new Users();
            using (var context = new RoomRentEntities())
            {
                user = context.Users
                    .Where(c => c.username == username)
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
                    .Where(c => c.startDate < DateTime.Now)
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
                    .Where(c => c.user_id == userId && !c.isRated)
                    .ToList();
            }

            return rents;
        }

        public List<Rents> getUserFutureRents(long userId)
        {
            List<Rents> rents = new List<Rents>();
            using (var context = new RoomRentEntities())
            {
                rents = context.Rents
                    .Where(c => c.user_id == userId)
                    .Where(c => c.startDate > DateTime.Now)
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
                    .Where(c => addressIds.Contains(c.addr_id));

                if (criteria.kitchen)
                {
                    temp = temp.Where(c => c.kitchen == true);
                }
                if (criteria.parking)
                {
                    temp = temp.Where(c => c.parking == true);
                }
                if (criteria.animals)
                {
                    temp = temp.Where(c => c.animals == true);
                }
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

        public string getAddress(long addressId)
        {
            Addresses temp = new Addresses();
            using (var context = new RoomRentEntities())
            {
                temp = context.Addresses
                    .Where(c => c.id == addressId).FirstOrDefault();
            }
            string str = temp.country + ", " + temp.city + " " + temp.street + " " + temp.house;

            if (temp.flat != null)
            {
                str += "/" + temp.flat;
            }

            return str;
        }

        public string getPerson(long userId)
        {
            Users temp = new Users();
            PersonalData temp2 = new PersonalData();
            using (var context = new RoomRentEntities())
            {
                temp = context.Users
                    .Where(c => c.id == userId).FirstOrDefault();

                temp2 = context.PersonalData
                    .Where(d => d.id == temp.personal_id).FirstOrDefault();
            }


            string str = temp2.name + " " + temp2.surname;

            return str;
        }
    }
}
