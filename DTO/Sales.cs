using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_ShoppeMobile.DTO
{
    class Sales
    {
        public int SlsId { get; set; }  
        public string IMEINO { get; set; }  
        public DateTime PurchageDate { get; set; }
        public decimal Price { get; set; }
        public int CusId { get; set; } 
    }
}
