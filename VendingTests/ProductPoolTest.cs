using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Assignment5_VendingMachine;
namespace VendingTests
{
   public class ProductPoolTest
    {
        [Fact]
        public void AddAndSummarizeTest()
        {
            ProductPool productpool = new ProductPool();
            productpool.AddProduct(new Pen(),5);
            productpool.AddProduct(new Eraser(),4);
            productpool.AddProduct(new NotebookXL()); //Add 5 pens, 4 erasers and one notebookxl
            bool matches = (productpool.ProductCountFor(new Pen()) == 5) &&
                (productpool.ProductCountFor(new Eraser()) == 4) &&
                (productpool.ProductCountFor(new NotebookXL()) == 1); //check if every product count is correct
            bool totalmatches = productpool.ProductCountTotal() == 10; // check if the total is 10
            Assert.True(totalmatches && matches);
        }
        [Fact]
        public void RemoveAndSummarizeTest()
        {
            ProductPool productpool = new ProductPool();
            productpool.AddProduct(new Pen(), 5);
            productpool.AddProduct(new Eraser(), 5);
            productpool.AddProduct(new NotebookXL(),2); //add 5 pens, 5 erasers and 2 notebookxls

            productpool.RemoveProducts(new Pen()); 
            productpool.RemoveProducts(new Eraser(),2);
           // productpool.RemoveProducts(new Eraser());
            productpool.RemoveProducts(new NotebookXL(),2); //remove 1 pens, 2 erasers and all 2 notebookxls
           // productpool.RemoveProducts(new NotebookXL());

            bool matches = (productpool.ProductCountFor(new Pen()) == 4) &&
                (productpool.ProductCountFor(new Eraser()) == 3) &&
                (productpool.ProductCountFor(new NotebookXL()) == 0); //check if the calculations of how many should be left for each item
            bool totalmatches = productpool.ProductCountTotal() == 7; //and the total product count
            Assert.True(totalmatches && matches);
            
        }
        [Fact]
        public void ClearTest()
        {
            ProductPool productpool = new ProductPool();

            productpool.AddProduct(new Eraser(), 5);
            int fiveerasers = productpool.ProductCountTotal(); //add 5 erasers and save number of total products

            productpool.ClearProducts();
            int clearedproducts = productpool.ProductCountTotal(); //remove everything and save number of total products

            productpool.AddProduct(new Pen(), 1);
            int addedpen= productpool.ProductCountTotal(); //add a pen and save the number of total products
            Assert.True(fiveerasers == 5 && clearedproducts == 0 && addedpen == 1); //check if everything was as we expected at that stage
        }
        [Fact]
        public void RemoveTooMany()
        {
            ProductPool productpool = new ProductPool();
            productpool.AddProduct(new Eraser(), 5);
            productpool.AddProduct(new NotebookXL(), 1);//add 5 erasers and 1 notebookxl
            Assert.Throws<Exception>(()=>productpool.RemoveProducts(new Eraser(), 6)); //we can not remove 6 erasers if we have only 5
        }
        [Fact]
        public void RemoveNegative()
        {
            ProductPool productpool = new ProductPool();
            productpool.AddProduct(new NotebookXL(), 10);
            Assert.Throws<Exception>(() => productpool.RemoveProducts(new NotebookXL(), -6)); //no negative numbers are allowed in removeproduct calculation

        }
        [Fact]
        public void AddNegative()
        {
            ProductPool productpool = new ProductPool();
            Assert.Throws<Exception>(() => productpool.AddProduct(new Pen(), -1)); //can not add negative numbers
        }
        [Theory]
        [InlineData(5, 10, 4)]
        [InlineData(1, 2, 4)]
        [InlineData(100, 1, 42)]
        public void SumPrice(int pencount, int erasercount, int notebookxlcount)
        {
            ProductPool productpool = new ProductPool();
            productpool.AddProduct(new Pen(), pencount);
            productpool.AddProduct(new Eraser(), erasercount);
            productpool.AddProduct(new NotebookXL(), notebookxlcount);
            int expectedsum =
                ((new Eraser()).GetPrice() * erasercount) +
                ((new NotebookXL()).GetPrice() * notebookxlcount) +
                ((new Pen()).GetPrice() * pencount);
            Assert.Equal(expectedsum, productpool.PriceSum()); //we need to check the sum function as well so that the total is what we expect it to be
        }
        [Fact]
        public void UniqueTest()
        { //check to see if we had three unique items
            ProductPool pool = new ProductPool();
            pool.AddProduct(new Pen(Pen.Material.WOOD), 8);
            pool.AddProduct(new Pen(Pen.Material.PLASTIC), 4);
            pool.RemoveProducts(new Pen(Pen.Material.WOOD), 7);
            pool.AddProduct(new NotebookXL());
            int amountOfItems = pool.Unique().Count;
            Assert.Equal(3, amountOfItems);
        }

