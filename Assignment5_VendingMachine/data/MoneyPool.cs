using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment5_VendingMachine
{
    public class MoneyPool
    {
        public static readonly int[] MoneyValues = { 1000,500,100,50,20,10,5,1};
        
        private Dictionary<int, int> NumberOfCash; 
        public Dictionary<int, int> GetAllNumberOfCash() { return NumberOfCash; } //if we need to look at the dictionary at some point outside the class
        public MoneyPool()
        {            
            InitilazeCash();
        }
        private void InitilazeCash() //Just a clear method to fix a new empty dictionary of all the different values
        {
            NumberOfCash = new Dictionary<int, int>();
            foreach (int valuekey in MoneyValues)
            {
                NumberOfCash.Add(valuekey, 0);
            }
        }
        public int GetNumberOfCash(int value) //just access the number of a specific value
        {
            return NumberOfCash[value];
        }
        public void AddCashFor(int value, int amount=1) //add as many money as you want, no negative values allowed
        {
            if (amount < 0)
                throw new Exception("can not accept negative numbers");
            else
                NumberOfCash[value] += amount;
        }
        
        public void ClearCashFor(int value) //clear cash for a specific value
        {
            NumberOfCash[value] -= NumberOfCash[value];
        }
        public int SumOfCashFor(int value) //count cash for a specific value
        {
            return value * NumberOfCash[value];
        }
        public int SumOfAllCash() //count the number of cash
        {
            int sum = 0;
            foreach(int value in MoneyValues)
            {
                sum += value * NumberOfCash[value];
            }
            return sum;
        }
        public static MoneyPool operator +(MoneyPool a, MoneyPool b) //operator overloader should summarize all of the cash values
        {
            MoneyPool c = new MoneyPool();
            for(int i=0; i<MoneyValues.Length; i++)
            {
                c.AddCashFor(MoneyValues[i], a.GetNumberOfCash(MoneyValues[i]));
                c.AddCashFor(MoneyValues[i], b.GetNumberOfCash(MoneyValues[i]));
            }
            return c;
        }
    }
}
