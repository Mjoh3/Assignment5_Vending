using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment5_VendingMachine
{
    public class Eraser : VendingProduct
    {
        public override string Use()
        {
            return "Erasing the things you wrote";
        }
        public override string GetName()
        {
            string name = "Eraser ";
            name += isLarge ? "(Large)" : "(Small)";
            return name;
        }
        private bool isLarge;
        public bool IsLarge() { return isLarge; }

        public override bool Matches(VendingProduct compared)
        {
            if (this.GetType() != compared.GetType())
                return false;
            else
            {
                Eraser pencompared = (Eraser)compared;
                return this.IsLarge() == pencompared.IsLarge();
            }
        }
        public Eraser(bool large=false)
        {
            isLarge = large;
            description = "Prompts the customer to erase mistake";
            
            price =  large ? 5:4;
        }
    }
}
