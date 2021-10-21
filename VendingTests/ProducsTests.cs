using System;
using Xunit;
using Assignment5_VendingMachine;
using System.Collections.Generic;

namespace VendingTests
{
   public class ProducsTests
    {
        [Fact]
        public void differentDescriptions() //the description for the products should not match each other
        {
            VendingProduct pen;
            VendingProduct eraser;
            VendingProduct notebookxl;
            pen = new Pen();
            eraser = new Eraser();
            notebookxl = new NotebookXL();

            bool differentDescription = 
                (pen.GetDescription() != eraser.GetDescription()) &&
                (pen.GetDescription() != notebookxl.GetDescription()) &&
                (eraser.GetDescription() != notebookxl.GetDescription());

            Assert.True(differentDescription);
        }
        [Fact]
        public void abstractionOrNot() //just a bit of check if you declare it as vendingproduct or the specific one matters
        {
            VendingProduct pen1= new Pen();
            VendingProduct eraser1 = new Eraser();
            VendingProduct notebookxl1=new NotebookXL();
            Pen pen2 = new Pen();
            Eraser eraser2=new Eraser();
            NotebookXL notebookxl2= new NotebookXL();
            
            bool sameprice = 
                (pen1.GetPrice() == pen2.GetPrice()) && 
                (eraser1.GetPrice() == eraser2.GetPrice()) && 
                (notebookxl1.GetPrice() == notebookxl2.GetPrice());
            bool samedescription = 
                (pen1.GetDescription() == pen2.GetDescription()) &&
                (eraser1.GetDescription() == eraser2.GetDescription()) &&
                (notebookxl1.GetDescription() == notebookxl2.GetDescription());
      
            
            Assert.True(sameprice && samedescription);
        }

        
        [Theory]
        [InlineData("Writing something",1)]
        [InlineData("Erasing the things you wrote", 2)]
        [InlineData("Opening notebook XL in order to write",3)]
        [InlineData("Opening notebook XL in order to write", 5)]
        public void UseCorrectly(string use, int choice) //just check the right behaviour of the use function
        {
            VendingProduct product;
            switch (choice)
            {
                case 1:
                    product = new Pen();
                    break;
                case 2:
                    product = new Eraser();
                    break;
                case 3:
                    product = new NotebookXL();
                    break;
                default:
                    product = new NotebookXL();
                    break;
            }

            Assert.Equal(use, product.Use());
        }
        [Fact]
        public void ExamineContains() //check if examine contains everything we want 
        {
            VendingProduct pen1 = new Pen();
            VendingProduct eraser1 = new Eraser();
            VendingProduct notebookxl1 = new NotebookXL();

            bool penDescriptionsInExamine = pen1.Examine().Contains(pen1.GetDescription());
            bool eraserDescriptionsInExamine = eraser1.Examine().Contains(eraser1.GetDescription());
            bool notebookxlDescriptionsInExamine = notebookxl1.Examine().Contains(notebookxl1.GetDescription());

            bool penPriceInExamine = pen1.Examine().Contains(pen1.GetPrice().ToString());
            bool eraserPriceInExamine = eraser1.Examine().Contains(eraser1.GetPrice().ToString());
            bool notebookxlPriceInExamine = notebookxl1.Examine().Contains(notebookxl1.GetPrice().ToString());

            bool allDescriptionsInExamine = penDescriptionsInExamine && eraserDescriptionsInExamine && notebookxlDescriptionsInExamine;
            bool allPriceInExamine = penPriceInExamine && eraserPriceInExamine && notebookxlPriceInExamine;
            Assert.True(allDescriptionsInExamine && allPriceInExamine);
        }
    }

}
