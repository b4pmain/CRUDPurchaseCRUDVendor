using Microsoft.Data.SqlClient;
using PurchaseVendorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;

// already implemented since March 25 Commit

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

        public Purchase? GetById(Guid id)
        {
            var selectStatement = "SELECT ProductID, PurchaseVndr, PurchaseName, PurchaseQty, PurchasePrice, PurchaseDate FROM tbl_purchase WHERE ProductID = @ProductID";

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
            var selectStatement = "SELECT ProductID, PurchaseVndr, PurchaseName, PurchaseQty, PurchasePrice, PurchaseDate FROM tbl_purchase WHERE PurchaseName = @PurchaseName";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@PurchaseName", pur);

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
        public Purchase? PurchaseGetByVndr(string pur)
        {
            var selectStatement = "SELECT ProductID, PurchaseVndr, PurchaseName, PurchaseQty, PurchasePrice, PurchaseDate FROM tbl_purchase WHERE PurchaseVndr = @PurchaseVndr";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@PurchaseVndr", pur);

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

        public bool PurchaseExists(string pur)
        {
            var selectStatement = "SELECT ProductID, PurchaseVndr, PurchaseName, PurchaseQty, PurchasePrice, PurchaseDate FROM tbl_purchase WHERE PurchaseName = @PurchaseName";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@PurchaseName", pur);

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var purch = new Purchase();

            while (reader.Read())
            {
                purch.ProductID = Guid.Parse(reader["ProductID"].ToString());
                purch.PurchaseVndr = reader["PurchaseVndr"].ToString();
                purch.PurchaseName = reader["PurchaseName"].ToString();
                purch.PurchaseQty = int.Parse(reader["PurchaseQty"].ToString());
                purch.PurchasePrice = double.Parse(reader["PurchasePrice"].ToString());
                purch.PurchaseDate = reader["PurchaseDate"].ToString();
            }

            sqlConnection.Close();

            return purch.PurchaseName != null;
        }

        public void Update(Purchase pur)
        {
            sqlConnection.Open();

            var updateStatement = $"UPDATE tbl_purchase SET PurchaseVndr = @PurchaseVndr, PurchaseName = @PurchaseName, PurchaseQty = @PurchaseQty, PurchasePrice = @PurchasePrice, PurchaseDate = @PurchaseDate WHERE ProductID = @ProductID";

            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);

            updateCommand.Parameters.AddWithValue("@PurchaseVndr", pur.PurchaseVndr);
            updateCommand.Parameters.AddWithValue("@PurchaseName", pur.PurchaseName);
            updateCommand.Parameters.AddWithValue("@PurchaseQty", pur.PurchaseQty);
            updateCommand.Parameters.AddWithValue("@PurchasePrice", pur.PurchasePrice);
            updateCommand.Parameters.AddWithValue("@PurchaseDate", pur.PurchaseDate);
            updateCommand.Parameters.AddWithValue("@ProductID", pur.ProductID);
            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public List<Purchase> GetAllPurchases()
        {
            var selectStatement = "SELECT ProductID, PurchaseVndr, PurchaseName, PurchaseQty, PurchasePrice, PurchaseDate FROM tbl_purchase";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var purch = new List<Purchase>();

            while (reader.Read())
            {
                // convert to string deserialization

                Purchase purchase = new Purchase();
                purchase.ProductID = Guid.Parse(reader["ProductID"].ToString());
                purchase.PurchaseVndr = reader["PurchaseVndr"].ToString();
                purchase.PurchaseName = reader["PurchaseName"].ToString();
                purchase.PurchaseQty = int.Parse(reader["PurchaseQty"].ToString());
                purchase.PurchasePrice = double.Parse(reader["PurchasePrice"].ToString());
                purchase.PurchaseDate = reader["PurchaseDate"].ToString();

                purch.Add(purchase);
            }

            sqlConnection.Close();
            return purch;
        }
        public List<Purchase> PurchaseFromVendors(string ven) // return purchase with specific vendors
        {
            var selectStatement = "SELECT ProductID, PurchaseVndr, PurchaseName, PurchaseQty, PurchasePrice, PurchaseDate FROM tbl_purchase WHERE PurchaseVndr = @PurchaseVndr";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@PurchaseVndr", ven);

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var purch = new List<Purchase>();

            while (reader.Read())
            {
                // convert to string deserialization

                Purchase purchase = new Purchase();
                purchase.ProductID = Guid.Parse(reader["ProductID"].ToString());
                purchase.PurchaseVndr = reader["PurchaseVndr"].ToString();
                purchase.PurchaseName = reader["PurchaseName"].ToString();
                purchase.PurchaseQty = int.Parse(reader["PurchaseQty"].ToString());
                purchase.PurchasePrice = double.Parse(reader["PurchasePrice"].ToString());
                purchase.PurchaseDate = reader["PurchaseDate"].ToString();

                purch.Add(purchase);
            }

            sqlConnection.Close();
            return purch;
        }
        public void RemovePurchase(string purName)
        {
            sqlConnection.Open();

            var deleteStatement = "DELETE FROM tbl_purchase WHERE PurchaseName = @PurchaseName";

            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);

            deleteCommand.Parameters.AddWithValue("@PurchaseName", purName);
            deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public void RemoveAllPurchaseByVen(string purVndr)
        {
            sqlConnection.Open();

            var deleteStatement = "DELETE FROM tbl_purchase WHERE PurchaseVndr = @PurchaseVndr";

            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);

            deleteCommand.Parameters.AddWithValue("@PurchaseVndr", purVndr);
            deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public void RemoveAllPur()
        {
            sqlConnection.Open();
            var truncateStatement = "TRUNCATE TABLE tbl_purchase";

            SqlCommand truncateCommand = new SqlCommand(truncateStatement, sqlConnection);
            truncateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public int GetPurchaseCount()
        {
            sqlConnection.Open();

            var query = "SELECT COUNT(*) FROM tbl_purchase";

            SqlCommand queryCommand = new SqlCommand(query, sqlConnection);
            var count = queryCommand.ExecuteScalar();

            sqlConnection.Close();
            return count != null ? Convert.ToInt32(count) : 0;
        }

    }
}
