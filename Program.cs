using System;
namespace CRUDPurchaseCRUDVendor
{
    internal class Program
    {
        static List<string> purchase = new List<string>();
        static List<string> vendor = new List<string>();
        static bool OnSession = true;
        static void Main(string[] args)
        {

            Vendors();
            while (OnSession)
            {
                Menu();
            }
            
        }

        static void Menu()
        {
            string userChoice;
            Console.WriteLine("");
            userChoice = Console.ReadLine();
            switch (userChoice.ToLower())
            {
                case "vendor":
                    vendorMenu();
                    break;
                case "purchase":
                    purchaseMenu();
                    break;
                default:
                    break;
            }
        }
        
        static void vendorMenu()
        {
            string userChoice;
            string vendor;
            Console.WriteLine("Welcome to Vendor Management\nAdd | Search | Update | Delete | Exit");
            userChoice = Console.ReadLine();
            switch (userChoice.ToLower())
            {
                case "add":
                    Console.WriteLine("Vendor Name: ");
                    vendor = Console.ReadLine();
                    AddVendor(vendor);
                    break;
                case "search":
                    Console.WriteLine("Vendor Name: ");
                    vendor = Console.ReadLine();
                    SearchVendor(vendor);
                    break;
                case "update":
                    Console.WriteLine("Choose Vendor: ");
                    vendor = Console.ReadLine();
                    // Update
                    break;
                case "delete":
                    break;
                case "exit":
                    OnSession = false;
                    break;
                default:
                    Console.WriteLine("Invalid Choice.");
                    break;
            }
        }
        static void purchaseMenu()
        {
            // yes
        }

        static void AddVendor(string vendorName) // add vendor
        {
            Console.WriteLine($"Added {vendorName}.");
            vendor.Add(vendorName);
        }

        static void SearchVendor(string vendorName) // vendor search
        {
            bool hasFound = false;

            Console.WriteLine($"Searching for {vendorName}");
            foreach (var vendorSearch in vendor)
            {
                if (vendorName.Equals(vendorSearch))
                {
                    hasFound = true;
                    break;
                }
                else
                {
                    hasFound = false;
                    continue;
                }
            }

            if (hasFound)
            {
                Console.WriteLine($"{vendorName} found.");
            }
            else
            {
                Console.WriteLine($"{vendorName} not found.");
            }

        }

        static void Vendors()
        {
            vendor.Add("Nescafe");
            vendor.Add("San Miguel Corporation");
            vendor.Add("Rebisco");
            vendor.Add("Nestle");
            vendor.Add("Oishi");
        }

    }
}
