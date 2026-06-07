using PurchaseVendorDataService;
using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PurchaseVendorAppService
{
    public class PurVenAppService
    {
        VendorDataService vendorDataService = new VendorDataService(new VendorDBData()); /* VendorDBData/VendorJsonData/VendorInMemData */
        PurchaseDataService purchaseDataService = new PurchaseDataService(new PurchaseDBData()); /* PurchaseDBData/PurchaseJsonData/PurchaseInMemData */

        // VENDOR
        public bool AddVendor(Vendor newVendor)
        {
            if (vendorDataService.VendorExists(newVendor.VendorName))
                return false;

            var vendor = new Vendor
            {
                VendorID = Guid.NewGuid(),
                VendorName = newVendor.VendorName.ToUpper(),
                VendorDescription = newVendor.VendorDescription,
                ContactPhone = newVendor.ContactPhone,
                ContactEmail = newVendor.ContactEmail
            };

            vendorDataService.AddVendor(vendor);
            return true;
        }

        public bool RemoveVendor(Vendor vendor)
        {
            if (!vendorDataService.VendorExists(vendor.VendorName))
                return false;

            String vendorName = vendor.VendorName;
            vendorDataService.DeleteVendor(vendorName.ToUpper());
            purchaseDataService.RemoveAllPurchaseByVen(vendorName.ToUpper()); // deleting vendor also deletes all of its products
            return true;
        }

        public bool RemoveAllVen()
        {
            vendorDataService.DeleteAllVen();
            return true;
        }
        public bool ChangeInfo(Vendor vendor)
        {
            vendorDataService.UpdateVendor(vendor);
            return true;
        }

        public bool venIsExisting(string vendorName)
        {
            var venSearch = vendorDataService.VendorExists(vendorName);
            if (venSearch == null)
                return false;

            return venSearch;
        }

        public List<Vendor> GetVendors()
        {
            return vendorDataService.GetVendors(); 
        }

        public Vendor? GetVendor(Guid VendorID)
        {
            return vendorDataService.GetById(VendorID);
        }


        public Vendor? GetVendorByName(string VendorName)
        {
            return vendorDataService.GetByVendorName(VendorName);
        }

        public int VenCount()
        {
            int count = vendorDataService.GetVendorCount();
            return count;
        }

        // PURCHASE =============================================================================== PURCHASE //
        // PURCHASE =============================================================================== PURCHASE //
        // PURCHASE =============================================================================== PURCHASE //

        public bool AddPurchase(Purchase newPurchase)
        {
            if (purchaseDataService.PurchaseExists(newPurchase.PurchaseName))
                return false;

            purchaseDataService.AddPurchase(newPurchase);
            return true;
        }

        public bool ChangePur(Purchase purchase)
        {
            purchaseDataService.UpdatePurchase(purchase);
            return true;
        }

        public bool RemovePurchase(Purchase pur)
        {
            if (!purchaseDataService.PurchaseExists(pur.PurchaseName))
                return false;

            String purchaseName = pur.PurchaseName;
            purchaseDataService.RemovePurchase(purchaseName);
            return true;
        }

        public bool prchIsExisting(string prchName)
        {
            var purSearch = purchaseDataService.PurchaseExists(prchName);
            if (purSearch == null)
                return false;

            return purSearch;
        }

        public bool RemoveAllPur()
        {
            purchaseDataService.RemoveAllPur();
            return true;
        }

        public List<Purchase> GetAllPurchases()
        {
            return purchaseDataService.GetAllPurchases();
        }
        public Purchase? PurGetByPurchaseName(string prchName)
        {
            var purchase = GetAllPurchases().FirstOrDefault(a => a.PurchaseName == prchName);

            if (purchase == null)
                return null; // yeesh

            return purchaseDataService.PurchaseGetByVndr(purchase.PurchaseVndr);
        }
        public List<Purchase> PurchaseFromVendor(string venName)
        {
            return purchaseDataService.PurchaseFromVendors(venName);
        }

        public Purchase? PurGetById(Guid id)
        {
            return purchaseDataService.GetById(id);
        }
        public int PurCount()
        {
            int count = purchaseDataService.GetPurchaseCount();
            return count;
        }

    }
}
