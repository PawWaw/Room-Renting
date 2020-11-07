using Room_Renting.WS;
using System.Collections.Generic;

namespace BizzLayer.Windows
{
    public class ExportService
    {
        HistoryService historyService = new HistoryService();

        public bool exportToCsv()
        {
            List<WSRents> rents = historyService.getUserRents(LoginService.userId);
            if (rents.Count == 0)
            {
                return false;
            }
            else
            {
                string str = "";
                for (int i = 0; i < rents.Count; i++)
                {
                    str += rents[i].address + "," + rents[i].startDate + "," + rents[i].endDate + "," + rents[i].rate + ",\n";
                }

                System.IO.File.WriteAllText(@"..\export.csv", str);
                return true;
            }  
        }
    }
}
