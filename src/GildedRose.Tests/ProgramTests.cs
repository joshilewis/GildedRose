using System.Collections.Generic;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void SafetyNetTest()
        {
            var expected = new List<Item>()
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 9, Quality = 19},
                new Item {Name = "Aged Brie", SellIn = 1, Quality = 1},
                new Item {Name = "Elixir of the Mongoose", SellIn = 4, Quality = 6},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 14,
                    Quality = 21
                },
                new Item {Name = "Conjured Mana Cake", SellIn = 2, Quality = 4}
            };

            var program = new Program()
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

            program.UpdateQuality();

            Assert.That(program.Items, Is.EquivalentTo(expected).Using(new EqualityComparer()));

        }

        [Test]
        public void ItemSellInAndQualityShouldDecreaseBy1()
        {
            var expected = new List<Item>()
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 9, Quality = 4},
            };
            var initial = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 5},
            };

            ActAndAssert(initial, expected);
        }

        [Test]
        public void ItemQualityShouldNeverBeNegative()
        {
            var expected = new List<Item>()
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 4, Quality = 0},
            };

            var initial = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 5, Quality = 0},
            };

            ActAndAssert(initial, expected);

        }

        [Test]
        public void ItemQualityShouldDegradeTwiceAsFastAfterSellBy()
        {
            var expected = new List<Item>()
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = -1, Quality = 8},
            };

            var initial = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 0, Quality = 10},
            };

            ActAndAssert(initial, expected);
        }

        [Test]
        public void AgedBrieShouldIncreaseInQualityTheOlderItGets()
        {
            var expected = new List<Item>()
            {
                new Item {Name = "Aged Brie", SellIn = 4, Quality = 6},
            };

            var initial = new List<Item>
            {
                new Item {Name = "Aged Brie", SellIn = 5, Quality = 5},
            };

            ActAndAssert(initial, expected);
        }

        [Test]
        public void AgedBrieQualityShouldNeverIncreaseAbove50()
        {
            var expected = new List<Item>()
            {
                new Item {Name = "Aged Brie", SellIn = 4, Quality = 50},
            };

            var initial = new List<Item>
            {
                new Item {Name = "Aged Brie", SellIn = 5, Quality = 50},
            };

            ActAndAssert(initial, expected);
        }

        [Test]
        public void SulfurasItemsShouldNeverChange()
        {
            var expected = new List<Item>()
            {
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            };

            var initial = new List<Item>
            {
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            };

            ActAndAssert(initial, expected);
        }

        [Test]
        public void BackstagePassesQualityShouldIncreaseNormallyBefore10DaysToGo()
        {
            var expected = new List<Item>()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 14, Quality = 21 },
            };

            var initial = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
            };

            ActAndAssert(initial, expected);
        }

        [Test]
        public void BackstagePassesQualityShouldDoubleBetween10And5DaysToGo()
        {
            var expected = new List<Item>()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 9, Quality = 22 },
            };

            var initial = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 },
            };

            ActAndAssert(initial, expected);
        }

        [Test]
        public void BackstagePassesQualityShouldIncreaseBy3Between3And0DaysToGo()
        {
            var expected = new List<Item>()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 3, Quality = 23 },
            };

            var initial = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 4, Quality = 20 },
            };

            ActAndAssert(initial, expected);
        }

        [Test]
        public void BackstagePassesQualityShouldBe0AfterConcert()
        {
            var expected = new List<Item>()
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -1, Quality = 0 },
            };

            var initial = new List<Item>
            {
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 },
            };

            ActAndAssert(initial, expected);
        }

        [Test]
        public void ConjuredItemsQualityShouldDecreaseTwiceAsFast()
        {
            var expected = new List<Item>()
            {
                new Item { Name = "Conjured Mana Cake", SellIn = 2, Quality = 4 }
            };

            var initial = new List<Item>
            {
                new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
            };

            ActAndAssert(initial, expected);
        }

        private void ActAndAssert(List<Item> initial, List<Item> expected)
        {
            var program = new Program()
            {
                Items = initial,
            };

            program.UpdateQuality();

            Assert.That(program.Items, Is.EquivalentTo(expected).Using(new EqualityComparer()));
        }
    }

    public class EqualityComparer : IEqualityComparer<Item>
    {
        public bool Equals(Item x, Item y)
        {
            return x.Name.Equals(y.Name) &&
                   x.Quality.Equals(y.Quality) &&
                   x.SellIn.Equals(y.SellIn)
                ;
        }

        public int GetHashCode(Item obj)
        {
            throw new System.NotImplementedException();
        }
    }
}