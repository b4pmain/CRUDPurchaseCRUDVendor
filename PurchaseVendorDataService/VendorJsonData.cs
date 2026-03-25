using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace PurchaseVendorDataService
{
    public class VendorJsonData : IVendorDataService
    {
        private List<Vendor> venJson = new List<Vendor>();
        private string _jsonFile;
        public VendorJsonData()
        {
            _jsonFile = $"{AppDomain.CurrentDomain.BaseDirectory}/VendorJsonData.json";

            SeedJsonVen();
        }

        private void SeedJsonVen()
        {
            RetrieveFromJsonVen();

            if (venJson.Count <= 0)
            {
                venJson.Add(new Vendor // populate 1
                {
                    VendorID = Guid.NewGuid(),
                    VendorName = "NESCAFE",
                    VendorDescription = "Coffee Maker Brand",
                    ContactPhone = "0912345678",
                    ContactEmail = "account@nescafe.com.ph"
                });

                venJson.Add(new Vendor // populate 2
                {
                    VendorID = Guid.NewGuid(),
                    VendorName = "REBISCO",
                    VendorDescription = "Maker of Biscuits",
                    ContactPhone = "0912345678",
                    ContactEmail = "account@rebisco.com.ph"
                });

                venJson.Add(new Vendor // populate 3
                {
                    VendorID = Guid.NewGuid(),
                    VendorName = "SAN MIGUEL CORPORATION",
                    VendorDescription = "Beverage Brand",
                    ContactPhone = "0912345678",
                    ContactEmail = "account@sanmigcorp.com.ph"
                });

                venJson.Add(new Vendor // populate 4
                {
                    VendorID = Guid.NewGuid(),
                    VendorName = "NESTLE",
                    VendorDescription = "Variety Goods Maker",
                    ContactPhone = "0912345678",
                    ContactEmail = "account@nestle.com.ph"
                });

                venJson.Add(new Vendor // populate 5
                {
                    VendorID = Guid.NewGuid(),
                    VendorName = "OISHI",
                    VendorDescription = "Chip Making Brand",
                    ContactPhone = "0912345678",
                    ContactEmail = "account@oishi.com.ph"
                });

                SaveToJsonVen();
            }
        }

        private void SaveToJsonVen()
        {
            using (var outputStream = File.OpenWrite(_jsonFile))
            {
                JsonSerializer.Serialize<List<Vendor>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , venJson);
            }
        }

        private void RetrieveFromJsonVen()
        {
            using (var jsonFileReader = File.OpenText(_jsonFile))
            {
                venJson = JsonSerializer.Deserialize<List<Vendor>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true })
                    .ToList();
            }
        }

        public void AddV(Vendor vendor)
        {
            venJson.Add(vendor);
            SaveToJsonVen();
        }

        public Vendor? GetById(Guid id) // foreach a in List<Vendor>, first found instance of Guid id (VendorID) is returned
        {
            RetrieveFromJsonVen(); // read first in file
            return venJson.FirstOrDefault(a => a.VendorID == id);
        }

        public Vendor? GetByVendorName(string vendor) // foreach a in List<Vendor>, first found instance of said query (vendor) is returned
        {
            RetrieveFromJsonVen();
            return venJson.FirstOrDefault(a => a.VendorName == vendor);
        }

        public bool VendorExists(string vendor) // compare if inputted string vendor returns true when there is an equal to it "=="
        {
            RetrieveFromJsonVen();
            return venJson.Any(a => a.VendorName == vendor);
        }

        public void Update(Vendor vendor)
        {
            RetrieveFromJsonVen();

            var existing = GetById(vendor.VendorID);
            if (existing != null)
            {
                existing.VendorName = vendor.VendorName;
                existing.VendorDescription = vendor.VendorDescription;
                existing.ContactPhone = vendor.ContactPhone;
                existing.ContactEmail = vendor.ContactEmail;
            }

            SaveToJsonVen();
        }

        public void RemoveV(string vendorName) // remove vendor object from the list by name
        {
            RetrieveFromJsonVen();
            venJson.Remove(venJson.First(a => a.VendorName == vendorName));
            SaveToJsonVen();
        }
        public void RemoveAllVen()
        {
            RetrieveFromJsonVen();
            venJson.Clear();
            SaveToJsonVen();
        }
        public List<Vendor> GetVendors()
        {
            RetrieveFromJsonVen();
            return venJson;
        }

        public int GetVendorCount()
        {
            RetrieveFromJsonVen();
            int count = venJson.Count;
            return count;
        }

    }
}
