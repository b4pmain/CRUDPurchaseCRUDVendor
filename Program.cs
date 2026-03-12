using System;
using System.Collections.Specialized;
using System.Threading.Channels;
using PurchaseVendorAppService;
using PurchaseVendorModels;

namespace CRUDPurchaseCRUDVendor
{
    internal class Program
    {
        static List<string> purchase = new List<string>();
        static List<string> vendor = new List<string>();

        static PurVenAppService pvAppService = new PurVenAppService();

        static bool OnSession = true;
        static void Main(string[] args)
        {
            while (OnSession)
            {
                Menu();
            }
            
        }

        static void Menu()
        {
            OnSession = true;

            string userChoice;
            Console.WriteLine("=================================================================");
            Console.WriteLine("Welcome to Vendor Purchase Management!\n[Add] | [Search] | [Update] | [Delete] | [Print Table] | [Exit]");
            Console.WriteLine("=================================================================\n");
            Console.Write("Input: ");
            userChoice = Console.ReadLine();
            switch (userChoice.ToLower())
            {
                case "add":
                    addOpt();
                    OnSession = false;
                    break;
                case "search":
                    srchOpn();
                    OnSession = false;
                    break;
                case "update":
                    updOpn();
                    OnSession = false;
                    break;
                case "delete":
                    delOpn();
                    OnSession = false;
                    break;
                case "print":
                    printTable();
                    break;
                case "exit":
                    Console.WriteLine("The Program will Exit.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Choice. Try Again.");
                    break;
            }
        }

        static void separator() // design separator
        {
            Console.WriteLine("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-");
        }

        static void printTable()
        {
            separator();
            List<Vendor> vendors = pvAppService.GetVendors();
            List<Purchase> purchases = pvAppService.GetAllPurchases();

            int venCount, purCount;
            venCount = pvAppService.VenCount();
            purCount = pvAppService.PurCount();

            bool hasVen = venCount > 0;
            bool hasPur = purCount > 0;

            Console.WriteLine("[List of Vendors]");
            if (hasVen)
            {
                foreach (var vendor in vendors)
                {
                    Console.WriteLine($"Vendor Name: {vendor.VendorName} " +
                        $"| Description: {vendor.VendorDescription} " +
                        $"| Contact: {vendor.ContactPhone} " +
                        $"| Email: {vendor.ContactEmail}");
                }
            }
            else
            {
                Console.WriteLine("List is Empty.");
            }
            Console.WriteLine("");

            Console.WriteLine("[List of Purchase]");
            if (hasPur)
            {
                foreach (var purchase in purchases)
                {
                    Console.WriteLine($"Purchase Vendor: {purchase.PurchaseVndr} " +
                        $"| Purchase Name: {purchase.PurchaseName} " +
                        $"| Qty: {purchase.PurchaseQty} " +
                        $"| Price: {purchase.PurchasePrice} " +
                        $"| Date: {purchase.PurchaseDate} ");
                }
            }
            else
            {
                Console.WriteLine("List is Empty.");
            }
            Menu();
        }

        static void invalid()
        {
            Console.WriteLine("Invalid Input. Please Try Again.");
        }
        static void addOpt()
        {
            string addChoice;

            while (true)
            {
                separator();
                Console.WriteLine("Select a category to add from: \n[Vendor] | [Purchase] | [Exit]\n");
                Console.Write("Input: ");
                addChoice = Console.ReadLine();

                if (addChoice != "")
                {
                    switch (addChoice.ToLower())
                    {
                        case "vendor":
                            Console.WriteLine("Selected: [Vendor]");
                            addVendor();
                            break;
                        case "purchase":
                            Console.WriteLine("Selected: [Purchase]");
                            addPrch();
                            break;
                        case "exit":
                            Menu();
                            break;
                        default:
                            Console.WriteLine("Invalid Choice.");
                            continue;
                    }
                    break;
                }

                invalid();
            }

            Menu();
        }
        static void addVendor()
        {
            string vendorName,
                vendorDescription,
                contactPhone,
                contactEmail;

            bool isExisting;

            while (true) 
            {
                separator();
                Console.Write("Enter Vendor Name: ");
                vendorName = Console.ReadLine();
                vendorName.ToUpper();
                Console.Write("Enter Vendor Description: ");
                vendorDescription = Console.ReadLine();
                Console.Write("Enter Contact Number: ");
                contactPhone = Console.ReadLine();
                Console.Write("Enter Contact Email: ");
                contactEmail = Console.ReadLine();
                isExisting = pvAppService.venIsExisting(vendorName);

                if (vendorName != "" && vendorDescription != "" && contactPhone != "" && contactEmail != "" && !isExisting)
                {
                    Console.WriteLine($"Successfully added \"{vendorName}\" and its entries." +
                        $"\nDescription: {vendorDescription}" +
                        $"\nContact Phone: {contactPhone}" +
                        $"\nContact Email: {contactEmail}");
                    break;
                }
                invalid();
                Console.WriteLine($"Double Check if Vendor \"{vendorName}\" is a duplicate.");
            }

            var newVendor = new Vendor 
            { 
                VendorName = vendorName, 
                VendorDescription = vendorDescription, 
                ContactPhone = contactPhone, 
                ContactEmail = contactEmail
            };

            pvAppService.AddVendor(newVendor);

            Menu();
        }

