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

        public void AddV(Vendor vendor)
        {
            _vendorDataService.AddV(vendor);
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

        public void Update(Vendor vendor)
        {
            _vendorDataService.Update(vendor);
        }

        public void RemoveV(string vendorName)
        {
            _vendorDataService.RemoveV(vendorName);
        }
        public void RemoveAllVen()
        {
            _vendorDataService.RemoveAllVen();
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
