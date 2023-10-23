using System;
using SplashKitSDK;

namespace PassTask13
{
    public static class Data
    {
        public static List<Admin> Admins = new List<Admin>
        {
            new Admin("admin1", "admin1@email.com", "password1"),
            new Admin("admin2", "admin2@email.com", "password2")
            // TODO - Add more if I want to
        };
        public static List<Merchant> RegisteredMerchants = new List<Merchant>();
        public static List<Merchant> RejectedMerchants = new List<Merchant>();
        public static List<Customer> RegisteredCustomers = new List<Customer>();
        public static List<Customer> RejectedCustomers = new List<Customer>();

        public static int NextProductId = 1;

        public static int NextOrderId = 1;

        public static int NextInvoiceId = 1;

        public static List<Product> RegisteredProducts = new List<Product>();
        public static List<Product> RejectedProducts = new List<Product>();
        public static List<Order> CancelledOrders = new List<Order>();
    }
}
