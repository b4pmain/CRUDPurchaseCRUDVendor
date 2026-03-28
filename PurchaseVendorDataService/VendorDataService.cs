using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;

namespace PurchaseVendorDataService
{
    public class VendorDataService
    {
        IVendorDataService _vendorDataService;
        public VendorDataService(IVendorDataService vendorDataService)
        {
            _vendorDataService = vendorDataService;
        }

        public void AddVendor(Vendor vendor)
        {
            _vendorDataService.AddVendor(vendor);
        }

        public Vendor? GetById(Guid id)
        {
            return _vendorDataService.GetById(id);
        }

        public Vendor? GetByVendorName(string vendor)
        {
            return _vendorDataService.GetByVendorName(vendor);
        }

        public bool VendorExists(string vendor)
        {
            return _vendorDataService.VendorExists(vendor);
        }

        public void UpdateVendor(Vendor vendor)
        {
            _vendorDataService.UpdateVendor(vendor);
        }

        public void DeleteVendor(string vendorName)
        {
            _vendorDataService.DeleteVendor(vendorName);
        }
        public void DeleteAllVen()
        {
            _vendorDataService.DeleteAllVen();
        }
        public List<Vendor> GetVendors()
        {
            return _vendorDataService.GetVendors();
        }

        public int GetVendorCount()
        {
            return _vendorDataService.GetVendorCount();
        }

    }
}
