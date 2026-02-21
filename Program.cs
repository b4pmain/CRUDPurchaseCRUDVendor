using System;
// https://github.com/b4pmain/CRUDPurchaseCRUDVendor.git
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
            string vendor;
            Console.WriteLine("Welcome to Vendor + Purchase Management\nAdd | Search | Update | Delete | Exit");
            userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "Add":
                    Console.WriteLine("Vendor Name: ");
                    vendor = Console.ReadLine();
                    AddVendor(vendor);
                    break;
                case "Search":
                    Console.WriteLine("Vendor Name: ");
                    vendor = Console.ReadLine();
                    SearchVendor(vendor);
                    break;
                case "Update":
                    break;
                case "Delete":
                    break;
                case "Exit":
                    OnSession = false;
                    break;
                default:
                    Console.WriteLine("Invalid Choice.");
                    break;
            }
        }

        static void Purchase()
        {

        }
        static void AddVendor(string vendorName) // add vendor
        {
            Console.WriteLine($"Added {vendorName}.");
            vendor.Add(vendorName);
        }

        static void SearchVendor(string vendorName) // vendor search
        {
            Console.WriteLine($"Searching for {vendorName}");
            foreach (var vendorSearch in vendor)
            {
                if (vendorName.Equals(vendorSearch))
                {
                    Console.WriteLine($"N{vendorName} found.");
                }
                else
                {
                    Console.WriteLine($"N{vendorName} not found.");
                }
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
