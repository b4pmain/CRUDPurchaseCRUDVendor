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
        VendorDataService vendorDataService = new VendorDataService();
        PurchaseDataService purchaseDataService = new PurchaseDataService();

        // VENDOR
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

        public bool RemoveVendor(Vendor vendor)
        {
            if (!vendorDataService.VendorExists(vendor.VendorName))
                return false;

            String vendorName = vendor.VendorName;
            vendorDataService.RemoveV(vendorName);
            purchaseDataService.RemoveAllPurchaseByVen(vendorName); // deleting vendor also deletes all of its products
            return true;
        }

        public bool RemoveAllVen()
        {
            vendorDataService.RemoveAllVen();
            return true;
        }
        public bool ChangeInfo(string vendorName, string vendorDescription, string contactPhone, string contactEmail)
        {
            var existing = vendorDataService.GetByVendorName(vendorName);

            if (existing == null)
                return false;

            existing.VendorDescription = vendorDescription;
            existing.ContactPhone = contactPhone;
            existing.ContactEmail = contactEmail;

            vendorDataService.Update(existing);
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

            var purchase = new Purchase
            {
                ProductID = Guid.NewGuid(),
                PurchaseVndr = newPurchase.PurchaseVndr,
                PurchaseName = newPurchase.PurchaseName,
                PurchaseQty = newPurchase.PurchaseQty,
                PurchasePrice = newPurchase.PurchasePrice,
                PurchaseDate = newPurchase.PurchaseDate
            };

            purchaseDataService.AddP(purchase);
            return true;
        }

        public bool ChangePur(string purchaseName, string purchaseVndr, int purchaseQty, double purchasePrice, string purchaseDate)
        {
            var existing = purchaseDataService.PurchaseGetByName(purchaseName);

            if (existing == null)
                return false;

            existing.PurchaseVndr = purchaseVndr;
            existing.PurchaseQty = purchaseQty;
            existing.PurchasePrice = purchasePrice;
            existing.PurchaseDate = purchaseDate;

            purchaseDataService.Update(existing);
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
        public Purchase? PurGetByVendorName(string prchName)
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
        public int PurCount()
        {
            int count = purchaseDataService.GetPurchaseCount();
            return count;
        }

    }
}
