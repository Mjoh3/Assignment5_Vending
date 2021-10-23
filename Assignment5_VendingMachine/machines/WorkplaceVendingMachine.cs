using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment5_VendingMachine
{
    public class WorkplaceVendingMachine : IVending
    {
        private ProductPool inventory=new ProductPool();
        public ProductPool GetInventory() { return inventory; }
        public void ReStockAll(int amount)
        {//just restocks everything we have right now in the store
            inventory.ClearProducts();
            inventory.AddProduct(new Pen(Pen.Material.WOOD),amount);
            inventory.AddProduct(new Pen(Pen.Material.METAL),amount);
            inventory.AddProduct(new Pen(Pen.Material.PLASTIC),amount);
            inventory.AddProduct(new NotebookXL(NotebookXL.PageDesigns.BLANK),amount);
            inventory.AddProduct(new NotebookXL(NotebookXL.PageDesigns.LINES),amount);
            inventory.AddProduct(new NotebookXL(NotebookXL.PageDesigns.SQUARES), amount);
            inventory.AddProduct(new Eraser(true),amount);
            inventory.AddProduct(new Eraser(false),amount);
        }
        public void AddSomeProduct(VendingProduct product,int amount=1)
        {
           
            inventory.AddProduct(product, amount);
        }
        private MoneyPool inserted=new MoneyPool();
        private int leftovers=0;
        public MoneyPool GetInserted() { return inserted; }
        public int GetLeftovers() { return leftovers; }
        
        public string ShowAll() { //just shows the available products
            List<String> fullItemDescriptions= new List<string>();
            foreach (VendingProduct uniqueproduct in inventory.Unique())
                fullItemDescriptions.Add(uniqueproduct.GetName()+ " " + uniqueproduct.GetPrice()+":- "+uniqueproduct.GetDescription());
            fullItemDescriptions.Sort();

            string showall = "";
            foreach (string itemDescription in fullItemDescriptions)
            {
                showall += itemDescription + "\n";
            }

            return showall;
        }
        public ProductPool Purchase(ProductPool wantedProducts) 
        {
            if((inserted.SumOfAllCash() - wantedProducts.PriceSum()) < 0) //you can not purchase if you lack the funds
            {
                throw new Exception("you can not purchase, need more money");
            }
            else//leftovers int should be the difference of the inserted cash sum and the prices of the purchased products
            {
                leftovers = inserted.SumOfAllCash() - wantedProducts.PriceSum();
            }
            
            inserted = new MoneyPool();//clean inserted money and give user the product
            inventory -= wantedProducts;
            return wantedProducts; 
        }
        public MoneyPool EndTransaction() { //get the leftovers int and converts it to moneypool
            MoneyPool change=new MoneyPool();
            int index = 0;
            while (leftovers >= 0 && index<MoneyPool.MoneyValues.Length) //we need to run this aslong as leftovers is higher than 0 and protect index for going out of range
            {
                if ((leftovers - MoneyPool.MoneyValues[index]) >= 0) //checks if there will be enough leftovers before we add to the moneypool and subtracts from the leftover int
                {
                    change.AddCashFor(MoneyPool.MoneyValues[index]);
                    leftovers -= MoneyPool.MoneyValues[index];
                }
                else { index++; } //othervise go to the next index (type of cash) from the moneypool values
            }
            if (leftovers != 0) //it should be exactly 0 left othervise something went wrong with the calculation
            {
                throw new Exception("Somthing went wrong with the calculation"); 
            }
            return change; 
        }
        public void InsertMoney(MoneyPool inputmoney) //summerize using the overloaded operators
        {
            inserted += inputmoney;
        }
    }
}
