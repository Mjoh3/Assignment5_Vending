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

            productpool.RemoveProducts(new Pen(), 3); 
            productpool.RemoveProducts(new Eraser());
            productpool.RemoveProducts(new Eraser());
            productpool.RemoveProducts(new NotebookXL(),2); //remove 3 pens, 2 erasers and all 2 notebookxls

            bool matches = (productpool.ProductCountFor(new Pen()) == 2) &&
                (productpool.ProductCountFor(new Eraser()) == 3) &&
                (productpool.ProductCountFor(new NotebookXL()) == 0); //check if the calculations of how many should be left for each item
            bool totalmatches = productpool.ProductCountTotal() == 5; //and the total product count
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
    }
}
