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
                        if (products[i].Matches(product1))
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
                if (product1.Matches(p))
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
        public List<VendingProduct> Unique()
        { //check for unique products always add the first product, then only add the other product once if they do not already exist in the array
            var uniquelist= new List<VendingProduct>();
            bool match=false;
            foreach(VendingProduct product in products)
            {
                if (uniquelist.Count == 0)
                {
                    uniquelist.Add(product);
                }
                else
                {
                    foreach (VendingProduct uniqueproduct in uniquelist)
                    {
                        if (!(product.Matches(uniqueproduct))){ match = false;}
                        else { match = true; }
                    }
                    if (match == false) { uniquelist.Add(product); }
                }                
            }
            return uniquelist;
        }
        public int ProductCountTotal() { return products.Count; }
        public bool ProductExist(VendingProduct expectedproduct)
        {//check for the product in the list and return true if it exists
            foreach(VendingProduct p in products)
            {
                if (p.Matches(expectedproduct))
                {
                    return true;
                }
            }
            return false;
        }
        public static ProductPool operator +(ProductPool a, ProductPool b)
        { //operator adds products from both pool to the summed pool
            ProductPool c = new ProductPool();
            foreach (var product in a.GetProductList())
                c.AddProduct(product);
            foreach (var product in b.GetProductList())
                c.AddProduct(product);
            return c;
        }

        public static ProductPool operator -(ProductPool a, ProductPool b)
        {//check every product in order to subtract(only works bool a has more than, or at least as many items as b)
            ProductPool c = new ProductPool(); 
            foreach (var uniqueproduct in a.Unique()) {
                if (a.ProductExist(uniqueproduct) && b.ProductExist(uniqueproduct))
                {
                    if((a.ProductCountFor(uniqueproduct) - b.ProductCountFor(uniqueproduct))>=0) 
                    {
                        c.AddProduct(uniqueproduct, (a.ProductCountFor(uniqueproduct) - b.ProductCountFor(uniqueproduct)));
                    }
                    else
                    {
                        throw new Exception("Not enough stock");
                    }
                }
            }
            return c;
        }

    }
}
