using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment5_VendingMachine
{
    public interface IVending
    {
        public MoneyPool EndTransaction(); //end transaction should return some money for the user
        public string ShowAll(); // just show all available items
        public ProductPool Purchase(ProductPool wantedProducts); //purchase method should just return to the user products
        public void InsertMoney(MoneyPool userMoney); //just inserts into the machine, no need to return anything
    }
}
