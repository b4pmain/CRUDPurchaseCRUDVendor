using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace PurchaseVendorDataService
{
    public class PurchaseJsonData : IPurchaseDataService
    {
        private List<Purchase> purJson = new List<Purchase>();
        private string _jsonFile;
        public PurchaseJsonData()
        {
            _jsonFile = $"{AppDomain.CurrentDomain.BaseDirectory}/PurchaseJsonData.json";

            SeedPurchaseJson();
        }
        private void SeedPurchaseJson()
        {
            RetrieveFromJsonPur();

            if (purJson.Count <= 0)
            {
                purJson.Add(new Purchase
                {
                    ProductID = Guid.NewGuid(),
                    PurchaseVndr = "NESCAFE",
                    PurchaseName = "Nescafe 3 in 1 Pack",
                    PurchaseQty = 10,
                    PurchasePrice = 150.00,
                    PurchaseDate = "2026-03-09"
                });

                purJson.Add(new Purchase
                {
                    ProductID = Guid.NewGuid(),
                    PurchaseVndr = "NESCAFE",
                    PurchaseName = "Nescafe Creamy Pack",
                    PurchaseQty = 20,
                    PurchasePrice = 300.00,
                    PurchaseDate = "2026-03-09"
                });

                SaveToJsonPur();
            }
        }
        private void SaveToJsonPur()
        {
            using (var outputStream = File.OpenWrite(_jsonFile))
            {
                JsonSerializer.Serialize<List<Purchase>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , purJson);
            }
        }
        private void RetrieveFromJsonPur()
        {
            using (var jsonFileReader = File.OpenText(_jsonFile))
            {
                purJson = JsonSerializer.Deserialize<List<Purchase>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }
        public void AddP(Purchase pur)
        {
            purJson.Add(pur);
            SaveToJsonPur();
        }

        public Purchase? GetById(Guid id) // foreach a in List<Purchase>, first found instance of Guid id (ProductID) is returned
        {
            RetrieveFromJsonPur();
            return purJson.FirstOrDefault(a => a.ProductID == id);
        }

        public Purchase? PurchaseGetByName(string pur)
        {
            RetrieveFromJsonPur();
            return purJson.FirstOrDefault(a => a.PurchaseName == pur);
        }
        public Purchase? PurchaseGetByVndr(string pur)
        {
            RetrieveFromJsonPur();
            return purJson.FirstOrDefault(a => a.PurchaseVndr == pur);
        }

        public bool PurchaseExists(string pur) // compare if inputted string vendor returns true when there is an equal to it "=="
        {
            RetrieveFromJsonPur();
            return purJson.Any(a => a.PurchaseName == pur);
        }

        public void Update(Purchase pur)
        {
            RetrieveFromJsonPur();

            var existing = GetById(pur.ProductID);
            if (existing != null)
            {
                existing.PurchaseVndr = pur.PurchaseVndr;
                existing.PurchaseName = pur.PurchaseName;
                existing.PurchaseQty = pur.PurchaseQty;
                existing.PurchasePrice = pur.PurchasePrice;
                existing.PurchaseDate = pur.PurchaseDate;
            }

            SaveToJsonPur();
        }

        public List<Purchase> GetAllPurchases()
        {
            RetrieveFromJsonPur();
            return purJson;
        }
        public List<Purchase> PurchaseFromVendors(string ven) // return purchase with specific vendors
        {
            RetrieveFromJsonPur();
            return purJson
                .Where(a => a.PurchaseVndr == ven)
                .ToList();
        }
        public void RemovePurchase(string purName)
        {
            RetrieveFromJsonPur();
            purJson.Remove(purJson.First(a => a.PurchaseName == purName));
            SaveToJsonPur();
        }

        public void RemoveAllPurchaseByVen(string purVndr)
        {
            RetrieveFromJsonPur();
            purJson.RemoveAll(a => a.PurchaseVndr == purVndr);
            SaveToJsonPur();
        }

        public void RemoveAllPur()
        {
            RetrieveFromJsonPur();
            purJson.Clear();
            SaveToJsonPur();
        }

        public int GetPurchaseCount()
        {
            RetrieveFromJsonPur();
            int count = purJson.Count;
            return count;
        }

    }
}
