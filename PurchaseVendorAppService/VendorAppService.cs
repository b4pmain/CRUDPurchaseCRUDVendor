using System;
using System.Collections.Generic;
using System.Text;
using PurchaseVendorDataService;
using PurchaseVendorModels;

namespace PurchaseVendorAppService
{
    public class VendorAppService
    {
        VendorDataService vendorDataService = new VendorDataService();

        public bool AddVendor(Vendor newVendor)
        {
            if (vendorDataService.VendorExists(newVendor.VendorName))
                return false;

            var vendor = new Vendor
            {
                VendorID = Guid.NewGuid(),
                VendorName = newVendor.VendorName,
                VendorDescription = newVendor.VendorDescription,
                ContactPhone = newVendor.ContactPhone,
                ContactEmail = newVendor.ContactEmail
            };

            vendorDataService.AddV(vendor);
            return true;
        }


    }
}
