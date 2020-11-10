using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Classes
{
    public class DbSetters
    {
        DbGetters dbGetters = new DbGetters();

        public long addAddress(Addresses address)
        {
            Addresses temp = new Addresses();

            using (var context = new RoomRentEntities())
            {
                temp = context.Addresses.Add(address);
                context.SaveChanges();
            }

            return temp.id;
        }

        public void updateUser(Users user)
        {
            using (var context = new RoomRentEntities())
            {
                var temp = context.Users
                    .Where(c => c.username == user.username)
                    .FirstOrDefault();
                if (temp != null)
                {
                    temp.email_addr = user.email_addr;
                    temp.acc_type = user.acc_type;
                    context.SaveChanges();
                }
            }
        }

        public void deleteRent(long v)
        {
            using (var context = new RoomRentEntities())
            {
                var temp = context.Rents
                    .Where(c => c.id == v)
                    .FirstOrDefault();

                if (temp != null)
                {
                    context.Rents.Remove(temp);
                    context.SaveChanges();
                }
            }
        }

        public void updatePersonalData(PersonalData personalData)
        {
            using (var context = new RoomRentEntities())
            {
                var temp = context.PersonalData
                    .Where(c => c.id == personalData.id)
                    .FirstOrDefault();
                if (temp != null)
                {
                    temp.phone_number = personalData.phone_number;
                    temp.name = personalData.name;
                    temp.surname = personalData.surname;
                    context.SaveChanges();
                }
            }
        }

        public void addUserAddress(UserAddresses userAddresses)
        {
            using (var context = new RoomRentEntities())
            {
                context.UserAddresses.Add(userAddresses);
                context.SaveChanges();
            }
        }

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

        public void createUser(Users user)
        {
            using (var context = new RoomRentEntities())
            {
                Users newUser = new Users
                {
                    personal_id = user.personal_id,
                    username = user.username,
                    password = user.password,
                    email_addr = user.email_addr,
                    acc_type = user.acc_type,
                    is_active = true,
                    is_banned = false,
                    verified = true,
                    create_date = DateTime.Now,
                    salt = user.salt
                };
                context.Users.Add(newUser);
                context.SaveChanges();
            };
        }

        public long createPersonalData(PersonalData data)
        {
            using (var context = new RoomRentEntities())
            {
                PersonalData personalData = new PersonalData
                {
                    name = data.name,
                    surname = data.surname,
                    phone_number = data.phone_number,
                };
                PersonalData returned = context.PersonalData.Add(personalData);
                context.SaveChanges();
            };

            return getNewDataId(data.phone_number);
        }

        private long getNewDataId(long phoneNumber)
        {
            PersonalData data = dbGetters.getPersonalDataByPhone(phoneNumber);

            if (data != null)
            {
                return data.id;
            }

            return 0;
        }
    }
}
