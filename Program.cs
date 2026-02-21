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
            Console.WriteLine("Welcome to Vendor + Purchase Management\nAdd | Search | Update | Delete | Exit");
            userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "Add":
                    break;
                case "Search":
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
        static void AddVendor(string vendorName)
        {
            Console.WriteLine($"Added {vendorName}.");
            vendor.Add(vendorName);
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
