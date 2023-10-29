using System;
using SplashKitSDK;

namespace PassTask13
{
    public static class UserActions
    {
        public const int ADMIN_CHOICE = 1;
        public const int MERCHANT_CHOICE = 2;
        public const int CUSTOMER_CHOICE = 3;

        public static int GetUserTypeChoice()
        {
            int choice;
            do
            {
                Console.WriteLine("Choose User Type");
                Console.WriteLine("1.Admin");
                Console.WriteLine("2.Merchant");
                Console.WriteLine("3.Customer");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            } while (choice < ADMIN_CHOICE || choice > CUSTOMER_CHOICE);

            return choice;
        }

        private static int GetLoginChoice()
        {
            int loginChoice;
            do
            {
                Console.WriteLine("1.Login or 2.Register");
                if (!int.TryParse(Console.ReadLine(), out loginChoice))
                {
                    Console.WriteLine("Invalid input. Please enter correct number.");
                }
            } while (loginChoice < 1 || loginChoice > 2);

            return loginChoice;
        }

        public static void HandleAdminFlow()
        {
            Admin loggedInAdmin = AdminLogin();
            if (loggedInAdmin != null)
            {
                int choice1 = 0;
                do{
                        Console.WriteLine($"Logged in as {loggedInAdmin.username}");
                        Console.WriteLine("1. Approve Customer");
                        Console.WriteLine("2. Reject Customer");
                        Console.WriteLine("3. Approve Merchant");
                        Console.WriteLine("4. Reject Merchant");
                        Console.WriteLine("5. Check products by Merchant");
                        Console.WriteLine("6. Approve Product");
                        Console.WriteLine("7. Reject Product");
                        Console.WriteLine("8. Get Invoice");
                        Console.WriteLine("9. View Customer Profile");
                        Console.WriteLine("10. Logout");
                        if (!int.TryParse(Console.ReadLine(), out choice1))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            continue;
                        }
                    
                    switch (choice1)
                    {
                        case 1:
                            ApproveCustomer();
                            break;
                        case 2:
                            RejectCustomer();
                            break;
                        case 3:
                            ApproveMerchant(); 
                            break;
                        case 4:
                            RejectMerchant();
                            break;
                        case 5:
                            loggedInAdmin.CheckProductsByMerchant();                        
                            break;
                        case 6:
                            Product productToApprove = SelectProductFromRegisteredProducts();
                            if (productToApprove != null)
                            {
                                loggedInAdmin.ApproveProduct(productToApprove);
                            }
                            break;
                        case 7:
                            Product productToReject = SelectProductFromRegisteredProducts();
                            if (productToReject != null)
                            {
                                loggedInAdmin.RejectProduct(productToReject);
                            }
                            break;
                        case 8:
                            loggedInAdmin.GetInvoice();
                            break;
                        case 9:
                            loggedInAdmin.ViewCustomerProfile();
                            break;
                        case 10:
                            Console.WriteLine("Logged out successfully!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option.");
                            break;   
                    }
                }while(choice1 != 10);
            }
        }

