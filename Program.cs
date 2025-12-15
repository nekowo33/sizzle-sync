using System;
using System.Collections.Generic;
using System.IO;

namespace SizzleSyncPOS
{
    /// <summary>
    /// Main Program - SizzleSync Restaurant POS Console Application
    /// Demonstrates use of Stack (OrderSystem), Queue (OrderQueue), and Arrays (MenuSystem)
    /// Addresses all objectives from the capstone proposal:
    /// - Order management with structured sequence (Queue)
    /// - Ability to modify/correct recent entries (Stack)
    /// - Local file storage for sales records (File I/O)
    /// - Daily sales summaries and order logs
    /// </summary>
    class Program
    {
        // Global instances for the POS system
        private static MenuSystem menu = null!;
        private static OrderQueue orderQueue = null!;
        private static OrderSystem? currentOrder;
        private static List<CompletedOrder> completedOrders = null!;
        private static string? currentCustomer;
        private static string? currentTable;
        private static int currentOrderNumber;

        static void Main(string[] args)
        {
            InitializeSystem();
            RunMainLoop();
        }

        /// <summary>
        /// Initializes all system components.
        /// </summary>
        private static void InitializeSystem()
        {
            menu = new MenuSystem();
            orderQueue = new OrderQueue();
            completedOrders = new List<CompletedOrder>();
            currentOrder = null;
            currentCustomer = null;
            currentTable = null;

            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    SIZZLESYNC POS SYSTEM                   ║");
            Console.WriteLine("║         Console-Based Restaurant Management System         ║");
            Console.WriteLine("║                  Data Structures Project                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        }

        /// <summary>
        /// Main application loop - continues until user chooses to exit.
        /// </summary>
        private static void RunMainLoop()
        {
            bool isRunning = true;

            do
            {
                DisplayMainMenu();
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        CreateNewOrder();
                        break;

                    case "2":
                        AddItemsToOrder();
                        break;

                    case "3":
                        RemoveLastItemFromOrder();
                        break;

                    case "4":
                        ViewCurrentOrder();
                        break;

                    case "5":
                        CompleteOrder();
                        break;

                    case "6":
                        ViewPendingOrders();
                        break;

                    case "7":
                        ProcessNextOrder();
                        break;

                    case "8":
                        ViewMenu();
                        break;

                    case "9":
                        ViewDailySalesSummary();
                        break;

                    case "10":
                        SaveDailySalesToFile();
                        break;

                    case "11":
                        isRunning = false;
                        ExitApplication();
                        break;

                    default:
                        Console.WriteLine("\nInvalid option. Please select a number from 1-11.\n");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (isRunning);
        }

        /// <summary>
        /// Displays the main menu.
        /// </summary>
        private static void DisplayMainMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                        MAIN MENU                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine("  [1]  Create New Order (Add to Queue)");
            Console.WriteLine("  [2]  Add Items to Current Order (Stack Push)");
            Console.WriteLine("  [3]  Remove Last Item (Stack Pop - Correction Feature)");
            Console.WriteLine("  [4]  View Current Order Details");
            Console.WriteLine("  [5]  Complete Order & Generate Receipt");
            Console.WriteLine("  [6]  View Pending Orders Queue (Queue Display)");
            Console.WriteLine("  [7]  Process Next Order (Queue Dequeue)");
            Console.WriteLine("  [8]  Display Restaurant Menu (Arrays)");
            Console.WriteLine("  [9]  View Daily Sales Summary");
            Console.WriteLine("  [10] Save Sales Records to File (Local Storage)");
            Console.WriteLine("  [11] Exit Application");
            Console.WriteLine("════════════════════════════════════════════════════════════");

            if (currentCustomer != null)
            {
                Console.WriteLine($"Current Order: #{currentOrderNumber} | Customer: {currentCustomer} | Table: {currentTable}");
                Console.WriteLine($"Items in Order: {currentOrder?.GetItemCount() ?? 0}");
                Console.WriteLine("════════════════════════════════════════════════════════════");
            }

            Console.Write("\nEnter your choice (1-11): ");
        }

        /// <summary>
        /// Case 1: Create a new order and add to queue.
        /// Demonstrates Queue Enqueue operation.
        /// </summary>
        private static void CreateNewOrder()
        {
            Console.WriteLine("\n--- CREATE NEW ORDER ---");
            Console.Write("Enter customer name: ");
            string? customerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(customerName))
            {
                Console.WriteLine("Error: Customer name cannot be empty.");
                return;
            }

            Console.Write("Enter table number: ");
            string? tableNumber = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(tableNumber))
            {
                Console.WriteLine("Error: Table number cannot be empty.");
                return;
            }

            int orderNum = orderQueue.AddOrder(customerName, tableNumber);

            if (orderNum > 0)
            {
                // If no current order, process this one immediately from the queue
                if (currentCustomer == null)
                {
                    PendingOrder pendingOrder = orderQueue.ProcessNextOrder();
                    if (pendingOrder != null)
                    {
                        currentCustomer = pendingOrder.CustomerName;
                        currentTable = pendingOrder.TableNumber;
                        currentOrderNumber = pendingOrder.OrderNumber;
                        currentOrder = new OrderSystem(pendingOrder.OrderNumber);
                        Console.WriteLine($"\n✓ Order #{orderNum} is now active. Ready to add items!");
                    }
                }
                else
                {
                    Console.WriteLine($"\n✓ Order #{orderNum} added to queue. Complete current order first.");
                }
            }
        }

