using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Program
    {
        public static void Main()
        {
            MainFlow();
        }

        public static void MainFlow()
        {
            while (true)
            {
                int userTypeChoice = UserActions.GetUserTypeChoice();

                switch (userTypeChoice)
                {
                    case UserActions.ADMIN_CHOICE:
                        UserActions.HandleAdminFlow();
                        break;
                    case UserActions.MERCHANT_CHOICE:
                        if (UserActions.HandleMerchantFlow())
                        {
                            continue; // User logged out, show main menu again
                        }
                        break;
                    case UserActions.CUSTOMER_CHOICE:
                        if (UserActions.HandleCustomerFlow())
                        {
                            continue; // User logged out, show main menu again
                        }
                        break;
                }

                Console.WriteLine("Do you want to continue? (yes/no)");
                if (Console.ReadLine().ToLower() != "yes")
                    break; // Exit the while loop and end the program
            }
        }

    }
}
