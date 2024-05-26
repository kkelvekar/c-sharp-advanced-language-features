using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain
{
    public class OrderProcessEventArgs : EventArgs
    {
        public Order Order { get; set; }

        public int TotalOrderPrice { get; set; }
    }
}