        public static void HandleMerchantFlow()
        {
            int loginChoice;
            do
            {
                loginChoice = GetLoginChoice();
                if (loginChoice == 1) // Login
                {
                    Merchant loggedInMerchant = MerchantLogin();
                    if (loggedInMerchant != null)
                    {
                        int choice1 = 0;
                        do
                        {
                            Console.WriteLine($"Logged in as {loggedInMerchant.username}");
                            Console.WriteLine("1. Add Product");
                            Console.WriteLine("2. Edit Product");
                            Console.WriteLine("3. Delete Product");
                            Console.WriteLine("4. Manage Order");
                            Console.WriteLine("5. View Products");
                            Console.WriteLine("6. View Invoices");
                            Console.WriteLine("7. Logout");
                            if (!int.TryParse(Console.ReadLine(), out choice1))
                            {
                                Console.WriteLine("Invalid Input. Please enter a number.");
                                continue; // Go back to the start of the loop
                            }

                            switch (choice1)
                            {
                                case 1:
                                    loggedInMerchant.AddProduct();
                                    break;
                                case 2:
                                    loggedInMerchant.EditProduct();
                                    break;
                                case 3:
                                    loggedInMerchant.DeleteProduct();
                                    break;
                                case 4:
                                    loggedInMerchant.ManageOrder();
                                    break;
                                case 5:
                                    loggedInMerchant.ViewProducts();
                                    break;
                                case 6:
                                    loggedInMerchant.ViewInvoices();
                                    break;
                                case 7:
                                    Console.WriteLine("Logged out successfully!");
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice. Please select a valid option.");
                                    break;
                            }

                        } while (choice1 != 7); // Keep looping until user chooses to logout
                    }
                    else
                    {
                        continue; // Go back to the start of the loop
                    }
                }
                else // Register
                {
                    RegisterMerchant();
                    Console.WriteLine("Do you want to login or register again? (yes/no)");
                    string response = Console.ReadLine().ToLower();
                    if (response != "yes")
                        break; // exit the loop
                }
            } while (true); // Keep looping until user chooses not to
        }

        
        public static void HandleCustomerFlow()
        {
            int loginChoice;
            do
            {
                loginChoice = GetLoginChoice();
                if (loginChoice == 1) // Login
                {
                    Customer loggedInCustomer = CustomerLogin();
                    if (loggedInCustomer != null)
                    {
                        int choice1 = 0;
                        do
                        {
                            Console.WriteLine($"Logged in as {loggedInCustomer.username}");
                            Console.WriteLine("1. Subscribe to Merchant");
                            Console.WriteLine("2. Unsubscribe to Merchant");
                            Console.WriteLine("3. Browse Products");
                            Console.WriteLine("4. Order Product");
                            Console.WriteLine("5. Rate Product");
                            Console.WriteLine("6. View Invoices");
                            Console.WriteLine("7. Logout");
                            if (!int.TryParse(Console.ReadLine(), out choice1))
                            {
                                Console.WriteLine("Invalid Input. Please enter a number");
                                continue; // Go back to the start of the loop
                            }

                            switch (choice1)
                            {
                                case 1:
                                    loggedInCustomer.Subscribe();
                                    break;
                                case 2:
                                    loggedInCustomer.Unsubscribe();
                                    break;
                                case 3:
                                    loggedInCustomer.BrowseProducts();
                                    break;
                                case 4:
                                    loggedInCustomer.OrderProduct();
                                    break;
                                case 5:
                                    loggedInCustomer.RateProduct();
                                    break;
                                case 6:
                                    loggedInCustomer.ViewInvoices();
                                    break;
                                case 7:
                                    Console.WriteLine("Logged out successfully!");
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice. Please select a valid option.");
                                    break;
                            }

                        } while (choice1 != 7); // Keep looping until user chooses to logout
                    }
                    else
                    {
                        continue; // Go back to the start of the loop
                    }
                }
                else // Register
                {
                    RegisterCustomer();
                    Console.WriteLine("Do you want to login or register again? (yes/no)");
                    string response = Console.ReadLine().ToLower();
                    if (response != "yes")
                        break; // exit the loop
                }
            } while (true); // Keep looping until user chooses not to
        }


        public static Admin AdminLogin()
        {
            Console.WriteLine("Enter admin username:");
            string enteredUsername = Console.ReadLine();

            Console.WriteLine("Enter admin password:");
            string enteredPassword = Console.ReadLine();

            foreach (Admin admin in Data.Admins)
            {
                if (admin.username == enteredUsername && admin.password == enteredPassword)
                {
                    Console.WriteLine("Login successful!");
                    return admin;
                }
            }

            Console.WriteLine("Invalid credentials.");
            return null; // returns null if login fails
        }

        public static T RegisterUser<T>() where T : User, new()
        {
            string enteredUsername;
            string enteredEmail;
            string enteredPassword;

            do
            {
                Console.WriteLine("Enter your name:");
                enteredUsername = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(enteredUsername));

            do
            {
                Console.WriteLine("Enter your Email:");
                enteredEmail = Console.ReadLine();

                if (EmailExists(enteredEmail))
                {
                    Console.WriteLine("This email is already registered. Please use a different one.");
                    enteredEmail = null; // Clear the email to loop again
                }
            } while (string.IsNullOrWhiteSpace(enteredEmail));

            do
            {
                Console.WriteLine("Enter your password:");
                enteredPassword = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(enteredPassword));

            T user = new T();
            user.SetDetails(enteredUsername, enteredEmail, enteredPassword);

            return user;
        }

