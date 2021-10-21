using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment5_VendingMachine
{
    public class ProductPool
    {
        private List<VendingProduct> products;
        public ProductPool()
        {
            products = new List<VendingProduct>();
        }
        public List<VendingProduct> GetProductList() { return products; }
        public void ClearProducts() { products = new List<VendingProduct>(); } //remove all products
        public void AddProduct(VendingProduct product1,int amount=1) //just adds product of a specific type as long as the number is positive
        {
            if (amount < 0)
            {
                throw new Exception("you can not add negative amount of products");
            }
            else
            {
                for (int i = 0; i < amount; i++)
                    products.Add(product1);
            }
            
        }
        public void RemoveProducts(VendingProduct product1,int amount=1) //just the removal function, might come in handy if the user changes its mind
        {
            if (amount < 0)
            {
                throw new Exception("you can not remove negative amount of products");
            }
            else if(amount > ProductCountFor(product1))
            {
                throw new Exception("not enough product to remove");
            }
            else
            {
                int count = 0;
                while (count<amount)
                {
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].GetType() == product1.GetType())
                        {
                            products.RemoveAt(i);
                            count++;
                            break;
                        }
                    }
                }
               
                
            }
        }
        public int ProductCountFor(VendingProduct product1) //counts the sum of products of a certain prouctgroup
        {
            int count = 0;
            foreach (VendingProduct p in products)
            {
                if (product1.GetType() == p.GetType())
                    count++;
            }
            return count;
        }
        public int PriceSum() //just count the sum of all the product PRICES in the list (what the user needs to pay)
        {
            int pricesum = 0;
            foreach(VendingProduct p in products)
            {
                pricesum += p.GetPrice();
            }
            return pricesum;

        }
        public int ProductCountTotal() { return products.Count; }
    }
}