        static void addPrch()
        {
            string purchaseVndr,
                purchaseName,
                purchaseDate;

            int purchaseQty;
            double purchasePrice;

            bool isExisting;

            while (true)
            {
                separator();
                Console.Write("Enter Purchase: ");
                purchaseName = Console.ReadLine();
                Console.Write("Enter Vendor: ");
                purchaseVndr = Console.ReadLine();
                purchaseVndr.ToUpper();
                Console.Write("Enter Qty: ");
                purchaseQty = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Price: ");
                purchasePrice = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter Date: ");
                purchaseDate = Console.ReadLine();
                isExisting = pvAppService.prchIsExisting(purchaseName);

                if (purchaseVndr != "" && purchaseName != "" && purchaseDate != "" && purchaseQty != null && purchasePrice != null && !isExisting)
                {
                    Console.WriteLine($"Successfully added \"{purchaseName}\" and its entries." +
                        $"\nVendor: {purchaseVndr}" +
                        $"\nQuantity: {purchaseQty}" +
                        $"\nPrice: {purchasePrice}" +
                        $"\nDate: {purchaseDate}");

                    var newPurchase = new Purchase
                    {
                        PurchaseName = purchaseName,
                        PurchaseVndr = purchaseVndr,
                        PurchaseQty = purchaseQty,
                        PurchasePrice = purchasePrice,
                        PurchaseDate = purchaseDate
                    };

                    pvAppService.AddPurchase(newPurchase);
                    break;
                }
                invalid();
                Console.WriteLine($"Double Check if Purchase \"{purchaseName}\" is a duplicate.");
            }

            Menu();
        }

        static void srchOpn() 
        {
            string srchChoice;

            while (true)
            {
                separator();
                Console.WriteLine("Select a category to retrieve from: \n[Vendor] | [Purchase] | [Exit]\n");
                Console.Write("Input: ");
                srchChoice = Console.ReadLine();

                if (srchChoice != "")
                {
                    switch (srchChoice.ToLower())
                    {
                        case "vendor":
                            Console.WriteLine("Selected: [Vendor]");
                            srchVendor();
                            break;
                        case "purchase":
                            Console.WriteLine("Selected: [Purchase]");
                            srchPrchMenu();
                            break;
                        case "exit":
                            Menu();
                            break;
                        default:
                            Console.WriteLine("Invalid Choice.");
                            continue;
                    }
                    break;
                }

                invalid();
            }

            Menu();
        }

        static void srchPrchMenu()
        {
            string srchChoice;

            while (true)
            {
                separator();
                Console.WriteLine("Select method of retrieval: \n[Specifc Purchase] | [Vendor-Specific Purchase] | [All Purchases] | [Exit]\n");
                Console.Write("Input: ");
                srchChoice = Console.ReadLine();

                if (srchChoice != "")
                {
                    switch (srchChoice.ToLower())
                    {
                        case "specific":
                            Console.WriteLine("Selected: [Specific Purchase]");
                            srchPrch();
                            break;
                        case "vendor":
                            Console.WriteLine("Selected: [Vendor-Specific Purchase]");
                            srchPrchVen();
                            break;
                        case "all":
                            Console.WriteLine("Selected: [All Purchases]");
                            srchPrchAll();
                            break;
                        case "exit":
                            Menu();
                            break;
                        default:
                            Console.WriteLine("Invalid Choice.");
                            continue;
                    }
                    break;
                }

                invalid();
            }

            Menu();
        }