        public static void RegisterMerchant()
        {
            Merchant newMerchant = RegisterUser<Merchant>();
            newMerchant.RegistrationStatus = MerchantStatus.PENDING;

            // Prompt the user to set the merchant category
            Console.WriteLine("Please enter what category of merchant you are.");
            Console.WriteLine("(If your category is invalid, all your product category will be set to Miscellaneous)");
            DisplayMerchantCategories();

            int categoryChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out categoryChoice) || categoryChoice < 1 || categoryChoice > Enum.GetValues(typeof(MerchantCategory)).Length)
                {
                    Console.WriteLine("Invalid choice. Please select a valid merchant category.");
                    DisplayMerchantCategories();
                }
            } while (categoryChoice < 1 || categoryChoice > Enum.GetValues(typeof(MerchantCategory)).Length);

            newMerchant.MerchantCategory = (MerchantCategory)(categoryChoice - 1); // Set the chosen category

            Data.RegisteredMerchants.Add(newMerchant);

            Console.WriteLine("Merchant registration successful!");
        }

        private static void DisplayMerchantCategories()
        {
            Console.WriteLine("Available Merchant Categories:");
            int index = 1;
            foreach (MerchantCategory category in Enum.GetValues(typeof(MerchantCategory)))
            {
                Console.WriteLine($"{index}. {category}");
                index++;
            }
        }

        public static void RegisterCustomer()
        {
            Customer newCustomer = RegisterUser<Customer>();
            newCustomer.CustomerStatus = CustomerStatus.PENDING;

            Data.RegisteredCustomers.Add(newCustomer);

            Console.WriteLine("Customer registration successful!");
        }

        // Other methods to be added

        public static void ApproveMerchant()
        {
            if (Data.RegisteredMerchants.Count == 0)
            {
                Console.WriteLine("No merchants available for approval.");
                return;
            }

            Console.WriteLine("Select a merchant to approve:");

            for (int i = 0; i < Data.RegisteredMerchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.RegisteredMerchants[i].username}");
            }

            int merchantChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out merchantChoice) || merchantChoice < 1 || merchantChoice > Data.RegisteredMerchants.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid merchant.");
                }
            } while (merchantChoice < 1 || merchantChoice > Data.RegisteredMerchants.Count);

            Merchant selectedMerchant = Data.RegisteredMerchants[merchantChoice - 1];
            selectedMerchant.RegistrationStatus = MerchantStatus.APPROVED;

            // Move the merchant from RegisteredMerchants list to the admin's Merchants list
            Admin.Merchants.Add(selectedMerchant);
            Data.RegisteredMerchants.RemoveAt(merchantChoice - 1);

            Console.WriteLine($"Merchant {selectedMerchant.username} approved successfully!");
        }

        public static void RejectMerchant()
        {
            if (Data.RegisteredMerchants.Count == 0)
            {
                Console.WriteLine("No merchants available for rejection.");
                return;
            }

            Console.WriteLine("Select a merchant to reject:");

            for (int i = 0; i < Data.RegisteredMerchants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.RegisteredMerchants[i].username}");
            }

            int merchantChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out merchantChoice) || merchantChoice < 1 || merchantChoice > Data.RegisteredMerchants.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid merchant.");
                }
            } while (merchantChoice < 1 || merchantChoice > Data.RegisteredMerchants.Count);

            Merchant selectedMerchant = Data.RegisteredMerchants[merchantChoice - 1];
            selectedMerchant.RegistrationStatus = MerchantStatus.REJECTED;

            Data.RejectedMerchants.Add(selectedMerchant);
            Data.RegisteredMerchants.RemoveAt(merchantChoice - 1);

            Console.WriteLine($"Merchant {selectedMerchant.username} rejected successfully!");
        }

        public static void ApproveCustomer()
        {
            if (Data.RegisteredCustomers.Count == 0)
            {
                Console.WriteLine("No customers available for approval.");
                return;
            }

            Console.WriteLine("Select a customer to approve:");

            for (int i = 0; i < Data.RegisteredCustomers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.RegisteredCustomers[i].username}");
            }

            int customerChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out customerChoice)
                    || customerChoice < 1 
                    || customerChoice > Data.RegisteredCustomers.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid customer.");
                }
            } while (customerChoice < 1 || customerChoice > Data.RegisteredCustomers.Count);

            Customer selectedCustomer = Data.RegisteredCustomers[customerChoice - 1];
            selectedCustomer.CustomerStatus = CustomerStatus.APPROVED;

            Admin.Customers.Add(selectedCustomer);
            Data.RegisteredCustomers.RemoveAt(customerChoice - 1);


            Console.WriteLine($"Customer {selectedCustomer.username} approved successfully!");
        }

        public static void RejectCustomer()
        {
            if (Data.RegisteredCustomers.Count == 0)
            {
                Console.WriteLine("No customers available for rejection.");
                return;
            }

            Console.WriteLine("Select a customer to reject:");

            for (int i = 0; i < Data.RegisteredCustomers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.RegisteredCustomers[i].username}");
            }

            int customerChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out customerChoice)
                    || customerChoice < 1 
                    || customerChoice > Data.RegisteredCustomers.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid customer.");
                }
            } while (customerChoice < 1 || customerChoice > Data.RegisteredCustomers.Count);

            Customer selectedCustomer = Data.RegisteredCustomers[customerChoice - 1];
            selectedCustomer.CustomerStatus = CustomerStatus.REJECTED;

            Data.RejectedCustomers.Add(selectedCustomer);
            Data.RegisteredCustomers.RemoveAt(customerChoice - 1);

            Console.WriteLine($"Customer {selectedCustomer.username} rejected successfully!");
        }

        public static Customer CustomerLogin()
        {
            if (Admin.Customers.Count == 0)
            {
                Console.WriteLine("No registered users.");
                return null;
            }

            Console.WriteLine("Enter your username:");
            string inputUsername = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            string inputPassword = Console.ReadLine();

            foreach (var customer in Admin.Customers)
            {
                if (customer.username == inputUsername && customer.password == inputPassword)
                {
                    Console.WriteLine($"Welcome back, {inputUsername}!");
                    return customer;
                }
            }

            Console.WriteLine("Invalid username or password.");
            return null;
        }

        public static Merchant MerchantLogin()
        {
            if (Admin.Merchants.Count == 0)
            {
                Console.WriteLine("No registered users.");
                return null;
            }

            Console.WriteLine("Enter your username:");
            string inputUsername = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            string inputPassword = Console.ReadLine();

            foreach (var merchant in Admin.Merchants)
            {
                if (merchant.username == inputUsername && merchant.password == inputPassword)
                {
                    Console.WriteLine($"Welcome back, {inputUsername}!");
                    return merchant;
                }
            }

            Console.WriteLine("Invalid username or password.");
            return null;
        }


        public static Product SelectProductFromRegisteredProducts()
        {
            if (Data.RegisteredProducts.Count == 0)
            {
                Console.WriteLine("No registered products.");
                return null;
            }

            Console.WriteLine("Select a product to approve:");
            for (int i = 0; i < Data.RegisteredProducts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Data.RegisteredProducts[i].Name}");
            }

            int productChoice;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out productChoice) || productChoice < 1 || productChoice > Data.RegisteredProducts.Count)
                {
                    Console.WriteLine("Invalid choice. Please select a valid product.");
                }
            } while (productChoice < 1 || productChoice > Data.RegisteredProducts.Count);

            return Data.RegisteredProducts[productChoice - 1];
        }

        public static bool EmailExists(string email)
        {
            // Check in RegisteredMerchants list
            if (Data.RegisteredMerchants.Any(m => m.email == email))
                return true;

            // Check in RegisteredCustomers list
            if (Data.RegisteredCustomers.Any(c => c.email == email))
                return true;

            // Check in Admin's Merchants list
                if (Admin.Merchants.Any(m => m.email == email))
                    return true;

                // Check in Admin's Customers list
                if (Admin.Customers.Any(c => c.email == email))
                    return true;
            
            return false;
        }
    }
}
