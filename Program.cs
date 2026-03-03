using System;

// re-writing code for better logic and structure

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
            Console.WriteLine("=================================================================");
            Console.WriteLine("Welcome to Vendor Purchase Management!\n[Add] | [Search] | [Update] | [Delete] | [Print Table] | [Exit]");
            Console.WriteLine("=================================================================\n");
            Console.Write("Input: ");
            userChoice = Console.ReadLine();
            switch (userChoice.ToLower())
            {
                case "add":
                    addVendor();
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

        static void separator() // design
        {
            Console.WriteLine("-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-");
        }

        static void printTable()
        {
            separator();
            int count = vendor.Count();
            Console.WriteLine("Vendor & Purchase List:");
            if (count != 0) { 
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(vendor[i] + " | " + purchase[i]);
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

        static void addVendor()
        {
            string vendorName;
            int index;

            while (true) 
            {
                separator();
                Console.Write("Enter Vendor Name: ");
                vendorName = Console.ReadLine();

                if (vendorName != "")
                {
                    Console.WriteLine($"Successfully added \"{vendorName}\".");
                    break;
                }
                invalid();
            }

            vendor.Add(vendorName);
            index = vendor.IndexOf(vendorName);
            addPrch(index, vendorName);
        }

        static void addPrch(int index, string vendorName)
        {
            string purchaseName;
            
            while (true)
            {
                separator();
                Console.Write("Enter Purchase(s): ");
                purchaseName = Console.ReadLine();

                if (purchaseName != "")
                {
                    Console.WriteLine($"Successfully added \"{purchaseName}\" into Vendor \"{vendorName}\".");
                    break;
                }

                Console.WriteLine("No Input Detected, Putting \"Empty\" instead.");
                purchase.Insert(index, "empty");
                Menu();
            }

            purchase.Insert(index, purchaseName);
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
