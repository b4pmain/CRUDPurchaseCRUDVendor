using PurchaseVendorModels;
using Microsoft.Data.SqlClient;
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
    public class VendorDBData : IVendorDataService
    {
        private string connectionString = 
            "Data Source = localhost\\SQLEXPRESS; Initial Catalog = PurVenDB; Integrated Security = True; TrustServerCertificate=True;";
        private SqlConnection sqlConnection;
        public VendorDBData()
        {
            sqlConnection = new SqlConnection(connectionString);

            AddSeedVen();
        }

        private void AddSeedVen()
        {
            var existing = GetVendors();

            if (existing.Count == 0)
            {
                Vendor nescafe = new Vendor // populate 1
                {
                    VendorID = Guid.NewGuid(),
                    VendorName = "NESCAFE",
                    VendorDescription = "Coffee Maker Brand",
                    ContactPhone = "0912345678",
                    ContactEmail = "account@nescafe.com.ph"
                };

                Vendor rebisco = new Vendor // populate 2
                {
                    VendorID = Guid.NewGuid(),
                    VendorName = "REBISCO",
                    VendorDescription = "Maker of Biscuits",
                    ContactPhone = "0912345678",
                    ContactEmail = "account@rebisco.com.ph"
                };

                Vendor sanmig = new Vendor // populate 3
                {
                    VendorID = Guid.NewGuid(),
                    VendorName = "SAN MIGUEL CORPORATION",
                    VendorDescription = "Beverage Brand",
                    ContactPhone = "0912345678",
                    ContactEmail = "account@sanmigcorp.com.ph"
                };

                Vendor nestle = new Vendor // populate 4
                {
                    VendorID = Guid.NewGuid(),
                    VendorName = "NESTLE",
                    VendorDescription = "Variety Goods Maker",
                    ContactPhone = "0912345678",
                    ContactEmail = "account@nestle.com.ph"
                };

                Vendor oishi = new Vendor // populate 5
                {
                    VendorID = Guid.NewGuid(),
                    VendorName = "OISHI",
                    VendorDescription = "Chip Making Brand",
                    ContactPhone = "0912345678",
                    ContactEmail = "account@oishi.com.ph"
                };

                AddV(nescafe);
                AddV(rebisco);
                AddV(sanmig);
                AddV(nestle);
                AddV(oishi);
            }
        }

        public void AddV(Vendor vendor)
        {
            var insertStatement = "INSERT INTO tbl_vendor VALUES (@VendorId, @VendorName, @VendorDescription, @ContactPhone, @ContactEmail)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@VendorID", vendor.VendorID);
            insertCommand.Parameters.AddWithValue("@VendorName", vendor.VendorName);
            insertCommand.Parameters.AddWithValue("@VendorDescription", vendor.VendorDescription);
            insertCommand.Parameters.AddWithValue("@ContactPhone", vendor.ContactPhone);
            insertCommand.Parameters.AddWithValue("@ContactEmail", vendor.ContactEmail);

            sqlConnection.Open();

            insertCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public Vendor? GetById(Guid id)
        {
            var selectStatement = "SELECT VendorID, VendorName, VendorDescription, ContactPhone, ContactEmail FROM tbl_vendor WHERE VendorID = @VendorID";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@VendorID", id.ToString());

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var vendor = new Vendor();

            while (reader.Read())
            {
                // convert to string deserialization
                vendor.VendorID = Guid.Parse(reader["VendorID"].ToString());
                vendor.VendorName = reader["VendorName"].ToString();
                vendor.VendorDescription = reader["VendorDescription"].ToString();
                vendor.ContactPhone = reader["ContactPhone"].ToString();
                vendor.ContactEmail = reader["ContactEmail"].ToString();

            }

            sqlConnection.Close();
            return vendor;
        }

        public Vendor? GetByVendorName(string vendorName) // foreach a in List<Vendor>, first found instance of said query (vendor) is returned
        {
            var selectStatement = "SELECT VendorID, VendorName, VendorDescription, ContactPhone, ContactEmail FROM tbl_vendor WHERE VendorName = @VendorName";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@VendorName", vendorName);

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var vendor = new Vendor();

            while (reader.Read())
            {
                // convert to string deserialization
                vendor.VendorID = Guid.Parse(reader["VendorID"].ToString());
                vendor.VendorName = reader["VendorName"].ToString();
                vendor.VendorDescription = reader["VendorDescription"].ToString();
                vendor.ContactPhone = reader["ContactPhone"].ToString();
                vendor.ContactEmail = reader["ContactEmail"].ToString();

            }

            sqlConnection.Close();

            return vendor;
        }

        public bool VendorExists(string vendorName)
        {
            var selectStatement = "SELECT VendorID, VendorName, VendorDescription, ContactPhone, ContactEmail FROM tbl_vendor WHERE VendorName = @VendorName";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@VendorName", vendorName);

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var vendor = new Vendor();

            while (reader.Read())
            {
                // convert to string deserialization
                vendor.VendorID = Guid.Parse(reader["VendorID"].ToString());
                vendor.VendorName = reader["VendorName"].ToString();
                vendor.VendorDescription = reader["VendorDescription"].ToString();
                vendor.ContactPhone = reader["ContactPhone"].ToString();
                vendor.ContactEmail = reader["ContactEmail"].ToString();
            }

            sqlConnection.Close();

            return vendor.VendorName != null;
        }

        public void Update(Vendor vendor)
        {
            sqlConnection.Open();

            var updateStatement = $"UPDATE tbl_vendor SET VendorName = @VendorName, VendorDescription = @VendorDescription, ContactPhone = @ContactPhone, ContactEmail = @ContactEmail WHERE VendorID = @VendorID";

            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);

            updateCommand.Parameters.AddWithValue("@VendorName", vendor.VendorName);
            updateCommand.Parameters.AddWithValue("@VendorDescription", vendor.VendorDescription);
            updateCommand.Parameters.AddWithValue("@ContactPhone", vendor.ContactPhone);
            updateCommand.Parameters.AddWithValue("@ContactEmail", vendor.ContactEmail);
            updateCommand.Parameters.AddWithValue("@VendorID", vendor.VendorID);
            updateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public void RemoveV(string vendorName) // remove vendor object from the list by name
        {
            sqlConnection.Open();

            var deleteStatement = "DELETE FROM tbl_vendor WHERE VendorName = @VendorName";

            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);

            deleteCommand.Parameters.AddWithValue("@VendorName", vendorName);
            deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        public void RemoveAllVen()
        {
            sqlConnection.Open();
            var truncateStatement = "TRUNCATE TABLE tbl_vendor";

            SqlCommand truncateCommand = new SqlCommand(truncateStatement, sqlConnection);
            truncateCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        public List<Vendor> GetVendors()
        {
            var selectStatement = "SELECT VendorID, VendorName, VendorDescription, ContactPhone, ContactEmail FROM tbl_vendor";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = selectCommand.ExecuteReader();

            var vendors = new List<Vendor>();

            while (reader.Read())
            {
                // convert to string deserialization

                Vendor vendor = new Vendor();
                vendor.VendorID = Guid.Parse(reader["VendorID"].ToString());
                vendor.VendorName = reader["VendorName"].ToString();
                vendor.VendorDescription = reader["VendorDescription"].ToString();
                vendor.ContactPhone = reader["ContactPhone"].ToString();
                vendor.ContactEmail = reader["ContactEmail"].ToString();

                vendors.Add(vendor);
            }

            sqlConnection.Close();
            return vendors;
        }

        public int GetVendorCount()
        {
            sqlConnection.Open();

            var query = "SELECT COUNT(*) FROM tbl_vendor";

            SqlCommand queryCommand = new SqlCommand(query, sqlConnection);
            var count = queryCommand.ExecuteScalar();

            sqlConnection.Close();
            return count != null ? Convert.ToInt32(count) : 0;
        }

    }
}
