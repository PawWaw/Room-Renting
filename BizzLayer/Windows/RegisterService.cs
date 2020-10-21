using DataLayer;
using System;

namespace BizzLayer
{
    public class RegisterService
    {
        DbGetters dbGetters = new DbGetters();

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
