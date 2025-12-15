using System;
using System.Collections.Generic;

namespace SizzleSyncPOS
{
    /// <summary>
    /// MenuSystem manages restaurant menu items using 1D Arrays, 2D Arrays, and Jagged Arrays.
    /// Demonstrates various array types for organizing and displaying menu categories and items.
    /// </summary>
    public class MenuSystem
    {
        // 1D Array: Menu item names
        private string[] menuItems;

        // 1D Array: Menu item prices (parallel to menuItems)
        private decimal[] menuPrices;

        // 1D Array: Menu item categories
        private string[] menuCategories;

        // 2D Array: Menu board layout showing item arrangement (4 categories x 5 items per category)
        private string[,] menuBoard;

        // Jagged Array: Special add-ons or variations for each menu item
        // Each item can have different number of add-ons
        private string[][] itemAddOns;

        public MenuSystem()
        {
            InitializeMenu();
        }

        /// <summary>
        /// Initializes all array data structures with restaurant menu data.
        /// </summary>
        private void InitializeMenu()
        {
            // Initialize 1D Arrays for menu items, prices, and categories
            menuItems = new string[]
            {
                // Appetizers (0-4)
                "Spring Rolls",
                "Chicken Wings",
                "Calamari",
                "Nachos",
                "Garlic Bread",
                
                // Main Courses (5-9)
                "Grilled Chicken",
                "Beef Steak",
                "Pork Chop",
                "Fish Fillet",
                "Pasta Carbonara",
                
                // Desserts (10-14)
                "Chocolate Cake",
                "Ice Cream",
                "Halo-Halo",
                "Leche Flan",
                "Fruit Salad",
                
                // Beverages (15-19)
                "Iced Tea",
                "Soft Drinks",
                "Fresh Juice",
                "Coffee",
                "Milkshake"
            };

            menuPrices = new decimal[]
            {
                // Appetizers
                120.00m, 150.00m, 180.00m, 160.00m, 90.00m,
                
                // Main Courses
                220.00m, 350.00m, 280.00m, 260.00m, 240.00m,
                
                // Desserts
                110.00m, 80.00m, 120.00m, 95.00m, 100.00m,
                
                // Beverages
                50.00m, 45.00m, 70.00m, 65.00m, 85.00m
            };

            menuCategories = new string[]
            {
                "Appetizers", "Appetizers", "Appetizers", "Appetizers", "Appetizers",
                "Main Course", "Main Course", "Main Course", "Main Course", "Main Course",
                "Desserts", "Desserts", "Desserts", "Desserts", "Desserts",
                "Beverages", "Beverages", "Beverages", "Beverages", "Beverages"
            };

            // Initialize 2D Array for menu board display (4 categories x 5 items)
            menuBoard = new string[4, 5]
            {
                { "Spring Rolls", "Chicken Wings", "Calamari", "Nachos", "Garlic Bread" },
                { "Grilled Chicken", "Beef Steak", "Pork Chop", "Fish Fillet", "Pasta Carbonara" },
                { "Chocolate Cake", "Ice Cream", "Halo-Halo", "Leche Flan", "Fruit Salad" },
                { "Iced Tea", "Soft Drinks", "Fresh Juice", "Coffee", "Milkshake" }
            };

            // Initialize Jagged Array for item add-ons/variations
            itemAddOns = new string[20][];

            // Appetizers add-ons
            itemAddOns[0] = new string[] { "Sweet Chili Sauce", "Garlic Mayo" };
            itemAddOns[1] = new string[] { "Buffalo", "BBQ", "Honey Garlic" };
            itemAddOns[2] = new string[] { "Mayonnaise", "Vinegar" };
            itemAddOns[3] = new string[] { "Extra Cheese", "Jalapeños", "Sour Cream" };
            itemAddOns[4] = new string[] { "Extra Butter", "Parmesan" };

            // Main courses add-ons
            itemAddOns[5] = new string[] { "Garlic Rice", "Plain Rice" };
            itemAddOns[6] = new string[] { "Mushroom Sauce", "Pepper Sauce", "Garlic Rice" };
            itemAddOns[7] = new string[] { "Gravy", "Steamed Veggies" };
            itemAddOns[8] = new string[] { "Lemon Butter", "Rice" };
            itemAddOns[9] = new string[] { "Extra Cheese", "Garlic Bread" };

            // Desserts add-ons
            itemAddOns[10] = new string[] { "Extra Frosting", "Sprinkles" };
            itemAddOns[11] = new string[] { "Chocolate", "Vanilla", "Ube", "Mango" };
            itemAddOns[12] = new string[] { "Extra Leche Flan", "Extra Ice Cream" };
            itemAddOns[13] = new string[] { "Caramel Drizzle" };
            itemAddOns[14] = new string[] { "Extra Cream" };

            // Beverages add-ons
            itemAddOns[15] = new string[] { "Lemon", "Extra Ice" };
            itemAddOns[16] = new string[] { "Coke", "Sprite", "Royal" };
            itemAddOns[17] = new string[] { "Orange", "Mango", "Pineapple", "Watermelon" };
            itemAddOns[18] = new string[] { "Black", "With Cream", "Iced" };
            itemAddOns[19] = new string[] { "Chocolate", "Strawberry", "Vanilla", "Mango" };
        }

