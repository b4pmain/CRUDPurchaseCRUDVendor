using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PurchaseVendorDataService
{
    public class PurchaseDataService
    {
        IPurchaseDataService _purchaseDataService;
        public PurchaseDataService(IPurchaseDataService purchaseDataService)
        {
            _purchaseDataService = purchaseDataService;
        }

        public void AddPurchase(Purchase pur)
        {
            _purchaseDataService.AddPurchase(pur);
        }

        public Purchase? GetById(Guid id) // foreach a in List<Purchase>, first found instance of Guid id (ProductID) is returned
        {
            return _purchaseDataService.GetById(id);
        }

        public Purchase? PurchaseGetByName(string pur)
        {
            return _purchaseDataService.PurchaseGetByName(pur);
        }
        public Purchase? PurchaseGetByVndr(string pur)
        {
            return _purchaseDataService.PurchaseGetByVndr(pur);
        }

        public bool PurchaseExists(string pur) // compare if inputted string vendor returns true when there is an equal to it "=="
        {
            return _purchaseDataService.PurchaseExists(pur);
        }

        public void UpdatePurchase(Purchase pur)
        {
            _purchaseDataService.UpdatePurchase(pur);
        }

        public List<Purchase> GetAllPurchases()
        {
            return _purchaseDataService.GetAllPurchases();
        }
        public List<Purchase> PurchaseFromVendors(string ven) // return purchase with specific vendors
        {
            return _purchaseDataService.PurchaseFromVendors(ven);
        }
        public void RemovePurchase(string purName)
        {
            _purchaseDataService.DeletePurchase(purName);
        }

        public void RemoveAllPurchaseByVen(string purVndr)
        {
            _purchaseDataService.DeleteAllPurchaseByVen(purVndr);
        }

        public void RemoveAllPur()
        {
            _purchaseDataService.DeleteAllPur();
        }

        public int GetPurchaseCount()
        {
            return _purchaseDataService.GetPurchaseCount();
        }

    }
}
