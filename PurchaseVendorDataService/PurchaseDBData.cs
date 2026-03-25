using Microsoft.Data.SqlClient;
using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace PurchaseVendorDataService
{
    public class PurchaseDBData : IPurchaseDataService
    {
        private string connectionString =
            "Data Source = localhost\\SQLEXPRESS; Initial Catalog = PurVenDB; Integrated Security = True; TrustServerCertificate=True;";
        private SqlConnection sqlConnection;
        public PurchaseDBData()
        {
            sqlConnection = new SqlConnection(connectionString);

            AddSeedPurch();
        }
        private void AddSeedPurch()
        {
            var existing = GetAllPurchases();

            if (existing.Count == 0)
            {
                Purchase cofOneNescafe = new Purchase
                {
                    ProductID = Guid.NewGuid(),
                    PurchaseVndr = "NESCAFE",
                    PurchaseName = "Nescafe 3 in 1 Pack",
                    PurchaseQty = 10,
                    PurchasePrice = 150.00,
                    PurchaseDate = "2026-03-09"
                };
                Purchase cofTwoNescafe = new Purchase
                {
                    ProductID = Guid.NewGuid(),
                    PurchaseVndr = "NESCAFE",
                    PurchaseName = "Nescafe Creamy Pack",
                    PurchaseQty = 20,
                    PurchasePrice = 300.00,
                    PurchaseDate = "2026-03-09"
                };

                AddP(cofOneNescafe);
                AddP(cofTwoNescafe);
            }
        }
        public void AddP(Purchase pur)
        {
            var insertStatement = "INSERT INTO tbl_purchase VALUES (@ProductID, @PurchaseVndr, @PurchaseName, @PurchaseQty, @PurchasePrice, @PurchaseDate)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@ProductID", pur.ProductID);
            insertCommand.Parameters.AddWithValue("@PurchaseVndr", pur.PurchaseVndr);
            insertCommand.Parameters.AddWithValue("@PurchaseName", pur.PurchaseName);
            insertCommand.Parameters.AddWithValue("@PurchaseQty", pur.PurchaseQty);
            insertCommand.Parameters.AddWithValue("@PurchasePrice", pur.PurchasePrice);
            insertCommand.Parameters.AddWithValue("@PurchaseDate", pur.PurchaseDate);

            sqlConnection.Open();

            insertCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public Purchase? GetById(Guid id) // foreach a in List<Purchase>, first found instance of Guid id (ProductID) is returned
        {
            var selectStatement = "SELECT tbl_purchase VALUES (ProductID, PurchaseVndr, PurchaseName, PurchaseQty, PurchasePrice, PurchaseDate FROM tbl_purchase WHERE ProductID = @ProductID";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@ProductID", id.ToString());

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var purch = new Purchase();

            while (reader.Read())
            {
                // convert to string deserialization
                purch.ProductID = Guid.Parse(reader["ProductID"].ToString());
                purch.PurchaseVndr = reader["PurchaseVndr"].ToString();
                purch.PurchaseName = reader["PurchaseName"].ToString();
                purch.PurchaseQty = int.Parse(reader["PurchaseQty"].ToString());
                purch.PurchasePrice = double.Parse(reader["PurchasePrice"].ToString());
                purch.PurchaseDate = reader["PurchaseDate"].ToString();
            }

            sqlConnection.Close();
            return purch;
        }

        public Purchase? PurchaseGetByName(string pur)
        {
            
            return purJson.FirstOrDefault(a => a.PurchaseName == pur);
        }
        public Purchase? PurchaseGetByVndr(string pur)
        {
            
            return purJson.FirstOrDefault(a => a.PurchaseVndr == pur);
        }

        public bool PurchaseExists(string pur) // compare if inputted string vendor returns true when there is an equal to it "=="
        {
            
            return purJson.Any(a => a.PurchaseName == pur);
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

        public List<Purchase> GetAllPurchases()
        {
            
            return purJson;
        }
        public List<Purchase> PurchaseFromVendors(string ven) // return purchase with specific vendors
        {
            
            return purJson
                .Where(a => a.PurchaseVndr == ven)
                .ToList();
        }
        public void RemovePurchase(string purName)
        {
            
            purJson.Remove(purJson.First(a => a.PurchaseName == purName));
            
        }

        public void RemoveAllPurchaseByVen(string purVndr)
        {
            
            purJson.RemoveAll(a => a.PurchaseVndr == purVndr);
            
        }

        public void RemoveAllPur()
        {
            
            purJson.Clear();
            
        }

        public int GetPurchaseCount()
        {
            
            int count = purJson.Count;
            return count;
        }

    }
}