        static void srchVendor() // vendor searching
        {
            string vendorName;
            bool venIsExisting;

            while (true)
            {
                separator();
                Console.Write("Search for Vendor Name: ");
                vendorName = Console.ReadLine();
                vendorName.ToUpper();

                if (vendorName != "")
                {
                    venIsExisting = pvAppService.venIsExisting(vendorName);
                    if (venIsExisting)
                    {
                        Console.WriteLine($"Vendor \"{vendorName}\" found." +
                            $"\n[Product List]");
                        srchPrchVen(vendorName);
                        // srchPrchVen
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Vendor \"{vendorName}\" not found.");
                        break;
                    }
                }
                invalid();
            }
            Menu();
        }

        static void srchPrch() // prch seaching
        {
            string prchName;
            bool isExisting = false;
            List<Purchase> purchases = pvAppService.GetAllPurchases();

            while (true)
            {
                separator();
                Console.Write("Enter Purchase Name: ");
                prchName = Console.ReadLine();

                var vendorName = pvAppService.PurGetByVendorName(prchName);
                isExisting = pvAppService.prchIsExisting(prchName);

                if (!isExisting)
                {
                    Console.WriteLine($"Purchase \"{prchName}\" not found.");
                    break;
                }
                else
                {
                    Console.WriteLine($"Purchase \"{prchName}\" found under \"{vendorName.PurchaseVndr}\"");
                    break;
                }

            }
            Menu();
        }
        static void srchPrchVen() // all purchases from vendor
        {
            string vendorName;
            bool isExisting = false;

            while (true)
            {
                separator();
                Console.Write("Enter Vendor Name: ");
                vendorName = Console.ReadLine();
                vendorName.ToUpper();
                isExisting = pvAppService.venIsExisting(vendorName);

                if (isExisting && vendorName != "")
                {
                    Console.WriteLine($"List of \"{vendorName}\" Purchases: ");
                    break;
                }

                invalid();

            }

            List<Purchase> vendorPurchase = pvAppService.PurchaseFromVendor(vendorName);
            foreach (Purchase purchase in vendorPurchase)
            {
                Console.WriteLine($"Purchase Vendor: {purchase.PurchaseVndr} " +
                    $"| Purchase Name: {purchase.PurchaseName} " +
                    $"| Qty: {purchase.PurchaseQty} " +
                    $"| Price: {purchase.PurchasePrice} " +
                    $"| Date: {purchase.PurchaseDate} ");
            }
            Menu();
        }
        static void srchPrchVen(string venName) // all purchases from vendor (after selecting the ven option)
        {
            bool isExisting = false;

            while (true)
            {
                isExisting = pvAppService.venIsExisting(venName);

                if (isExisting && venName != "")
                {
                    Console.WriteLine($"List of \"{venName}\" Purchases: ");
                    break;
                }

                invalid();

            }

            List<Purchase> vendorPurchase = pvAppService.PurchaseFromVendor(venName);
            foreach (Purchase purchase in vendorPurchase)
            {
                Console.WriteLine($"Purchase Vendor: {purchase.PurchaseVndr} " +
                    $"| Purchase Name: {purchase.PurchaseName} " +
                    $"| Qty: {purchase.PurchaseQty} " +
                    $"| Price: {purchase.PurchasePrice} " +
                    $"| Date: {purchase.PurchaseDate} ");
            }
            Menu();
        }
        static void srchPrchAll() // all purchases
        {
            separator();
            List<Purchase> purchases = pvAppService.GetAllPurchases();
            Console.WriteLine("[List of Purchase]");
            foreach (var purchase in purchases)
            {
                Console.WriteLine($"Purchase Vendor: {purchase.PurchaseVndr} " +
                    $"| Purchase Name: {purchase.PurchaseName} " +
                    $"| Qty: {purchase.PurchaseQty} " +
                    $"| Price: {purchase.PurchasePrice} " +
                    $"| Date: {purchase.PurchaseDate} ");
            }
        }

