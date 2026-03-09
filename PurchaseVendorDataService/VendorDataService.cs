using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PurchaseVendorDataService
{
    public class VendorDataService
    {
        public List<Vendor> vendors = new List<Vendor>();

        public VendorDataService()
        {
            Vendor nescafe = new Vendor
            {
                VendorID = Guid.NewGuid(),
                VendorName = "NESCAFE",
                VendorDescription = "Coffee Maker Brand",
                ContactPhone = "0912345678",
                ContactEmail = "account@nescafe.com.ph"
            };

            Vendor rebisco = new Vendor
            {
                VendorID = Guid.NewGuid(),
                VendorName = "REBISCO",
                VendorDescription = "Maker of Biscuits",
                ContactPhone = "0912345678",
                ContactEmail = "account@rebisco.com.ph"
            };

            Vendor sanmig = new Vendor
            {
                VendorID = Guid.NewGuid(),
                VendorName = "SAN MIGUEL CORPORATION",
                VendorDescription = "Beverage Brand",
                ContactPhone = "0912345678",
                ContactEmail = "account@sanmigcorp.com.ph"
            };

            Vendor nestle = new Vendor
            {
                VendorID = Guid.NewGuid(),
                VendorName = "NESTLE",
                VendorDescription = "Variety Goods Maker",
                ContactPhone = "0912345678",
                ContactEmail = "account@nestle.com.ph"
            };

            Vendor oishi = new Vendor
            {
                VendorID = Guid.NewGuid(),
                VendorName = "OISHI",
                VendorDescription = "Chip Making Brand",
                ContactPhone = "0912345678",
                ContactEmail = "account@oishi.com.ph"
            };

            vendors.Add(nescafe);
            vendors.Add(rebisco);
            vendors.Add(sanmig);
            vendors.Add(nestle);
            vendors.Add(oishi);
        }

        public void AddV(Vendor vendor)
        {
            vendors.Add(vendor);
        }

    }
}