        [Fact]
        public void PlusTest()
        { //check to see if the plus function works
            ProductPool pool1 = new ProductPool();
            pool1.AddProduct(new Pen(Pen.Material.WOOD), 10);
            pool1.AddProduct(new Pen(Pen.Material.PLASTIC), 10);
            
            pool1.AddProduct(new NotebookXL(NotebookXL.PageDesigns.BLANK),10);
            pool1.AddProduct(new NotebookXL(NotebookXL.PageDesigns.LINES), 10);

            ProductPool pool2 = new ProductPool();

            pool2.AddProduct(new Pen(Pen.Material.WOOD), 1);
            pool2.AddProduct(new Pen(Pen.Material.PLASTIC), 2);

            pool2.AddProduct(new NotebookXL(NotebookXL.PageDesigns.BLANK), 5);
            pool2.AddProduct(new NotebookXL(NotebookXL.PageDesigns.LINES), 5);
            pool1 += pool2;
            bool checktotalcount = pool1.ProductCountTotal()==53;
            bool checkspecificcount = (pool1.ProductCountFor(new Pen(Pen.Material.WOOD)) == 11) &&
                (pool1.ProductCountFor(new Pen(Pen.Material.PLASTIC)) == 12) &&
                (pool1.ProductCountFor(new NotebookXL(NotebookXL.PageDesigns.BLANK)) == 15) &&
                (pool1.ProductCountFor(new NotebookXL(NotebookXL.PageDesigns.LINES)) == 15);
            Assert.True(checktotalcount && checkspecificcount);
            //Assert.Equal(3, amountOfItems);
        }
        [Fact]
        public void MinusTest()
        { //check to see if the minus function works
            ProductPool pool1 = new ProductPool();
            pool1.AddProduct(new Pen(Pen.Material.WOOD), 10);
            pool1.AddProduct(new Pen(Pen.Material.PLASTIC), 10);

            pool1.AddProduct(new NotebookXL(NotebookXL.PageDesigns.BLANK), 10);
            pool1.AddProduct(new NotebookXL(NotebookXL.PageDesigns.LINES), 10);

            ProductPool pool2 = new ProductPool();

            pool2.AddProduct(new Pen(Pen.Material.WOOD), 1);
            pool2.AddProduct(new Pen(Pen.Material.PLASTIC), 2);

            pool2.AddProduct(new NotebookXL(NotebookXL.PageDesigns.BLANK), 5);
            pool2.AddProduct(new NotebookXL(NotebookXL.PageDesigns.LINES), 5);
            pool1 -= pool2;
            bool checktotalcount = pool1.ProductCountTotal() == 27;
            bool checkspecificcount = (pool1.ProductCountFor(new Pen(Pen.Material.WOOD)) == 9) &&
                (pool1.ProductCountFor(new Pen(Pen.Material.PLASTIC)) == 8) &&
                (pool1.ProductCountFor(new NotebookXL(NotebookXL.PageDesigns.BLANK)) == 5) &&
                (pool1.ProductCountFor(new NotebookXL(NotebookXL.PageDesigns.LINES)) == 5);
            Assert.True(checktotalcount && checkspecificcount);

        }
        [Fact]
        public void MinusError()
        { //test if the programs thows an error if I try to remove more products
            ProductPool pool1 = new ProductPool();
            pool1.AddProduct(new Pen(Pen.Material.WOOD), 10);
            ProductPool pool2 = new ProductPool();
            pool2.AddProduct(new Pen(Pen.Material.WOOD), 101);

            Assert.Throws<Exception>(()=>pool1-pool2);
            
        }


    }
}
