using System;
using System.Collections.Generic;
using System.Text;
using Assignment5_VendingMachine;
using Xunit;
namespace VendingTests
{
    public class WorkVendingMachineTests
    {
        private (MoneyPool cash, ProductPool products) user; //my bootleg user class is a tuple :)
        [Fact]
        public void Procedure1() //this is a normal procedure
        {
            user.products = new ProductPool();
            user.cash = new MoneyPool();
            user.cash.AddCashFor(100, 2);//empty product pool and 200kr
            
            ProductPool products = new ProductPool();
            products.AddProduct(new Pen(), 10); //60
            products.AddProduct(new Eraser(), 10); //40
            products.AddProduct(new NotebookXL(), 3); //87 //total cost 187


            WorkplaceVendingMachine vending = new WorkplaceVendingMachine();
            vending.InsertMoney(user.cash); //user inserts 200:-
            user.products = vending.Purchase(products); //user purchases for 187:- 
            MoneyPool change = vending.EndTransaction(); //user recieves 13:- return (1 tens, 3 ones)

            int total = change.SumOfAllCash();
            int ones = change.GetNumberOfCash(1);
            int tens = change.GetNumberOfCash(10); //we count the number of coins

            bool correctNumberOfProducts = (user.products.ProductCountFor(new Pen()) == 10)
                && (user.products.ProductCountFor(new Eraser()) == 10)
                && (user.products.ProductCountFor(new NotebookXL()) == 3);

            Assert.True(total == 13 && ones == 3 && tens == 1 && correctNumberOfProducts);
        }
        [Fact]
        public void Procedure2() //similar to last time but this time we insert money twice in order to test the input and summarizes
        {//we also wants to see that the user has got its product
            user.products = new ProductPool();
            user.cash = new MoneyPool();
            user.cash.AddCashFor(500, 1);
            user.cash.AddCashFor(50, 1); //user inserts 550 to machine
            
            MoneyPool otherMoney = new MoneyPool();
            otherMoney.AddCashFor(5); //the user found other 5 , and inserts to the machine
            
            ProductPool products = new ProductPool();
            products.AddProduct(new Pen(), 20); //120
            products.AddProduct(new Eraser(), 20); //80
            products.AddProduct(new NotebookXL(), 10); //290 //total cost: 490


            WorkplaceVendingMachine vending = new WorkplaceVendingMachine();
            vending.InsertMoney(user.cash);
            vending.InsertMoney(otherMoney);
            user.products = vending.Purchase(products);
            MoneyPool change = vending.EndTransaction();

            int total = change.SumOfAllCash();
            int fiftys = change.GetNumberOfCash(50);
            int tens = change.GetNumberOfCash(10);
            int fives= change.GetNumberOfCash(5); //we count the number of coins

            bool correctNumberOfProducts = (user.products.ProductCountFor(new Pen()) == 20)
                && (user.products.ProductCountFor(new Eraser()) == 20)
                && (user.products.ProductCountFor(new NotebookXL()) == 10); //returns true if it is what we expect

            Assert.True(total == 65 && fiftys == 1 && tens == 1 && fives==1 && correctNumberOfProducts && products==user.products);
        }
        [Fact]
        public void BrokeCustomer() //we test if a user has not enough funds
        {
            user.products = new ProductPool();
            user.cash = new MoneyPool();
            user.cash.AddCashFor(1, 2); //user has 2:-
            
            ProductPool products = new ProductPool();

            products.AddProduct(new NotebookXL(), 1); //wants to buy for 29 lol

            WorkplaceVendingMachine vending = new WorkplaceVendingMachine();
            vending.InsertMoney(user.cash); 
            Assert.Throws<Exception>(()=>user.products = vending.Purchase(products)); //Rejected
            
        }
        [Fact]
        public void ShowAllContains() //check if show all contain at least everything it should
        {
            WorkplaceVendingMachine vending = new WorkplaceVendingMachine();
            bool containsAllPrices = vending.ShowAll().Contains(new Pen().GetPrice().ToString()) &&
                vending.ShowAll().Contains(new Eraser().GetPrice().ToString()) &&
                vending.ShowAll().Contains(new NotebookXL().GetPrice().ToString());
            bool containsAllDesc = vending.ShowAll().Contains(new Pen().GetDescription()) &&
                vending.ShowAll().Contains(new Eraser().GetDescription()) &&
                vending.ShowAll().Contains(new NotebookXL().GetDescription());
            Assert.True(containsAllDesc && containsAllPrices);
        }
    }
}
