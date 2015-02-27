using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//created by Garrett
namespace TravelExperts
{
    public class Packages_Products_Suppliers
    {
        public int PackageId { get; set; }
        public int ProductSupplierId { get; set; }

        public string ProdName { get; set; }

        public string SupName { get; set; }
        // constructor
        public Packages_Products_Suppliers() { }
    }
}
