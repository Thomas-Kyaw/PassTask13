using System;
using SplashKitSDK;

namespace PassTask13
{
    public class Product
    {
        /// <summary>
        /// unique id for product
        /// </summary>
        private int id;
        /// <summary>
        /// name of the product
        /// </summary>
        private string name;
        /// <summary>
        /// category of the product(it will be categorized correctly by the setter)
        /// </summary>
        private string category;
        /// <summary>
        /// price of the product
        /// </summary>
        private float price;
        /// <summary>
        /// Merchant who owns the merchant
        /// </summary>
        private Merchant merchant;
        /// <summary>
        /// list of ratings the product has recieved
        /// </summary>
        private List<float> ratings = new List<float>();
        /// <summary>
        /// status of the product(approved or not)
        /// </summary>
        private ProductStatus status;
        /// <summary>
        /// constructor of the product
        /// </summary>
        /// <param name="merchant"></param>
        public Product(Merchant merchant) 
        {
            id = Data.NextProductId++;
            status = ProductStatus.PENDING;
            this.merchant = merchant;
            category = OthersCategory.MISC.ToString();
        }
        /// <summary>
        /// read only property of id
        /// </summary>
        public int Id
        {
            get{return id;}
        }
        /// <summary>
        /// property for status
        /// </summary>
        public ProductStatus Status
        {
            get{return status;}
            set{status = value;}
        }
        /// <summary>
        /// property for name
        /// </summary>
        public string Name
        {
            get{return name;}
            set{name = value;}
        }
        /// <summary>
        /// property for price
        /// </summary>
        public float Price
        {
            get{return price;}
            set{price = value;}
        }
        /// <summary>
        /// property for merchant
        /// </summary>
        public Merchant Merchant
        {
            get { return merchant; }
            set { merchant = value; }
        }
        /// <summary>
        /// property for ratings
        /// </summary>
        public List<float> Ratings
        {
            get{return ratings;}
            set{ratings = value;}
        }
        /// <summary>
        /// property for category
        /// the setter here makes sure the input category is in the category list otherwise set the default
        /// </summary>
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
        /// <summary>
        /// adding rating for this product
        /// </summary>
        /// <param name="rating"></param>
        public void AddRating(float rating)
        {
            ratings.Add(rating);
        }
        /// <summary>
        /// average rating of the product
        /// </summary>
        /// <returns></returns>
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