        static void updOpn()
        {
            string updChoice;

            while (true)
            {
                separator();
                Console.WriteLine("Select a category to update from: \n[Vendor] | [Purchase] | [Exit]\n");
                Console.Write("Input: ");
                updChoice = Console.ReadLine();

                if (updChoice != "")
                {
                    switch (updChoice.ToLower())
                    {
                        case "vendor":
                            Console.WriteLine("Selected: [Vendor]");
                            updVendor();
                            break;
                        case "purchase":
                            Console.WriteLine("Selected: [Purchase]");
                            updPrch();
                            break;
                        case "exit":
                            Menu();
                            break;
                        default:
                            Console.WriteLine("Invalid Choice.");
                            continue;
                    }
                    break;
                }

                invalid();
            }

            Menu();
        }

        static void updVendor()
        {
            string vendorName, 
                vendorReplace,
                vendorDescription,
                contactPhone,
                contactEmail;

            bool venIsExisting, isSuccess;

            while (true)
            {
                separator();
                Console.Write("Search for Vendor Name: ");
                vendorName = Console.ReadLine();

                if (vendorName != "")
                {
                    venIsExisting = pvAppService.venIsExisting(vendorName);
                    if (venIsExisting)
                    {
                        Console.WriteLine($"Found Vendor \"{vendorName}\".");
                        Console.Write("Enter Vendor Description: ");
                        vendorDescription = Console.ReadLine();
                        Console.Write("Enter Contact Number: ");
                        contactPhone = Console.ReadLine();
                        Console.Write("Enter Contact Email: ");
                        contactEmail = Console.ReadLine();

                        if (vendorDescription != "" && contactPhone != "" && contactEmail != "")
                        {
                            isSuccess = pvAppService.ChangeInfo(vendorName, vendorDescription, contactPhone, contactEmail);

                            if (isSuccess)
                            {
                                Console.WriteLine($"Successfully updated \"{vendorName}\"." +
                                    $"\nDescription: {vendorDescription}" +
                                    $"\nContact Phone: {contactPhone}" +
                                    $"\nContact Email: {contactEmail}");
                            }
                            else
                            {
                                Console.WriteLine($"Failed updating \"{vendorName}\".");
                            }

                            break;
                        }

                        Console.WriteLine("Invalid Input.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Vendor \"{vendorName}\" not found.");
                        break;
                    }
                }
                invalid();
            }
            Menu();
        }

