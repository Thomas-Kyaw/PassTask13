using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Invoice
    {
        private int id;
        private Merchant merchant;
        
        public Invoice()
        {
            id = Data.NextInvoiceId++;
        }
        public float CalculateTotal()
        {
            
        }
    }
}