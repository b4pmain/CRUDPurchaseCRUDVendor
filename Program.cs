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

            Vendors(); // initialize vendors
            while (OnSession)
            {
                Menu();
            }
            
        }

        static void Menu()
        {
            OnSession = true;

            string userChoice;
            Console.WriteLine("Welcome to Purchases and Vendor Management!\n[Purchase Management] | [Vendor Management] | [Exit]");
            userChoice = Console.ReadLine();
            switch (userChoice.ToLower())
            {
                case "vendor":
                    vendorMenu();
                    OnSession = false;
                    break;
                case "purchase":
                    purchaseMenu();
                    OnSession = false;
                    break;
                case "exit":
                    Console.WriteLine("Program will Exit.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Choice. Try Again.");
                    break;
            }
        }
        
        static void vendorMenu()
        {
            string userChoice;
            string vendor;
            string subChoice;
            bool isContinue = doContinue();
            Console.WriteLine(isContinue);

            while (isContinue)
            {
                Console.WriteLine("Vendor Management\n[Add] | [Search] | [Update] | [Delete] | [Exit]");
                userChoice = Console.ReadLine();
                switch (userChoice.ToLower())
                {
                    case "add":
                        Console.WriteLine("Vendor Name: ");
                        vendor = Console.ReadLine();
                        AddVendor(vendor);
                        break;
                    case "search":
                        Console.WriteLine("What method of search do you want?\n[Single] | [Table]");
                        subChoice = Console.ReadLine();
                        SearchVendor(subChoice);
                        break;
                    case "update":
                        Console.WriteLine("Choose Vendor: ");
                        vendor = Console.ReadLine();
                        UpdateVendor(vendor);
                        break;
                    case "delete":
                        Console.WriteLine("Choose Vendor: ");
                        vendor = Console.ReadLine();
                        DeleteVendor(vendor);
                        break;
                    case "exit":
                        Menu();
                        break;
                    default:
                        Console.Write("Invalid Input. ");
                        break;
                }

                isContinue = doContinue();
            }

        }

        static bool doContinue()
        {
            Console.Write("Do you wish to continue? y/n: ");
            string choiceInput = Console.ReadLine();
            bool isContinue = false;

            switch (choiceInput.ToLower())
            {
                case "y":
                    isContinue = true;
                    break;
                case "n":
                    isContinue = false;
                    break;
                default:
                    Console.WriteLine("Invalid Input, System will exit.");
                    Environment.Exit(0);
                    break;
            }

            return isContinue;
        }

        static void purchaseMenu()
        {
            // yes
        }

        static void AddVendor(string vendorName) // vendor create
        {
            Console.WriteLine($"Added {vendorName}.");
            vendor.Add(vendorName);
        }

        static void SearchVendor(string subChoice) // vendor retrieve sub-menu
        {
            string vendor;
            switch (subChoice.ToLower())
            {
                case "single":
                    Console.WriteLine("Search for Vendor Name: ");
                    vendor = Console.ReadLine();
                    retrieveSingleVendor(vendor);
                    break;
                case "table":
                    Console.WriteLine("List of Vendors: ");
                    retrieveTableVendor();
                    break;
                default:
                    Console.WriteLine("Invalid Choice.");
                    break;
            }
        }

        static void retrieveSingleVendor(string vendorName) // single/specific vendor
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

        static void retrieveTableVendor() // printall
        {
            foreach (var vendors in vendor)
            {
                Console.WriteLine(vendors);
            }
        }

        static void UpdateVendor(string vendorName) // update vendor
        {
            bool hasFound = false;
            string replaceVendor;
            int index = 0;

            foreach (var vendorSearch in vendor)
            {
                if (vendorName.Equals(vendorSearch))
                {
                    hasFound = true;
                    index = vendor.IndexOf(vendorName);
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
                Console.Write($"Replace {vendorName} with: ");
                replaceVendor = Console.ReadLine();
                vendor[index] = replaceVendor;

                Console.WriteLine($"Successfully replaced {vendorName} with {replaceVendor}");
            }
            else
            {
                Console.WriteLine($"{vendorName} not found.");
            }

        }

        static void DeleteVendor(string vendorName)
        {
            bool hasFound = false;
            int index = 0;

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
                vendor.Remove(vendorName);
                Console.WriteLine($"Successfully deleted {vendorName}");
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
