using System;
using System.Collections.Generic;
using System.Text;

// Mikaela Stanislav / Technical Showcase for Lean Techniques
// December 2020.  mstanislav05@gmail.com

namespace GildedRose
{
    class Item
    {
        public string name { get; set; }
        public int sell_in { get; set; }
        public int quality { get; set; }
        public bool conjuredInd { get; set; }

        //Constructors
        public Item()
        {
            this.name = "";
            this.sell_in = 0;
            this.quality = 0;
            this.conjuredInd = false;
        }

        public Item(string inName, int inSellIn, int inQuality, bool inConjuredInd)
        {
            this.name = inName;
            this.sell_in = inSellIn;
            this.quality = inQuality;
            this.conjuredInd = inConjuredInd;
        }

        public void update_quality(Item inItem)
        {
            inItem.sell_in--;
            //Updates the sell in days and quality according to item sent in
            if (inItem.quality > 0 && inItem.quality < 50)
            {
                int conjuredFactor = 1;
                if(inItem.conjuredInd == true)
                {
                    conjuredFactor = 2;
                }
                switch (inItem.name)
                {
                    case "Aged Brie":
                        inItem.quality++;
                        if (inItem.sell_in <= 0)
                        {
                            inItem.quality = inItem.quality - 2;
                        }
                        break;
                    case "Backstage passes to a TAFKAL80ETC concert":
                        if (inItem.sell_in <= 0)
                        {
                            inItem.quality = 0;
                        }
                        else if (inItem.sell_in <= 10 && inItem.sell_in > 5)
                        {
                            inItem.quality = inItem.quality + 2;
                        }
                        else if (inItem.sell_in <= 5)
                        {
                            inItem.quality = inItem.quality + 3;
                        }
                        break;
                    case "Sulfuras, Hand of Ragnaros":
                        inItem.sell_in++; //undo above sell days decrement
                        break;
                    default:
                        inItem.quality = inItem.quality - (1 * conjuredFactor);
                        if (inItem.sell_in < 0)
                        {
                            inItem.quality = inItem.quality - (1 * conjuredFactor); //degrade twice as fast after sell date
                        }
                        break;
                }
            }
        }
    }
}
