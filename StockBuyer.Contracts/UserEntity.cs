using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBuyer.Contracts
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Int64 TotalCash { get; set; }
    }
}