        static void updPrch() // prch update
        {
            string prchName,
                prchVendor,
                prchDate;

            int prchQty;
            double prchPrice;

            bool purIsExisting, isSuccess;

            while (true)
            {
                separator();
                Console.Write("Enter Purchase: ");
                prchName = Console.ReadLine();

                if (prchName != "")
                {
                    purIsExisting = pvAppService.prchIsExisting(prchName);

                    if (purIsExisting) // proceed to purchase update
                    {

                        while (true)
                        {
                            Console.WriteLine($"Found Purchase \"{prchName}\".");
                            Console.Write("Enter Vendor: ");
                            prchVendor = Console.ReadLine();
                            Console.Write("Enter Qty: ");
                            prchQty = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Price: ");
                            prchPrice = Convert.ToDouble(Console.ReadLine());
                            Console.Write("Enter Date: ");
                            prchDate = Console.ReadLine();

                            if (prchVendor != "" && prchQty != null && prchPrice != null && prchDate != "") // validity checking if not empty
                            {

                                isSuccess = pvAppService.ChangePur(prchName, prchVendor, prchQty, prchPrice, prchDate);

                                if (isSuccess)
                                {
                                    Console.WriteLine($"Successfully updated \"{prchName}\" and its entries." +
                                    $"\nVendor: {prchVendor}" +
                                    $"\nQuantity: {prchQty}" +
                                    $"\nPrice: {prchPrice}" +
                                    $"\nDate: {prchDate}");
                                }
                                else
                                {
                                    Console.WriteLine($"Failed updating \"{prchName}\".");
                                }

                                break;
                            }

                            invalid();
                        }

                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Purchase \"{prchName}\" not found.");
                        break;
                    }
                }

                invalid();

            }
            Menu();
        }

        static void delOpn()
        {
            string delChoice;

            while (true)
            {
                separator();
                Console.WriteLine("Select a category to delete from: \n[Vendor] | [Purchase] | [Wipe All] | [Exit]\n");
                Console.Write("Input: ");
                delChoice = Console.ReadLine();

                if (delChoice != "")
                {
                    switch (delChoice.ToLower())
                    {
                        case "vendor":
                            Console.WriteLine("Selected: [Vendor]");
                            delVendor();
                            break;
                        case "purchase":
                            Console.WriteLine("Selected: [Purchase]");
                            delPrch();
                            break;
                        case "wipe":
                            Console.WriteLine("Selected: [Wipe All]");
                            delAll();
                            break;
                        case "exit":
                            Menu();
                            break;
                        default:
                            Console.WriteLine("Invalid Choice.");
                            continue;
                    }
                    break;
                }

                invalid();
            }

            Menu();
        }

        static void delVendor()
        {
            string vendorName, choice;
            bool isExisting, hasRemovedVendor;

            while (true)
            {
                separator();
                Console.Write("Search for Vendor Name: ");
                vendorName = Console.ReadLine();

                if (vendorName != "")
                {
                    isExisting = pvAppService.venIsExisting(vendorName);

                    if (isExisting)
                    {
                        Console.WriteLine($"Do you want to delete Vendor \"{vendorName}\"? This will also delete all of its purchases.\n[Y] or [N]\n");
                        Console.Write("Input: ");
                        choice = Console.ReadLine();

                        switch (choice.ToLower())
                        {
                            case "y":
                                var vendor = new Vendor();
                                vendor.VendorName = vendorName;

                                hasRemovedVendor = pvAppService.RemoveVendor(vendor);

                                if (!hasRemovedVendor)
                                {
                                    Console.WriteLine($"Deletion of Vendor \"{vendorName}\" has failed.");
                                }

                                Console.WriteLine($"Deletion of Vendor \"{vendorName}\" is successful.");
                                break;
                            case "n":
                                Console.WriteLine("Cancelling Deletion. Returning to Menu.");
                                break;
                            default:
                                Console.WriteLine("Invalid Choice.");
                                continue;
                        }
                        break;
                        
                    }
                    else
                    {
                        Console.WriteLine($"Vendor \"{vendorName}\" not found.");
                        break;
                    }
                }
                invalid();
            }
            Menu();
        }

        static void delPrch()
        {
            string purchaseName, choice;
            bool isExisting, hasRemovedPrch;

            while (true)
            {
                separator();
                Console.Write("Search for Vendor Name: ");
                purchaseName = Console.ReadLine();

                if (purchaseName != "")
                {
                    isExisting = pvAppService.prchIsExisting(purchaseName);

                    if (isExisting)
                    {
                        Console.WriteLine($"Do you want to delete Purchase \"{purchaseName}\"?\n[Y] or [N]\n");
                        Console.Write("Input: ");
                        choice = Console.ReadLine();

                        switch (choice.ToLower())
                        {
                            case "y":
                                var purchase = new Purchase();
                                purchase.PurchaseName = purchaseName;

                                hasRemovedPrch = pvAppService.RemovePurchase(purchase);

                                if (!hasRemovedPrch)
                                {
                                    Console.WriteLine($"Deletion of Vendor \"{purchaseName}\" has failed.");
                                }

                                Console.WriteLine($"Deletion of Vendor \"{purchaseName}\" is successful.");
                                break;
                            case "n":
                                Console.WriteLine("Cancelling Deletion. Returning to Menu.");
                                break;
                            default:
                                Console.WriteLine("Invalid Choice.");
                                continue;
                        }
                        break;

                    }
                    else
                    {
                        Console.WriteLine($"Vendor \"{purchaseName}\" not found.");
                        break;
                    }
                }
                invalid();
            }
            Menu();
        }

        static void delAll()
        {
            string choice;

            int venCount, purCount;
            venCount = pvAppService.VenCount();
            purCount = pvAppService.PurCount();

            bool isVenSuccess, isPurSuccess;

            if (venCount > 0 || purCount > 0)
            {
                while (true)
                {
                    separator();
                    Console.WriteLine("Are you sure you want to delete the entire list?\n[Y] or [N]");
                    Console.Write("Input: ");
                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "y":
                            isVenSuccess = pvAppService.RemoveAllPur();
                            isPurSuccess = pvAppService.RemoveAllVen();

                            if (isVenSuccess && isPurSuccess)
                            {
                                Console.WriteLine("Operation Successful.");
                                break;
                            }
                            Console.WriteLine("Operation unsuccessful.");
                            
                            break;
                        case "n":
                            Console.WriteLine("Operation Cancelled. Returning to Menu.");
                            break;
                        default:
                            invalid();
                            continue;
                    }
                    break;
                }
                Menu();
            }
            else
            {
                Console.WriteLine("List is Empty, this Operation cannot be done.");
                Menu();
            }
        }

    }
}
