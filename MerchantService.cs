using System;
using SplashKitSDK;

namespace PassTask13
{
    public class MerchantService
    {
        public Merchant RegisterMerchant(string username, string email, string password)
        {
            return new Merchant(username,email,password);
        }
    }
}