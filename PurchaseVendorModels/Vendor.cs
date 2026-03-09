using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseVendorModels
{
    public class Vendor
    {
        public Guid VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorDescription { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
    }
}
