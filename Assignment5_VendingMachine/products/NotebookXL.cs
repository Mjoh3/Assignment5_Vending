using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment5_VendingMachine
{
    public class NotebookXL : VendingProduct 
    {
        
        public enum PageDesigns {BLANK,LINES,SQUARES}
        private PageDesigns pageDesign;
        public PageDesigns GetPageDesign() { return pageDesign; }
        public override string Use() 
        {
            return "Opening notebook XL in order to write";
        }
        public override string GetName()
        {
            string name = "NotebookXL ";
            switch (pageDesign)
            {
                case PageDesigns.BLANK:
                    name += "(Blank)";
                    break;
                case PageDesigns.LINES:
                    name += "(Lines)";
                    break;
                case PageDesigns.SQUARES:
                    name += "(Squares)";
                    break;
                default:
                    name += "(Unknown)";
                    break;
            }
            return name;
        }
        public override bool Matches(VendingProduct compared)
        {
            if (this.GetType() != compared.GetType())
                return false;
            else
            {
                NotebookXL notebookcompared = (NotebookXL)compared;
                return this.GetPageDesign() == notebookcompared.GetPageDesign();
            }
        }
        public NotebookXL(PageDesigns pageDesign=PageDesigns.LINES)
        {
            this.pageDesign = pageDesign;
            price = 29;
            switch (pageDesign)
            {
                case PageDesigns.BLANK:
                    description = "Big notebook to draw in";
                    break;
                case PageDesigns.LINES:
                    description = "Big notebook to write in";
                    break;
                case PageDesigns.SQUARES:
                    description= "Big notebook for calculatiton";
                    break;
                default:
                    description = "How am I suppose to use this";
                    break;
            }
            
        }
    }
}
