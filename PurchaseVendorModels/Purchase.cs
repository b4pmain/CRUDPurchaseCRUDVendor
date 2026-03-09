using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseVendorModels
{
    public class Purchase
    {
        public Guid ProductID { get; set; }
        public string PurchaseVndr { get; set; }
        public string PurchaseName { get; set; }
        public int PurchaseQty { get; set; }
        public float PurchasePrice { get; set; }
        public string PurchaseDate { get; set; }

    }
}
