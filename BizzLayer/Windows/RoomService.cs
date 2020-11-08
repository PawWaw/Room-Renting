using DataLayer;
using DataLayer.Classes;

namespace BizzLayer.Windows
{
    public class RoomService
    {
        DbSetters dbSetters = new DbSetters();

        public long addAddress(Addresses address)
        {
            return dbSetters.addAddress(address);
        }

        public void addUserAddress(UserAddresses userAddresses)
        {
            dbSetters.addUserAddress(userAddresses);
        }
    }
}
