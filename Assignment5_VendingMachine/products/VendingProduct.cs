using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment5_VendingMachine
{
    public abstract class VendingProduct
    {
        protected int price;
        protected string description;
        public string Examine() //should describe the product
        {
            return "Description:\n" + description + "\nPrice:\n" + price.ToString();
        }
        public abstract string Use(); //implemented in sub classes but not here, we can not use a generic product

        public int GetPrice() //price
        {
            return price;
        }
        public string GetDescription() //description
        {
            return description;
        }
    }
}
