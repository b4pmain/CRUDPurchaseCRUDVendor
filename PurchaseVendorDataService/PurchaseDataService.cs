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
    }
}
