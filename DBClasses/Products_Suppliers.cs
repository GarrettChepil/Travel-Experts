using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts
{
    // class created by Levi for the Product and suppliers table
    public class Products_Suppliers
    {
        //private data
        private int ProductSupplierId;
        private int ProductId;
        private int SupplierId;
        


        public Products_Suppliers() { }
        // constructor with parameters
        public Products_Suppliers(int ps, Product pi, Supplier si)
        {
            ProductSupplierId  = ps;
            ProductId = pi.ProductId;
            SupplierId = si.SupplierId;
        }

        // public properties
        public int productsupplierid
        {
            get
            {
                return ProductSupplierId;
            }
            set
            {
                ProductSupplierId = value;
            }
        }
        public int productid
        {
            get
            {
                return ProductId;
            }
            set
            {
                ProductId = value;
            }
        }
        public int supplierid
        {
            get
            {
                return SupplierId;
            }
            set
            {
                SupplierId = value;
            }
        }

        public string productName { get; set; }
        public string SupName { get; set; }

        // ToString method
        public override string ToString()
        {
            return productName;
        }
    }
}