        /// <summary>
        /// Displays the complete restaurant menu.
        /// </summary>
        public void DisplayMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                 SIZZLESYNC RESTAURANT MENU                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            string currentCategory = "";
            for (int i = 0; i < menuItems.Length; i++)
            {
                // Print category header when category changes
                if (menuCategories[i] != currentCategory)
                {
                    currentCategory = menuCategories[i];
                    Console.WriteLine($"\n===== {currentCategory.ToUpper()} =====");
                }

                Console.WriteLine($"[{i + 1,2}] {menuItems[i],-25} PHP{menuPrices[i],7:F2}");
            }

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              MENU BOARD LAYOUT (2D Array)                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            string[] categoryNames = { "APPETIZERS", "MAIN COURSES", "DESSERTS", "BEVERAGES" };

            for (int row = 0; row < menuBoard.GetLength(0); row++)
            {
                Console.WriteLine($"{categoryNames[row]}:");
                // Print two items per line with extra spacing; handle odd count
                for (int col = 0; col < menuBoard.GetLength(1); col += 2)
                {
                    string left = menuBoard[row, col];
                    string right = (col + 1 < menuBoard.GetLength(1)) ? menuBoard[row, col + 1] : null;

                    if (right != null)
                    {
                        // Three leading spaces, left column padded to align the right column
                        Console.WriteLine($"   {left,-24}{right}");
                    }
                    else
                    {
                        Console.WriteLine($"   {left}");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║            ITEM ADD-ONS/VARIATIONS (Jagged Array)          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");


            for (int i = 0; i < itemAddOns.Length; i++)
            {
                // Print item name
                Console.WriteLine($"{menuItems[i]}");

                // Print add-ons on the next line with indentation, each prefixed by '#'
                if (itemAddOns[i] != null && itemAddOns[i].Length > 0)
                {
                    string addons = string.Join(", ", itemAddOns[i].Select(a => $"#{a}"));
                    Console.WriteLine($"   {addons}");
                }

                // Blank line between entries for readability
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Gets a menu item name by its number (1-based).
        /// </summary>
        public string GetItemName(int itemNumber)
        {
            int index = itemNumber - 1;
            if (index >= 0 && index < menuItems.Length)
            {
                return menuItems[index];
            }
            return null;
        }

        /// <summary>
        /// Gets a menu item price by its number (1-based).
        /// </summary>
        public decimal GetItemPrice(int itemNumber)
        {
            int index = itemNumber - 1;
            if (index >= 0 && index < menuPrices.Length)
            {
                return menuPrices[index];
            }
            return 0;
        }

        /// <summary>
        /// Gets the total number of menu items.
        /// </summary>
        public int GetMenuItemCount()
        {
            return menuItems.Length;
        }

        /// <summary>
        /// Gets the category of a menu item.
        /// </summary>
        public string GetItemCategory(int itemNumber)
        {
            int index = itemNumber - 1;
            if (index >= 0 && index < menuCategories.Length)
            {
                return menuCategories[index];
            }
            return null;
        }

        /// <summary>
        /// Gets add-ons for a specific menu item.
        /// </summary>
        public string[] GetItemAddOns(int itemNumber)
        {
            int index = itemNumber - 1;
            if (index >= 0 && index < itemAddOns.Length)
            {
                return itemAddOns[index];
            }
            return null;
        }

        /// <summary>
        /// Creates a price dictionary for quick lookups.
        /// </summary>
        public Dictionary<string, decimal> GetPriceDictionary()
        {
            Dictionary<string, decimal> priceDict = new Dictionary<string, decimal>();
            for (int i = 0; i < menuItems.Length; i++)
            {
                priceDict[menuItems[i]] = menuPrices[i];
            }
            return priceDict;
        }
    }
}