using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment5_VendingMachine
{
    public class NotebookXL : VendingProduct 
    {
        public override string Use() 
        {
            return "Opening notebook XL in order to write";
        }
        public NotebookXL()
        {
            price = 29;
            description = "Big notebook to write notes in";
        }
    }
}
