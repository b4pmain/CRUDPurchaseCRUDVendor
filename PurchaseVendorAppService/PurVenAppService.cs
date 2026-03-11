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

        public bool ChangeInfo(string vendorName, string vendorDescription, string contactPhone, string contactEmail)
        {
            var existing = vendorDataService.GetByVendorName(vendorName);

            if (existing != null)
                return false;

            existing.VendorDescription = vendorDescription;
            existing.ContactPhone = contactPhone;
            existing.ContactEmail = contactEmail;

            vendorDataService.Update(existing);
            return false;
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
            int count = purchaseDataService.GetPurchaseCount();
            return count;
        }

        // PURCHASE

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

        public bool RemovePurchase(Purchase pur)
        {
            if (!purchaseDataService.PurchaseExists(pur.PurchaseName))
                return false;

            String purchaseName = pur.PurchaseName;
            purchaseDataService.RemovePurchase(purchaseName);
            return true;
        }

        public bool RemoveAllPurchase(Purchase pur)
        {
            if (!purchaseDataService.PurchaseExists(pur.PurchaseVndr))
                return false;

            String purchaseVndr = pur.PurchaseVndr;
            purchaseDataService.RemoveAllPurchaseByVen(purchaseVndr);
            return true;
        }

        public List<Purchase> GetAllPurchases()
        {
            return purchaseDataService.GetAllPurchases();
        }

        public List<Purchase> PurchaseFromVendor(Vendor vendor)
        {
            if (!vendorDataService.VendorExists(vendor.VendorName))
            {
                List<Purchase> emptyList = new List<Purchase>();
                return emptyList;
            }
            string purVendor = vendor.VendorName;
            return purchaseDataService.PurchaseFromVendors(purVendor);
        }
        public int PurCount()
        {
            int count = purchaseDataService.GetPurchaseCount();
            return count;
        }

    }
}
