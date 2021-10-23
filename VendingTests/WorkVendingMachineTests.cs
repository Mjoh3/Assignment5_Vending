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
            vending.ReStockAll(30); //add 30 of each product;
            vending.InsertMoney(user.cash); //user inserts 200:-
            user.products = vending.Purchase(products); //user purchases for 187:- 
            MoneyPool change = vending.EndTransaction(); //user recieves 13:- return (1 tens, 3 ones)

            int total = change.SumOfAllCash();
            int ones = change.GetNumberOfCash(1);
            int tens = change.GetNumberOfCash(10); //we count the number of coins

            bool correctNumberOfProducts = (user.products.ProductCountFor(new Pen()) == 10)
                && (user.products.ProductCountFor(new Eraser()) == 10)
                && (user.products.ProductCountFor(new NotebookXL()) == 3);

            bool correctNumberOfCoinsReturned = total == 13 && ones == 3 && tens == 1;

            bool stocksLeft = vending.GetInventory().ProductCountFor(new Pen())==20;//30 in stock -10 from purchase

            Assert.True(correctNumberOfCoinsReturned && correctNumberOfProducts && stocksLeft);
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
            vending.ReStockAll(25);
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

            bool correctNumberOfCoinsReturned = total == 65 && fiftys == 1 && tens == 1 && fives == 1;

            bool stocksLeft = vending.GetInventory().ProductCountFor(new Pen()) == 15; //25-10
            Assert.True(correctNumberOfCoinsReturned && correctNumberOfProducts && products==user.products);
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
        public void Showallcontains()
        { //test certain expected to be in the storage after we fill it 
            WorkplaceVendingMachine vending = new WorkplaceVendingMachine();
            vending.ReStockAll(100);
            bool namechecks = vending.ShowAll().Contains("Pen") && 
                vending.ShowAll().Contains("NotebookXL") &&
                vending.ShowAll().Contains("Eraser");
            bool attchecks = vending.ShowAll().Contains("Metal") && vending.ShowAll().Contains("Blank")&& vending.ShowAll().Contains("Small");
            bool pricechecks =vending.ShowAll().Contains("29:-") && vending.ShowAll().Contains("8:-") && vending.ShowAll().Contains("5:-");
            Assert.True(namechecks && attchecks);
        }
        [Fact]
        public void LimitedShowAll()
        {// we should find only metal pen at 8:- nothing else
            WorkplaceVendingMachine vending = new WorkplaceVendingMachine();
            vending.AddSomeProduct(new Pen(Pen.Material.METAL));
            bool metalExist=vending.ShowAll().Contains("Metal");
            bool priceExist6= vending.ShowAll().Contains("6:-");
            bool priceExist8 = vending.ShowAll().Contains("8:-");
            Assert.True(metalExist && priceExist8 && !(priceExist6));
        }
    }
}
