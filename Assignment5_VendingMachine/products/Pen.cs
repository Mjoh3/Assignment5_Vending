using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment5_VendingMachine
{
    public class Pen : VendingProduct
    {
        public override string Use() { return "Writing something"; }
        public Pen()
        {
            description = "Prompts the customer to take notes and draw";
            price = 6;
        }
    }
}
