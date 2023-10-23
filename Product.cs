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
        private List<float> ratings = new List<float>();
        private ProductStatus status;

        public Product(Merchant merchant) 
        {
            id = Data.NextProductId++;
            status = ProductStatus.PENDING;
            this.merchant = merchant;
            category = OthersCategory.MISC.ToString();
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

        public List<float> Ratings
        {
            get{return ratings;}
            set{ratings = value;}
        }
        public string Category
        {
            get { return category; }
            set
            {
                bool isValidCategory = false;
                switch (this.Merchant.MerchantCategory)
                {
                    case MerchantCategory.FOODS:
                        isValidCategory = Enum.TryParse(typeof(FoodCategory), value, true, out _);
                        break;
                    case MerchantCategory.ELECTRONICS:
                        isValidCategory = Enum.TryParse(typeof(ElectronicsCategory), value, true, out _);
                        break;
                    case MerchantCategory.TOYS:
                        isValidCategory = Enum.TryParse(typeof(ToysCategory), value, true, out _);
                        break;
                    case MerchantCategory.ENTERTAINMENT:
                        isValidCategory = Enum.TryParse(typeof(EntertainmentCategory), value, true, out _);
                        break;
                    case MerchantCategory.FASHION:
                        isValidCategory = Enum.TryParse(typeof(FashionCategory), value, true, out _);
                        break;
                    case MerchantCategory.LEISURE:
                        isValidCategory = Enum.TryParse(typeof(LeisureCategory), value, true, out _);
                        break;
                    case MerchantCategory.OTHERS:
                        isValidCategory = Enum.TryParse(typeof(OthersCategory), value, true, out _);
                        break;
                    default:
                        isValidCategory = false;
                        break;
                }

                if (isValidCategory)
                {
                    category = value;
                }
                else
                {
                    category = OthersCategory.MISC.ToString();
                    Console.WriteLine($"Invalid category. Setting product category to {OthersCategory.MISC}.");
                }
            }
        }
        public void AddRating(float rating)
        {
            ratings.Add(rating);
        }

        public float CalculateAverageRating()
        {
            if (ratings.Count == 0) return 0; // Return 0 if there are no ratings

            float total = 0;
            foreach(float rating in ratings)
            {
                total += rating;
            }
            return total / ratings.Count;
        }

    }
}
