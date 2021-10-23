using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment5_VendingMachine
{
    public class Pen : VendingProduct
    {
        
        public override string Use() { return "Writing something"; }
        public override string GetName()
        {
            string name = "Pen ";
            switch (material)
            {
                case Material.WOOD:
                    name += "(Wood)";
                    break;
                case Material.PLASTIC:
                    name += "(Plastic)";
                    break;
                case Material.METAL:
                    name += "(Metal)";
                    break;
                default:
                    name += "(Unknown)";
                    break;
            }
            return name;
        }
        public enum Material { WOOD,PLASTIC,METAL}
        private Material material;
        public Material GetMaterial() { return material; }
        public override bool Matches(VendingProduct compared) {
            if(this.GetType()!=compared.GetType())
                return false;
            else
            {
                Pen pencompared = (Pen)compared;
                return this.GetMaterial() == pencompared.GetMaterial();
            }
        }
        public Pen(Material material=Material.PLASTIC)
        {
            this.material = material;
            description = "Prompts the customer to take notes and draw";
            if (material == Material.METAL) {
                price = 8;
            }
            else
            {
                price = 6;
            }
            
        }

    }
}
