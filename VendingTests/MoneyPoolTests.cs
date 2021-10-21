using System;
using Xunit;
using Assignment5_VendingMachine;
namespace VendingTests
{
    public class MoneyPoolTests
    {
        MoneyPool p;
        [Theory]
        [InlineData(1000,0)]
        [InlineData(500, 1)]
        [InlineData(100, 2)]
        [InlineData(50, 3)]
        [InlineData(20, 4)]
        [InlineData(10, 5)]
        [InlineData(5, 6)]
        [InlineData(1, 7)]
        public void MoneyValueIndex(int value, int expected_index)
        {
           
            Assert.Equal(value, MoneyPool.MoneyValues[expected_index]);  //just checking the dictionary intex contra array index
        }
        
        [Theory]
        [InlineData(1000, 0)]
        [InlineData(500, 1)]
        [InlineData(100, 2)]
        [InlineData(1, 7)]
        public void MoneyAdder(int value, int number)
        {
            p = new MoneyPool();
            
            p.AddCashFor(value, number);
            p.AddCashFor(value); //add the selected value to the moneypool, both with 1 and 2 parameters
            Assert.Equal(value * (number+1), p.SumOfCashFor(value)); //and check if the sum calculation is correct
        }
        [Fact]
        public void AddingNegativeMoney()
        {
            p = new MoneyPool();
            Assert.Throws<Exception>(() => p.AddCashFor(50, -1)); //addcash should not accept negative values
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Indexerror(int functionnumber)
        {
            p = new MoneyPool();
            switch (functionnumber)
            {
                case 1:
                    Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => p.AddCashFor(19));
                    break;
                case 2:
                    Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => p.GetNumberOfCash(42));
                    break;
                case 3:
                    Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => p.ClearCashFor(174));
                    break;
                case 4:
                    Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => p.SumOfCashFor(303));
                    break;
                default:
                    break;
            }
            //we dont want values that we dont have, weare checking the methods that uses value as index. there are no 19 values for example
        }
        [Fact]
        public void SumAllCashTest() // add a bunch of cash together
        {
            p = new MoneyPool();
            p.AddCashFor(1000); //+1000
            p.AddCashFor(100,3);//+300
            p.AddCashFor(10,3); //+30
            p.AddCashFor(5); //+5
            p.AddCashFor(1, 2); //+2
            Assert.Equal(1337, p.SumOfAllCash()); //sum should be 1337 lol
        }
        
        [Theory]
        [InlineData(1000,10)]
        [InlineData(500,1)]
        [InlineData(100,0)]
        [InlineData(1,0)]
        public void ClearSomething(int value, int numberofcash) //we should check if we can just clear something  for example all 100s and 1s
        {
            p = new MoneyPool();
            p.AddCashFor(1000, 10); //10000
            p.AddCashFor(500);      //500
            p.AddCashFor(100, 2);   //200
            p.AddCashFor(1, 5);     //5 
            p.ClearCashFor(100);    //-200 =0
            p.ClearCashFor(1);      //-5 =0
            Assert.Equal(numberofcash, p.GetNumberOfCash(value));
           
        }
        [Fact]
        public void Summarize() //we need to test the operator overload method
        {
            var a = new MoneyPool();
            var b = new MoneyPool();

            a.AddCashFor(10);
            a.AddCashFor(1); //add 11:- to moneypool a

            b.AddCashFor(10, 3); 
            b.AddCashFor(1, 4); //add 34:- to moneypool b

            MoneyPool c = a + b; //moneypool c should now be 45:- with 4 tens and 5 ones
            bool onesCorrectSum = c.GetNumberOfCash(1)==(a.GetNumberOfCash(1) + b.GetNumberOfCash(1));
            bool tensCorrectSum = c.GetNumberOfCash(10) == (a.GetNumberOfCash(10) + b.GetNumberOfCash(10));
            bool totalsCorrectSum = c.SumOfAllCash() == (a.SumOfAllCash() + b.SumOfAllCash()); //45
            Assert.True(totalsCorrectSum && onesCorrectSum && totalsCorrectSum);
        }
    }
}
