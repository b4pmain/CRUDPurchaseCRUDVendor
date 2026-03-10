using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseVendorDataService
{
    public class PurchaseDataService
    {
        public List<Purchase> purchase = new List<Purchase>();

        public PurchaseDataService()
        {
            Purchase cofOneNescafe = new Purchase
            {
                ProductID = Guid.NewGuid(),
                PurchaseVndr = "NESCAFE",
                PurchaseName = "Nescafe 3 in 1 Pack",
                PurchaseQty = 10,
                PurchasePrice = 150.00f,
                PurchaseDate = "2026-03-09"
            };
            Purchase cofTwoNescafe = new Purchase
            {
                ProductID = Guid.NewGuid(),
                PurchaseVndr = "NESCAFE",
                PurchaseName = "Nescafe Creamy Pack",
                PurchaseQty = 20,
                PurchasePrice = 300.00f,
                PurchaseDate = "2026-03-09"
            };

            purchase.Add(cofOneNescafe);
            purchase.Add(cofTwoNescafe);


        }

        public void AddP(Purchase pur)
        {
            purchase.Add(pur);
        }

        public Purchase? GetById(Guid id) // foreach a in List<Purchase>, first found instance of Guid id (ProductID) is returned
        {
            return purchase.FirstOrDefault(a => a.ProductID == id);
        }

        public Purchase? GetByVendorName(string pur) // foreach a in List<Purchase>, first found instance of said query (pur) is returned
        {
            return purchase.FirstOrDefault(a => a.PurchaseName == pur);
        }

        public bool PurchaseExists(string pur) // compare if inputted string vendor returns true when there is an equal to it "=="
        {
            return purchase.Any(a => a.PurchaseName == pur);
        }

        public void Update(Purchase pur)
        {
            var existing = GetById(pur.ProductID);
            if (existing != null)
            {
                existing.PurchaseVndr = pur.PurchaseVndr;
                existing.PurchaseName = pur.PurchaseName;
                existing.PurchaseQty = pur.PurchaseQty;
                existing.PurchasePrice = pur.PurchasePrice;
                existing.PurchaseDate = pur.PurchaseDate;
            }
        }

        public List<Purchase> GetPurchases()
        {
            return purchase;
        }

    }
}
