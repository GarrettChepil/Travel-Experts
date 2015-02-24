using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts
{
    // a class to represent a supplier 
    // created by Garrett Chepil Team 4
    // Dec 9 2014
    public class Supplier
    {
        // private data
        private int supplierId;
        private string supName;

        // public properties
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }
        public string SupName
        {
            get { return supName; }
            set { supName = value; }
        }

        // constructor

        public Supplier() { }
        public Supplier(int supplierId, string supName)
        {
            this.supplierId = supplierId;
            this.supName = supName;
        }

        // Tostring method
        public override string ToString()
        {
            return supplierId.ToString() + ", " + supName;
        }
    }
}
