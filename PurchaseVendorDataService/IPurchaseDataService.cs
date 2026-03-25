using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseVendorDataService
{
    public interface IPurchaseDataService
    {
        void AddP(Purchase pur);
        Purchase? GetById(Guid id); // foreach a in List<Purchase>, first found instance of Guid id (ProductID) is returned
        Purchase? PurchaseGetByName(string pur);
        Purchase? PurchaseGetByVndr(string pur);
        bool PurchaseExists(string pur); // compare if inputted string vendor returns true when there is an equal to it "=="
        void Update(Purchase pur);
        List<Purchase> GetAllPurchases();
        List<Purchase> PurchaseFromVendors(string ven); // return purchase with specific vendors
        void RemovePurchase(string purName);
        void RemoveAllPurchaseByVen(string purVndr);
        void RemoveAllPur();
        int GetPurchaseCount();
    }
}
