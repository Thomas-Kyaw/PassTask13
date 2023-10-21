using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Program
    {
        public static void Main()
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
                        UserActions.HandleMerchantFlow();
                        break;
                    case UserActions.CUSTOMER_CHOICE:
                        UserActions.HandleCustomerFlow();
                        break;
                }

                Console.WriteLine("Do you want to continue? (yes/no)");
                if (Console.ReadLine().ToLower() != "yes")
                    break; // Exit the while loop and end the program
            }
        }

    }
}
