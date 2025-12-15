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
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            string currentCategory = "";
            for (int i = 0; i < menuItems.Length; i++)
            {
                // Print category header when category changes
                if (menuCategories[i] != currentCategory)
                {
                    currentCategory = menuCategories[i];
                    Console.WriteLine($"\n┌─────────────────────────────────────────────────────────┐");
                    Console.WriteLine($"│  {currentCategory.ToUpper(),-54}│");
                    Console.WriteLine($"└─────────────────────────────────────────────────────────┘");
                }

                // Print item number, name, and price on the main line
                Console.WriteLine($"\n [{i + 1,2}] {menuItems[i],-30} PHP {menuPrices[i],6:F2}");

                // Print add-ons/variations indented below the item, each on its own line
                if (itemAddOns[i] != null && itemAddOns[i].Length > 0)
                {
                    for (int j = 0; j < itemAddOns[i].Length; j++)
                    {
                        Console.WriteLine($"      w/ {itemAddOns[i][j]}");
                    }
                }
            }

            Console.WriteLine("\n════════════════════════════════════════════════════════════");
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
        /// Displays variations for an item and prompts user to select one.
        /// Returns the selected variation or null if no variations or user skips.
        /// </summary>
        public string SelectVariation(int itemNumber)
        {
            int index = itemNumber - 1;
            if (index < 0 || index >= itemAddOns.Length || itemAddOns[index] == null || itemAddOns[index].Length == 0)
            {
                return null;
            }

            Console.WriteLine($"\nSelect variation for {menuItems[index]}:");
            Console.WriteLine("  [0] No variation (plain)");
            
            for (int i = 0; i < itemAddOns[index].Length; i++)
            {
                Console.WriteLine($"  [{i + 1}] w/ {itemAddOns[index][i]}");
            }

            Console.Write("\nYour choice: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            if (int.TryParse(input, out int choice))
            {
                if (choice == 0)
                {
                    return null; // No variation
                }
                else if (choice >= 1 && choice <= itemAddOns[index].Length)
                {
                    return itemAddOns[index][choice - 1];
                }
                else
                {
                    Console.WriteLine("Invalid choice. No variation selected.");
                    return null;
                }
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