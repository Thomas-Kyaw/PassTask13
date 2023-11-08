using System;
using System.Collections.Generic;
using SplashKitSDK;
using NUnit.Framework;

namespace PassTask13
{
    [TestFixture()]
    public class Test()
    {
        [Test()]
        public void TestApproveProduct()
        {
            Admin admin = Data.Admins.First();
            Merchant testMerchant = new Merchant("Merchant 1", "test@email.com", "123456");
            Product testProduct = new Product(testMerchant);
            Data.RegisteredProducts.Add(testProduct);
            
            admin.ApproveProduct(testProduct);

            // Assert
            Assert.AreEqual(ProductStatus.APPROVED, testProduct.Status);
            Assert.Contains(testProduct, testMerchant.Products);
        }
        [Test()]
        public void RejectProduct()
        {
            Admin admin = Data.Admins.First();
            Merchant testMerchant = new Merchant("Merchant 1", "test@email.com", "123456");
            Product testProduct = new Product(testMerchant);
            Data.RegisteredProducts.Add(testProduct);
            
            admin.RejectProduct(testProduct);

            // Assert
            Assert.AreEqual(ProductStatus.REJECTED, testProduct.Status);
            Assert.Contains(testProduct, Data.RejectedProducts);
        }
        [Test()]
        public void TestTotalRating()
        {
            Merchant testMerchant = new Merchant("Merchant 1", "test@email.com", "123456");
            Product testProduct = new Product(testMerchant);
            testProduct.AddRating(2);
            testProduct.AddRating(4);

            Assert.AreEqual(testProduct.CalculateAverageRating(), 3);
        }
        [Test]
        public void TestRating()
        {
            Merchant testMerchant = new Merchant("Merchant 1", "test@email.com", "123456");
            Product testProduct = new Product(testMerchant);
            testProduct.AddRating(2);
            testProduct.AddRating(4);

            Assert.Contains(4, testProduct.Ratings);
            Assert.Contains(2, testProduct.Ratings);
        }
    }
}
