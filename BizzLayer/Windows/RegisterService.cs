using DataLayer;
using System;

namespace BizzLayer
{
    public class RegisterService
    {
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
                };
                context.Users.Add(user);
                context.SaveChanges();
            };
        }
    }
}
