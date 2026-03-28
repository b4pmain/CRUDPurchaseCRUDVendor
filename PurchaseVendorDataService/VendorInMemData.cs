using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;

namespace PurchaseVendorDataService
{
    public class VendorInMemData : IVendorDataService
    {
        public List<Vendor> vendors = new List<Vendor>();

        public VendorInMemData()
        {
            Vendor nescafe = new Vendor // populate 1
            {
                VendorID = Guid.NewGuid(),
                VendorName = "NESCAFE",
                VendorDescription = "Coffee Maker Brand",
                ContactPhone = "0912345678",
                ContactEmail = "account@nescafe.com.ph"
            };

            Vendor rebisco = new Vendor // populate 2
            {
                VendorID = Guid.NewGuid(),
                VendorName = "REBISCO",
                VendorDescription = "Maker of Biscuits",
                ContactPhone = "0912345678",
                ContactEmail = "account@rebisco.com.ph"
            };

            Vendor sanmig = new Vendor // populate 3
            {
                VendorID = Guid.NewGuid(),
                VendorName = "SAN MIGUEL CORPORATION",
                VendorDescription = "Beverage Brand",
                ContactPhone = "0912345678",
                ContactEmail = "account@sanmigcorp.com.ph"
            };

            Vendor nestle = new Vendor // populate 4
            {
                VendorID = Guid.NewGuid(),
                VendorName = "NESTLE",
                VendorDescription = "Variety Goods Maker",
                ContactPhone = "0912345678",
                ContactEmail = "account@nestle.com.ph"
            };

            Vendor oishi = new Vendor // populate 5
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

        public void AddVendor(Vendor vendor)
        {
            vendors.Add(vendor);
        }

        public Vendor? GetById(Guid id) // foreach a in List<Vendor>, first found instance of Guid id (VendorID) is returned
        {
            return vendors.FirstOrDefault(a => a.VendorID == id);
        }

        public Vendor? GetByVendorName(string vendor) // foreach a in List<Vendor>, first found instance of said query (vendor) is returned
        {
            return vendors.FirstOrDefault(a => a.VendorName == vendor);
        }

        public bool VendorExists(string vendor) // compare if inputted string vendor returns true when there is an equal to it "=="
        {
            return vendors.Any(a => a.VendorName == vendor);
        }

        public void UpdateVendor(Vendor vendor)
        {
            var existing = GetById(vendor.VendorID);
            if (existing != null)
            {
                existing.VendorName = vendor.VendorName;
                existing.VendorDescription = vendor.VendorDescription;
                existing.ContactPhone = vendor.ContactPhone;
                existing.ContactEmail = vendor.ContactEmail;
            }
        }

        public void DeleteVendor(string vendorName) // remove vendor object from the list by name
        { 
            vendors.Remove(vendors.First(a => a.VendorName == vendorName));
        }
        public void DeleteAllVen()
        {
            vendors.Clear();
        }
        public List<Vendor> GetVendors()
        {
            return vendors;
        }

        public int GetVendorCount()
        {
            int count = vendors.Count;
            return count;
        }

    }
}
