using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseVendorDataService
{
    public interface IVendorDataService
    {
        void AddV(Vendor vendor);
        Vendor? GetById(Guid id);
        Vendor? GetByVendorName(string vendor); // foreach a in List<Vendor>, first found instance of said query (vendor) is returned
        bool VendorExists(string vendor); // compare if inputted string vendor returns true when there is an equal to it "=="
        void Update(Vendor vendor);
        void RemoveV(string vendorName); // remove vendor object from the list by name
        void RemoveAllVen();
        List<Vendor> GetVendors();
        int GetVendorCount();
    }
}
