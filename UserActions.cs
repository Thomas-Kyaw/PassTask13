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
                    if (!int.TryParse(Console.ReadLine(), out choice1))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }while(choice1 < 1 || choice1 > 9);
                switch (choice1)
                {
                    case 1:
                        ApproveCustomer(loggedInAdmin);
                        break;
                    case 2:
                        RejectCustomer(loggedInAdmin);
                        break;
                    case 3:
                        ApproveMerchant(loggedInAdmin); 
                        break;
                    case 4:
                        RejectMerchant(loggedInAdmin);
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    default:
                        break;
                }
            }
        }

        public static void HandleMerchantFlow()
        {
            int loginChoice;
            do
            {
                loginChoice = GetLoginChoice();
                if (loginChoice == 1)
                {
                    // Handle merchant login...
                }
                else
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
                if (loginChoice == 1)
                {
                    // Handle customer login...
                }
                else
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

            Data.RegisteredMerchants.Add(newMerchant);

            Console.WriteLine("Merchant registration successful!");
        }

        public static void RegisterCustomer()
        {
            Customer newCustomer = RegisterUser<Customer>();
            newCustomer.CustomerStatus = CustomerStatus.PENDING;

            Data.RegisteredCustomers.Add(newCustomer);

            Console.WriteLine("Customer registration successful!");
        }

        // Other methods to be added

        public static void ApproveMerchant(Admin admin)
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
            admin.Merchants.Add(selectedMerchant);
            Data.RegisteredMerchants.RemoveAt(merchantChoice - 1);

            Console.WriteLine($"Merchant {selectedMerchant.username} approved successfully!");
        }

        public static void RejectMerchant(Admin admin)
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

        public static void ApproveCustomer(Admin admin)
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

            admin.Customers.Add(selectedCustomer);
            Data.RegisteredCustomers.RemoveAt(customerChoice - 1);


            Console.WriteLine($"Customer {selectedCustomer.username} approved successfully!");
        }

        public static void RejectCustomer(Admin admin)
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


        public static bool EmailExists(string email)
        {
            // Check in RegisteredMerchants list
            if (Data.RegisteredMerchants.Any(m => m.email == email))
                return true;

            // Check in RegisteredCustomers list
            if (Data.RegisteredCustomers.Any(c => c.email == email))
                return true;

            // Check in Admin's Merchants list
            foreach (Admin admin in Data.Admins)
            {
                if (admin.Merchants.Any(m => m.email == email))
                    return true;

                // Check in Admin's Customers list
                if (admin.Customers.Any(c => c.email == email))
                    return true;
            }
            
            return false;
        }
    }
}