using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        public IList<Item> Items;

        private static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }

            };

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.SellIn = item.SellIn - 1;
                }

                ChangeQualityBasedOnName(item);

                ChangeQualityBasedOnSellIn(item);
            }
        }

        private void ChangeQualityBasedOnSellIn(Item item)
        {
            if (item.SellIn >= 0) return;

            switch (item.Name)
            {
                case "Aged Brie":
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                    return;
                case "Backstage passes to a TAFKAL80ETC concert":
                    item.Quality = item.Quality - item.Quality;
                    return;
                default:
                    if (item.Quality <= 0) return;

                    if (item.Name != "Sulfuras, Hand of Ragnaros")
                    {
                        item.Quality = item.Quality - 1;
                    }
                    return;
            }
        }

        private void ChangeQualityBasedOnName(Item item)
        {
            switch (item.Name)
            {
                case "Aged Brie":
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                    return;
                case "Backstage passes to a TAFKAL80ETC concert":
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;

                        if (item.SellIn < 11)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality = item.Quality + 1;
                            }
                        }

                        if (item.SellIn < 6)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality = item.Quality + 1;
                            }
                        }
                    }
                    return;
                case "Conjured Mana Cake":
                    item.Quality = item.Quality - 2;
                    return;
                default:
                    if (item.Quality <= 0) return;

                    if (item.Name != "Sulfuras, Hand of Ragnaros")
                    {
                        item.Quality = item.Quality - 1;
                    }
                    return;
            }

        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
