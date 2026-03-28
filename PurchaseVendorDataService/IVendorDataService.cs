using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseVendorDataService
{
    public interface IVendorDataService
    {
        void AddVendor(Vendor vendor);
        Vendor? GetById(Guid id);
        Vendor? GetByVendorName(string vendor); // foreach a in List<Vendor>, first found instance of said query (vendor) is returned
        bool VendorExists(string vendor); // compare if inputted string vendor returns true when there is an equal to it "=="
        void UpdateVendor(Vendor vendor);
        void DeleteVendor(string vendorName); // remove vendor object from the list by name
        void DeleteAllVen();
        List<Vendor> GetVendors();
        int GetVendorCount();
    }
}
