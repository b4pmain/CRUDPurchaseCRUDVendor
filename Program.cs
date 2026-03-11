using System;
using System.Collections.Specialized;
using System.Threading.Channels;
using PurchaseVendorAppService;
using PurchaseVendorModels;

// re-writing code for better logic and structure

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

            Console.WriteLine("List of Vendors: ");
            foreach (var vendor in vendors)
            {
                Console.WriteLine($"Vendor Name: {vendor.VendorName} " +
                    $"| Description: {vendor.VendorDescription} " +
                    $"| Contact: {vendor.ContactPhone} " +
                    $"| Email: {vendor.ContactEmail}");
            }
            Console.WriteLine("");

            Console.WriteLine("List of Purchase: ");
            foreach (var purchase in purchases)
            {
                Console.WriteLine($"Purchase Vendor: {purchase.PurchaseVndr} " +
                    $"| Purchase Name: {purchase.PurchaseName} " +
                    $"| Qty: {purchase.PurchaseQty} " +
                    $"| Price: {purchase.PurchasePrice} " +
                    $"| Date: {purchase.PurchaseDate} ");
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

            while (true) 
            {
                separator();
                Console.Write("Enter Vendor Name: ");
                vendorName = Console.ReadLine();
                Console.Write("Enter Vendor Description: ");
                vendorDescription = Console.ReadLine();
                Console.Write("Enter Contact Number: ");
                contactPhone = Console.ReadLine();
                Console.Write("Enter Contact Email: ");
                contactEmail = Console.ReadLine();

                if (vendorName != "")
                {
                    Console.WriteLine($"Successfully added \"{vendorName}\" and its entries." +
                        $"\nDescription: {vendorDescription}" +
                        $"\nContact Phone: {contactPhone}" +
                        $"\nContact Email: {contactEmail}");
                    break;
                }
                invalid();
            }

            var newVendor = new Vendor 
            { 
                VendorName = vendorName, 
                VendorDescription = vendorDescription, 
                ContactPhone = contactPhone, 
                ContactEmail = contactEmail
            };

            Menu();
        }

        static void addPrch()
        {
            string purchaseVndr,
                purchaseName,
                purchaseDate;

            int purchaseQty;
            double purchasePrice;

            while (true)
            {
                separator();
                Console.Write("Enter Purchase: ");
                purchaseName = Console.ReadLine();
                Console.Write("Enter Vendor: ");
                purchaseVndr = Console.ReadLine();
                Console.Write("Enter Qty: ");
                purchaseQty = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Price: ");
                purchasePrice = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter Date: ");
                purchaseDate = Console.ReadLine();

                if (purchaseName != "")
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
            }

            Menu();
        }

        static void srchOpn() 
        {
            string srchChoice;

            while (true)
            {
                separator();
                Console.WriteLine("Select a category to search from: \n[Vendor] | [Purchase] | [Exit]\n");
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
                            srchPrch();
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

            while (true)
            {
                separator();
                Console.Write("Search for Vendor Name: ");
                vendorName = Console.ReadLine();

                if (vendorName != "")
                {
                    if (vendor.Contains(vendorName))
                    {
                        Console.WriteLine($"Vendor \"{vendorName}\" is located at index \"{vendor.IndexOf(vendorName)}\".");
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
            string prchName, vendorName;
            int index;

            while (true)
            {
                separator();
                Console.Write("Enter Purchase Name: ");
                prchName = Console.ReadLine();

                if (purchase.Contains(prchName))
                {
                    index = purchase.IndexOf(prchName);
                    vendorName = vendor[index];
                    Console.WriteLine($"Purchase \"{prchName}\" located on Vendor \"{vendorName}\".");
                    break;
                }
                else
                {
                    Console.WriteLine($"Purchase \"{prchName}\" not found.");
                    break;
                }
            }
            Menu();
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
            string vendorName, vendorReplace;
            int index;

            while (true)
            {
                separator();
                Console.Write("Search for Vendor Name: ");
                vendorName = Console.ReadLine();

                if (vendorName != "")
                {
                    if (vendor.Contains(vendorName))
                    {
                        index = vendor.IndexOf(vendorName);
                        Console.Write($"Update \"{vendorName}\" with?: ");
                        vendorReplace = Console.ReadLine();

                        if (vendorReplace != "")
                        {
                            Console.WriteLine($"Successfully replaced \"{vendorName}\" with \"{vendorReplace}\".");
                            vendor[index] = vendorReplace;
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

        static void updPrch() // prch seaching
        {
            string prchName, vendorName;
            int index;

            while (true)
            {
                separator();
                Console.Write("Enter Vendor's Name: ");
                vendorName = Console.ReadLine();

                if (vendorName != "")
                {
                    if (vendor.Contains(vendorName)) // proceed to purchase update
                    {

                        while (true)
                        {
                            index = vendor.IndexOf(vendorName);
                            Console.Write("Input Purchase: ");
                            prchName = Console.ReadLine();

                            if (prchName != "") // validity checking if not empty
                            {
                                Console.WriteLine($"Successfully updated Purchase \"{purchase[index]}\" to \"{prchName}\" in Vendor \"{vendorName}\".");
                                purchase.Insert(index, prchName);
                                break;
                            }

                            invalid();
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
            string vendorName, vendorReplace, choice;
            int index;

            while (true)
            {
                separator();
                Console.Write("Search for Vendor Name: ");
                vendorName = Console.ReadLine();

                if (vendorName != "")
                {
                    if (vendor.Contains(vendorName))
                    {
                        index = vendor.IndexOf(vendorName);
                        Console.WriteLine($"Do you want to delete Vendor \"{vendorName}\"? This will also delete all of its purchases.\n[Y] or [N]\n");
                        Console.Write("Input: ");
                        choice = Console.ReadLine();

                        switch (choice.ToLower())
                        {
                            case "y":
                                Console.WriteLine($"Deletion of Vendor \"{vendorName}\" is successful.");
                                vendor.RemoveAt(index);
                                purchase.RemoveAt(index);
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
            string prchName, vendorName, choice;
            int index;

            while (true)
            {
                separator();
                Console.Write("Enter Vendor's Name: ");
                vendorName = Console.ReadLine();

                if (vendorName != "")
                {
                    index = vendor.IndexOf(vendorName);
                    if (vendor.Contains(vendorName))
                    {
                        while (true)
                        {
                            if (!purchase[index].Equals("empty"))
                            {
                                Console.Write("Enter Purchase: ");
                                prchName = Console.ReadLine();

                                if (prchName != "")
                                {
                                    Console.WriteLine($"Do you want to delete the Purchase \"{prchName}\"?\n[Y] or [N]\n");
                                    Console.Write("Input: ");
                                    choice = Console.ReadLine();

                                    switch (choice.ToLower())
                                    {
                                        case "y":
                                            Console.WriteLine($"Deletion of Purchase \"{prchName}\" is successful.");
                                            purchase[index] = "empty";
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
                            }
                            else
                            {
                                Console.WriteLine($"Purchase list of \"{vendorName}\" is already Empty. Returning to Main menu.");
                                break;
                            }
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

        static void delAll()
        {
            string choice;
            int count = vendor.Count();

            if (count != 0)
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
                            Console.WriteLine("Operation Successful.");
                            purchase.Clear();
                            vendor.Clear();
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
        static void Vendors()
        {

        }

        static void Purchases()
        {

        }

    }
}
