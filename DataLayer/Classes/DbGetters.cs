using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DbGetters
    {
        public void getUserById(int id)
        {
            using (var context = new RoomRentEntities())
            {
                var personal = context.PersonalData.Where(c => c.id == id).FirstOrDefault();
            }
        }
    }
}
