using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Product
    {
        private int id;
        private string name;
        private string category;
        private float price;
        private Merchant merchant;
        private List<int> ratings = new List<int>();
        private ProductStatus status;

        public Product(Merchant merchant) 
        {
            id = Data.NextProductId++;
            status = ProductStatus.PENDING;
            this.merchant = merchant;
        }
        public int Id
        {
            get{return id;}
        }

        public ProductStatus Status
        {
            get{return status;}
            set{status = value;}
        }

        public string Name
        {
            get{return name;}
            set{name = value;}
        }
        public string Category
        {
            get{return category;}
            set{category = value;}
        }
        public float Price
        {
            get{return price;}
            set{price = value;}
        }

        public Merchant Merchant
        {
            get { return merchant; }
            set { merchant = value; }
        }
        public void AddRating(int rating)
        {

        }
    }
}