        /// <summary>
        /// Case 2: Add items to the current order.
        /// Demonstrates Stack Push operation.
        /// </summary>
        private static void AddItemsToOrder()
        {
            if (currentCustomer == null || currentOrder == null)
            {
                Console.WriteLine("\n No active order. Please create a new order first (Option 1).");
                return;
            }

            Console.WriteLine($"\n--- TAKING ORDER FOR: {currentCustomer.ToUpper()} (Table {currentTable}) ---");
            menu.DisplayMenu();

            bool addingItems = true;

            while (addingItems)
            {
                Console.Write("\nEnter menu item number (or 0 to finish): ");
                string? input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                
                if (int.TryParse(input, out int itemNumber))
                {
                    if (itemNumber == 0)
                    {
                        addingItems = false;
                    }
                    else if (itemNumber >= 1 && itemNumber <= menu.GetMenuItemCount())
                    {
                        string itemName = menu.GetItemName(itemNumber);
                        decimal itemPrice = menu.GetItemPrice(itemNumber);

                        // Ask for variation if available
                        string? variation = menu.SelectVariation(itemNumber);
                        string fullItemName = itemName;
                        if (!string.IsNullOrWhiteSpace(variation))
                        {
                            fullItemName = $"{itemName} w/ {variation}";
                        }

                        Console.Write($"Enter quantity for {fullItemName}: ");
                        string? qtyInput = Console.ReadLine();
                        
                        if (string.IsNullOrWhiteSpace(qtyInput))
                        {
                            Console.WriteLine("Error: Quantity cannot be empty.");
                            continue;
                        }
                        
                        if (int.TryParse(qtyInput, out int quantity))
                        {
                            if (quantity > 0 && quantity <= 100)
                            {
                                currentOrder.AddItem(fullItemName, itemPrice, quantity);
                            }
                            else if (quantity > 100)
                            {
                                Console.WriteLine("Error: Quantity cannot exceed 100.");
                            }
                            else
                            {
                                Console.WriteLine("Error: Quantity must be greater than zero.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Please enter a valid number for quantity.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: Please enter a number between 1 and {menu.GetMenuItemCount()}.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter a valid number.");
                }
            }
        }

        /// <summary>
        /// Case 3: Remove the last added item (correction feature).
        /// Demonstrates Stack Pop operation - addresses "modify, correct, or remove recent entries."
        /// </summary>
        private static void RemoveLastItemFromOrder()
        {
            if (currentCustomer == null || currentOrder == null)
            {
                Console.WriteLine("\nNo active order to modify.");
                return;
            }

            Console.WriteLine($"\n--- REMOVE LAST ITEM FROM ORDER #{currentOrderNumber} ---");
            currentOrder.RemoveLastItem(); // Stack Pop operation
        }

        /// <summary>
        /// Case 4: View current order details.
        /// </summary>
        private static void ViewCurrentOrder()
        {
            if (currentCustomer == null || currentOrder == null)
            {
                Console.WriteLine("\nNo active order.");
                return;
            }

            Console.WriteLine($"\n--- ORDER DETAILS ---");
            Console.WriteLine($"Order Number: #{currentOrderNumber}");
            Console.WriteLine($"Customer: {currentCustomer}");
            Console.WriteLine($"Table: {currentTable}");
            currentOrder.ViewOrder();
            Console.WriteLine($"Current Total: PHP{currentOrder.CalculateTotal():F2}");
        }

        /// <summary>
        /// Case 5: Complete the order and generate receipt.
        /// Demonstrates integration of Stack (get all items) and file storage.
        /// </summary>
        private static void CompleteOrder()
        {
            if (currentCustomer == null || currentOrder == null)
            {
                Console.WriteLine("\nNo active order to complete.");
                return;
            }

            if (currentOrder.GetItemCount() == 0)
            {
                Console.WriteLine("\nOrder is empty. Add items before completing.");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    COMPLETING ORDER                        ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            decimal total = currentOrder.CalculateTotal();
            List<OrderItem> items = currentOrder.GetAllItems();

            // Print receipt (null checks already done at method start)
            PrintReceipt(currentOrderNumber, currentCustomer!, currentTable!, items, total);

            // Save to completed orders
            CompletedOrder completed = new CompletedOrder
            {
                OrderNumber = currentOrderNumber,
                CustomerName = currentCustomer!,
                TableNumber = currentTable!,
                Items = items,
                Total = total,
                CompletedTime = DateTime.Now
            };
            completedOrders.Add(completed);

            Console.WriteLine($"\n✓ Order #{currentOrderNumber} completed and saved!");

            // Clear current order
            currentOrder = null;
            currentCustomer = null;
            currentTable = null;
            currentOrderNumber = 0;

            // Check if there are more orders in queue
            if (!orderQueue.IsEmpty())
            {
                Console.WriteLine("\n--- Next Order Ready ---");
                Console.Write("Process next order from queue? (y/n): ");
                string? response = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(response) && response.ToLower() == "y")
                {
                    ProcessNextOrder();
                }
            }
        }

        /// <summary>
        /// Prints a formatted receipt.
        /// </summary>
        private static void PrintReceipt(int orderNum, string customer, string table, List<OrderItem> items, decimal total)
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    SIZZLESYNC RECEIPT                      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine($"Order #: {orderNum}");
            Console.WriteLine($"Customer: {customer.ToUpper()}");
            Console.WriteLine($"Table: {table}");
            Console.WriteLine($"Date/Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("────────────────────────────────────────────────────────────");
            Console.WriteLine("ITEMS ORDERED:");
            Console.WriteLine("────────────────────────────────────────────────────────────");

            int itemNumber = 1;
            foreach (OrderItem item in items)
            {
                Console.WriteLine($"  {itemNumber}. {item.ItemName,-25} x{item.Quantity}");
                Console.WriteLine($"     PHP{item.Price:F2} each = PHP{item.Subtotal:F2}");
                itemNumber++;
            }

            Console.WriteLine("────────────────────────────────────────────────────────────");
            Console.WriteLine($"TOTAL ITEMS: {items.Count}");
            Console.WriteLine($"TOTAL AMOUNT: PHP{total:F2}");
            Console.WriteLine("────────────────────────────────────────────────────────────");
            Console.WriteLine("         THANK YOU FOR DINING WITH US!".ToUpper());
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        }

        /// <summary>
        /// Case 6: View all pending orders in queue.
        /// </summary>
        private static void ViewPendingOrders()
        {
            Console.WriteLine("\n--- PENDING ORDERS QUEUE ---");
            orderQueue.ViewPendingOrders();
        }

        /// <summary>
        /// Case 7: Process the next order from queue.
        /// Demonstrates Queue Dequeue operation.
        /// </summary>
        private static void ProcessNextOrder()
        {
            if (currentCustomer != null)
            {
                Console.WriteLine("\nComplete current order before processing next one.");
                return;
            }

            Console.WriteLine("\n--- PROCESS NEXT ORDER ---");
            PendingOrder nextOrder = orderQueue.ProcessNextOrder(); // Queue Dequeue

            if (nextOrder != null)
            {
                currentCustomer = nextOrder.CustomerName;
                currentTable = nextOrder.TableNumber;
                currentOrderNumber = nextOrder.OrderNumber;
                currentOrder = new OrderSystem(nextOrder.OrderNumber);
                Console.WriteLine($"\n✓ Order #{currentOrderNumber} is now active. Ready to add items!");
            }
        }

        /// <summary>
        /// Case 8: Display restaurant menu.
        /// </summary>
        private static void ViewMenu()
        {
            menu.DisplayMenu();
        }

        /// <summary>
        /// Case 9: View daily sales summary.
        /// Addresses "generate daily sales summaries" objective.
        /// </summary>
        private static void ViewDailySalesSummary()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   DAILY SALES SUMMARY                      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd}");
            Console.WriteLine($"Total Orders Completed: {completedOrders.Count}");
            Console.WriteLine("────────────────────────────────────────────────────────────");

            if (completedOrders.Count == 0)
            {
                Console.WriteLine("No completed orders yet today.");
                return;
            }

            decimal totalSales = 0;
            int totalItems = 0;

            foreach (var order in completedOrders)
            {
                Console.WriteLine($"Order #{order.OrderNumber} - {order.CustomerName} (Table {order.TableNumber})");
                Console.WriteLine($"  Time: {order.CompletedTime:HH:mm:ss} | Total: ₱{order.Total:F2}");
                totalSales += order.Total;
                totalItems += order.Items.Count;
            }

            Console.WriteLine("────────────────────────────────────────────────────────────");
            Console.WriteLine($"TOTAL SALES: PHP{totalSales:F2}");
            decimal averageOrder = completedOrders.Count > 0 ? totalSales / completedOrders.Count : 0;
            Console.WriteLine($"AVERAGE ORDER: PHP{averageOrder:F2}");
            Console.WriteLine($"TOTAL ITEMS SOLD: {totalItems}");
            Console.WriteLine("═══════════════════════════════════════════════════════════=");
        }

        /// <summary>
        /// Case 10: Save sales records to local file.
        /// Addresses "implement simple storage method using local file handling" objective.
        /// </summary>
        private static void SaveDailySalesToFile()
        {
            Console.WriteLine("\n--- SAVE SALES RECORDS TO FILE ---");

            if (completedOrders.Count == 0)
            {
                Console.WriteLine("No sales data to save.");
                return;
            }

            try
            {
                string fileName = $"SizzleSync_Sales_{DateTime.Now:yyyyMMdd}.txt";
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                
                using (StreamWriter writer = new StreamWriter(fullPath))
                {
                    writer.WriteLine("╔════════════════════════════════════════════════════════════╗");
                    writer.WriteLine("║           SIZZLESYNC DAILY SALES REPORT                    ║");
                    writer.WriteLine("╚════════════════════════════════════════════════════════════╝");
                    writer.WriteLine($"Report Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    writer.WriteLine($"Total Orders: {completedOrders.Count}");
                    writer.WriteLine("════════════════════════════════════════════════════════════\n");

                    decimal totalSales = 0;

                    foreach (var order in completedOrders)
                    {
                        if (order == null) continue;
                        
                        writer.WriteLine($"ORDER #{order.OrderNumber}");
                        writer.WriteLine($"Customer: {order.CustomerName ?? "N/A"} | Table: {order.TableNumber ?? "N/A"}");
                        writer.WriteLine($"Completed: {order.CompletedTime:yyyy-MM-dd HH:mm:ss}");
                        writer.WriteLine("Items:");

                        if (order.Items != null)
                        {
                            foreach (var item in order.Items)
                            {
                                if (item != null)
                                {
                                    writer.WriteLine($"  - {item.ItemName} x{item.Quantity} @ ₱{item.Price:F2} = ₱{item.Subtotal:F2}");
                                }
                            }
                        }

                        writer.WriteLine($"Order Total: PHP{order.Total:F2}");
                        writer.WriteLine("────────────────────────────────────────────────────────────");

                        totalSales += order.Total;
                    }

                    writer.WriteLine($"\nTOTAL DAILY SALES: PHP{totalSales:F2}");
                    decimal averageOrderValue = completedOrders.Count > 0 ? totalSales / completedOrders.Count : 0;
                    writer.WriteLine($"AVERAGE ORDER VALUE: PHP{averageOrderValue:F2}");
                }

                Console.WriteLine($"✓ Sales records saved successfully to: {fullPath}");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Access denied. Please check file permissions.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Error: Directory not found. Please check the path.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: Unable to write file - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }

        /// <summary>
        /// Case 11: Exit the application.
        /// </summary>
        private static void ExitApplication()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           THANK YOU FOR USING SIZZLESYNC POS               ║");
            Console.WriteLine("║         Streamlined Restaurant Operations System           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine("\nGoodbye!");
        }
    }

    /// <summary>
    /// Represents a completed order for sales tracking.
    /// </summary>
    public class CompletedOrder
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string TableNumber { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal Total { get; set; }
        public DateTime CompletedTime { get; set; }
    }
}