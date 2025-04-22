using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_ShoppeMobile.DTO
{
    class Transaction
    {
        public int TransId { get; set; }  
        public int ModelId { get; set; } 
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
