using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;
using DataLayer.Classes;

namespace BizzLayer.Windows
{
    public class CreateService
    {
        DbSetters dbSetters = new DbSetters();

        public void addRent(Rents address)
        {
            dbSetters.addRent(address);
        }
    }
}
