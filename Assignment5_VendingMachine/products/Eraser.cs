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
        public Eraser()
        {
            description = "Prompts the customer to erase mistake";
            price = 4;
        }
    }
}
