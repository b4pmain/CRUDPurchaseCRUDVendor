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
            Purchases(); // initialize sample purchases
            Vendors(); // initialize sample vendors
            while (OnSession)
            {
                Menu();
            }
            
        }

        static void Menu()
        {
            OnSession = true;

            string userChoice;
            Console.WriteLine("Purchase and Vendor Management\n[Add] [Search] [Update] [Delete] [Exit]");
            userChoice = Console.ReadLine();
            switch (userChoice.ToLower())
            {
                case "add":
                    AddEntry();
                    OnSession = false;
                    break;
                case "search":
                    SearchEntry();
                    OnSession = false;
                    break;
                case "update":
                    UpdateEntry();
                    OnSession = false;
                    break;
                case "delete":
                    DeleteEntry();
                    OnSession = false;
                    break;
                case "exit":
                    Console.WriteLine("Exiting the Program.");
                    OnSession = false;
                    break;
                default:
                    break;
            }
        }

        static void AddEntry()
        {
            string VendorName;
            string PurchaseName;
            bool isContinue = doContinue();

            while(isContinue)
            {
                Console.Write("Add Entry For Vendor: \n");
                VendorName = Console.ReadLine();
                if (VendorName != null)
                {
                    Console.WriteLine($"{VendorName} Added.");
                    vendor.Add(VendorName);
                }

                Console.Write("Add Entry For Vendor: \n");
                PurchaseName = Console.ReadLine();
                if (PurchaseName != null)
                {
                    Console.WriteLine($"{PurchaseName} Added.");
                    purchase.Add(PurchaseName);
                }

                isContinue = doContinue();
            }

        }

        static void SearchEntry()
        {
            string userChoice;
            Console.WriteLine("What do you wanna search for?\n[Vendor] [Purchase] [Print Table]");
            userChoice = Console.ReadLine();

            switch (userChoice.ToLower())
            {
                case "vendor":
                    searchVendor();
                    break;
                case "purchase":
                    searchPurchase();
                    break;
                case "print table":
                    // printTable();
                    break;
                default:
                    Console.WriteLine("Invalid Choice.");
                    break;
            }
        }

        static void searchVendor()
        {
            string vendorName;
            Console.Write("Type Vendor Name: ");
            vendorName = Console.ReadLine();

            bool hasFound = false;

            Console.WriteLine($"Searching for {vendorName}");
            foreach (var vendorSearch in purchase)
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

        static void searchPurchase()
        {
            string purchaseName;
            Console.Write("Type Purchase Name: ");
            purchaseName = Console.ReadLine();
            bool hasFound = false;
            Console.WriteLine($"Searching for {purchaseName}");
            foreach (var purchaseSearch in purchase)
            {
                if (purchaseName.Equals(purchaseSearch))
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
                Console.WriteLine($"{purchaseName} found.");
            }
            else
            {
                Console.WriteLine($"{purchaseName} not found.");
            }
        }

        static void UpdateEntry()
        {
            string userChoice;
            Console.WriteLine("What do you wanna search for?\n[Vendor] [Purchase]");
            userChoice = Console.ReadLine();

            switch (userChoice.ToLower())
            {
                case "vendor":
                    updateVendor();
                    break;
                case "purchase":
                    updatePurchase();
                    break;
                default:
                    Console.WriteLine("Invalid Choice.");
                    break;
            }
        }

        static void updateVendor()
        {
            bool hasFound = false;
            string replaceVendor, vendorName;
            int index = 0;
            Console.WriteLine("");
            vendorName = Console.ReadLine();

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
                purchase[index] = replaceVendor;

                Console.WriteLine($"Successfully replaced {vendorName} with {replaceVendor}");
            }
            else
            {
                Console.WriteLine($"{vendorName} not found.");
            }
        }

        static void updatePurchase()
        {
            bool hasFound = false;
            string replacePurchase, purchaseName;
            int index = 0;
            Console.WriteLine("");
            purchaseName = Console.ReadLine();

            foreach (var purchaseSearch in purchase)
            {
                if (purchaseName.Equals(purchaseSearch))
                {
                    hasFound = true;
                    index = purchase.IndexOf(purchaseName);
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
                Console.WriteLine($"{purchaseName} found.");
                Console.Write($"Replace {purchaseName} with: ");
                replacePurchase = Console.ReadLine();
                purchase[index] = replacePurchase;

                Console.WriteLine($"Successfully replaced {purchaseName} with {replacePurchase}");
            }
            else
            {
                Console.WriteLine($"{purchaseName} not found.");
            }
        }

        static void DeleteEntry()
        {

        }

        //static void Menu()
        //{
        //    OnSession = true;

        //    string userChoice;
        //    Console.WriteLine("Welcome to Purchases and Vendor Management!\n[Purchase Management] | [Vendor Management] | [Exit]");
        //    userChoice = Console.ReadLine();
        //    switch (userChoice.ToLower())
        //    {
        //        case "vendor":
        //            vendorMenu();
        //            OnSession = false;
        //            break;
        //        case "purchase":
        //            purchaseMenu();
        //            OnSession = false;
        //            break;
        //        case "exit":
        //            Console.WriteLine("Program will Exit.");
        //            Environment.Exit(0);
        //            break;
        //        default:
        //            Console.WriteLine("Invalid Choice. Try Again.");
        //            break;
        //    }
        //}

        static bool doContinue() // confirmation message
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

        //static void purchaseMenu() // purchase menu
        //{
        //    string userChoice;
        //    string purchase;
        //    string subChoice;
        //    bool isContinue = doContinue();
        //    Console.WriteLine(isContinue);

        //    while (isContinue)
        //    {
        //        Console.WriteLine("Purchase Management\n[Add] | [Search] | [Update] | [Delete] | [Exit]");
        //        userChoice = Console.ReadLine();
        //        switch (userChoice.ToLower())
        //        {
        //            case "add":
        //                Console.WriteLine("Purchase Name: ");
        //                purchase = Console.ReadLine();
        //                AddPurchase(purchase);
        //                break;
        //            case "search":
        //                Console.WriteLine("What method of search do you want?\n[Single] | [Table]");
        //                subChoice = Console.ReadLine();
        //                SearchPurchase(subChoice);
        //                break;
        //            case "update":
        //                Console.WriteLine("Choose Purchase: ");
        //                purchase = Console.ReadLine();
        //                UpdatePurchase(purchase);
        //                break;
        //            case "delete":
        //                Console.WriteLine("Choose Purchase: ");
        //                purchase = Console.ReadLine();
        //                DeletePurchase(purchase);
        //                break;
        //            case "exit":
        //                Menu();
        //                break;
        //            default:
        //                Console.Write("Invalid Input. ");
        //                break;
        //        }

        //        isContinue = doContinue();
        //    }
        //}

        //static void AddPurchase(string purchaseName)
        //{
        //    Console.WriteLine($"Added {purchaseName}.");
        //    purchase.Add(purchaseName);
        //}

        //static void SearchPurchase(string subChoice) // purchase retrieve sub-menu
        //{
        //    string purchase;
        //    switch (subChoice.ToLower())
        //    {
        //        case "single":
        //            Console.WriteLine("Search for Purchase: ");
        //            purchase = Console.ReadLine();
        //            retrieveSinglePurchase(purchase);
        //            break;
        //        case "table":
        //            Console.WriteLine("List of Purchase: ");
        //            retrieveTablePurchase();
        //            break;
        //        default:
        //            Console.WriteLine("Invalid Choice.");
        //            break;
        //    }
        //}

        //static void retrieveSinglePurchase(string purchaseName) // single/specific purchase
        //{
        //    bool hasFound = false;

        //    Console.WriteLine($"Searching for {purchaseName}");
        //    foreach (var purchaseSearch in purchase)
        //    {
        //        if (purchaseName.Equals(purchaseSearch))
        //        {
        //            hasFound = true;
        //            break;
        //        }
        //        else
        //        {
        //            hasFound = false;
        //            continue;
        //        }
        //    }

        //    if (hasFound)
        //    {
        //        Console.WriteLine($"{purchaseName} found.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{purchaseName} not found.");
        //    }
        //}

        //static void retrieveTablePurchase() // printall purchase
        //{
        //    foreach (var purchases in purchase)
        //    {
        //        Console.WriteLine(purchases);
        //    }

        //    if (purchase.Count < 1)
        //    {
        //        Console.WriteLine("List is Empty.");
        //    }
        //}

        //static void UpdatePurchase(string purchaseName) // update purchase
        //{
        //    bool hasFound = false;
        //    string replacePurchase;
        //    int index = 0;

        //    foreach (var purchaseSearch in purchase)
        //    {
        //        if (purchaseName.Equals(purchaseSearch))
        //        {
        //            hasFound = true;
        //            index = purchase.IndexOf(purchaseName);
        //            break;
        //        }
        //        else
        //        {
        //            hasFound = false;
        //            continue;
        //        }
        //    }

        //    if (hasFound)
        //    {
        //        Console.WriteLine($"{purchaseName} found.");
        //        Console.Write($"Replace {purchaseName} with: ");
        //        replacePurchase = Console.ReadLine();
        //        purchase[index] = replacePurchase;

        //        Console.WriteLine($"Successfully replaced {purchaseName} with {replacePurchase}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{purchaseName} not found.");
        //    }

        //}

        //static void DeletePurchase(string purchaseName)
        //{
        //    bool hasFound = false;
        //    int index = 0;

        //    foreach (var purchaseSearch in purchase)
        //    {
        //        if (purchaseName.Equals(purchaseSearch))
        //        {
        //            hasFound = true;
        //            break;
        //        }
        //        else
        //        {
        //            hasFound = false;
        //            continue;
        //        }
        //    }

        //    if (hasFound)
        //    {
        //        Console.WriteLine($"{purchaseName} found.");
        //        purchase.Remove(purchaseName);
        //        Console.WriteLine($"Successfully deleted {purchaseName}.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{purchaseName} not found.");
        //    }
        //}

        //static void vendorMenu()
        //{
        //    string userChoice;
        //    string vendor;
        //    string subChoice;
        //    bool isContinue = doContinue();
        //    Console.WriteLine(isContinue);

        //    while (isContinue)
        //    {
        //        Console.WriteLine("Vendor Management\n[Add] | [Search] | [Update] | [Delete] | [Exit]");
        //        userChoice = Console.ReadLine();
        //        switch (userChoice.ToLower())
        //        {
        //            case "add":
        //                Console.WriteLine("Vendor Name: ");
        //                vendor = Console.ReadLine();
        //                AddVendor(vendor);
        //                break;
        //            case "search":
        //                Console.WriteLine("What method of search do you want?\n[Single] | [Table]");
        //                subChoice = Console.ReadLine();
        //                SearchVendor(subChoice);
        //                break;
        //            case "update":
        //                Console.WriteLine("Choose Vendor: ");
        //                vendor = Console.ReadLine();
        //                UpdateVendor(vendor);
        //                break;
        //            case "delete":
        //                Console.WriteLine("Choose Vendor: ");
        //                vendor = Console.ReadLine();
        //                DeleteVendor(vendor);
        //                break;
        //            case "exit":
        //                Menu();
        //                break;
        //            default:
        //                Console.Write("Invalid Input. ");
        //                break;
        //        }

        //        isContinue = doContinue();
        //    }

        //}

        //static void AddVendor(string vendorName) // vendor create
        //{
        //    Console.WriteLine($"Added {vendorName}.");
        //    vendor.Add(vendorName);
        //}

        //static void SearchVendor(string subChoice) // vendor retrieve sub-menu
        //{
        //    string vendor;
        //    switch (subChoice.ToLower())
        //    {
        //        case "single":
        //            Console.WriteLine("Search for Vendor Name: ");
        //            vendor = Console.ReadLine();
        //            retrieveSingleVendor(vendor);
        //            break;
        //        case "table":
        //            Console.WriteLine("List of Vendors: ");
        //            retrieveTableVendor();
        //            break;
        //        default:
        //            Console.WriteLine("Invalid Choice.");
        //            break;
        //    }
        //}

        //static void retrieveSingleVendor(string vendorName) // single/specific vendor
        //{
        //    bool hasFound = false;

        //    Console.WriteLine($"Searching for {vendorName}");
        //    foreach (var vendorSearch in vendor)
        //    {
        //        if (vendorName.Equals(vendorSearch))
        //        {
        //            hasFound = true;
        //            break;
        //        }
        //        else
        //        {
        //            hasFound = false;
        //            continue;
        //        }
        //    }

        //    if (hasFound)
        //    {
        //        Console.WriteLine($"{vendorName} found.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{vendorName} not found.");
        //    }
        //}

        //static void retrieveTableVendor() // printall
        //{
        //    foreach (var vendors in vendor)
        //    {
        //        Console.WriteLine(vendors);
        //    }
        //}

        //static void UpdateVendor(string vendorName) // update vendor
        //{
        //    bool hasFound = false;
        //    string replaceVendor;
        //    int index = 0;

        //    foreach (var vendorSearch in vendor)
        //    {
        //        if (vendorName.Equals(vendorSearch))
        //        {
        //            hasFound = true;
        //            index = vendor.IndexOf(vendorName);
        //            break;
        //        }
        //        else
        //        {
        //            hasFound = false;
        //            continue;
        //        }
        //    }

        //    if (hasFound)
        //    {
        //        Console.WriteLine($"{vendorName} found.");
        //        Console.Write($"Replace {vendorName} with: ");
        //        replaceVendor = Console.ReadLine();
        //        vendor[index] = replaceVendor;

        //        Console.WriteLine($"Successfully replaced {vendorName} with {replaceVendor}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{vendorName} not found.");
        //    }

        //}

        //static void DeleteVendor(string vendorName)
        //{
        //    bool hasFound = false;
        //    int index = 0;

        //    foreach (var vendorSearch in vendor)
        //    {
        //        if (vendorName.Equals(vendorSearch))
        //        {
        //            hasFound = true;
        //            break;
        //        }
        //        else
        //        {
        //            hasFound = false;
        //            continue;
        //        }
        //    }

        //    if (hasFound)
        //    {
        //        Console.WriteLine($"{vendorName} found.");
        //        vendor.Remove(vendorName);
        //        Console.WriteLine($"Successfully deleted {vendorName}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{vendorName} not found.");
        //    }
        //}

        static void Vendors()
        {
            vendor.Add("Nescafe");
            vendor.Add("San Miguel Corporation");
            vendor.Add("Rebisco");
            vendor.Add("Nestle");
            vendor.Add("Oishi");
        }

        static void Purchases()
        {
            purchase.Add("Coffee Packs");
            purchase.Add("Beer Packs");
            purchase.Add("Biscuit Packs");
            purchase.Add("Tea Bag");
            purchase.Add("Cracker Packs");
        }

    }
}
