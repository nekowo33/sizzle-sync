using System;
using System.Collections.Generic;
using System.Linq;

namespace SizzleSyncPOS
{
    /// <summary>
    /// OrderSystem manages customer orders using a Stack data structure.
    /// Stack is used here because it provides LIFO (Last-In-First-Out) behavior,
    /// which allows staff to easily correct mistakes by removing the most recently added item.
    /// This addresses the problem of "modifying, correcting, or removing recent entries when necessary."
    /// </summary>
    public class OrderSystem
    {
        // Stack data structure to store order items - LIFO principle allows easy correction
        private Stack<OrderItem> orderItems;
        private int orderNumber;

        public OrderSystem(int orderNum)
        {
            orderItems = new Stack<OrderItem>();
            orderNumber = orderNum;
        }

        /// <summary>
        /// Adds a menu item to the order by pushing it onto the stack.
        /// The most recently added item will be on top of the stack.
        /// </summary>
        public void AddItem(string itemName, decimal price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(itemName))
            {
                Console.WriteLine("Error: Item name cannot be empty.");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Error: Quantity must be greater than zero.");
                return;
            }

            OrderItem item = new OrderItem(itemName, price, quantity);
            orderItems.Push(item);
            Console.WriteLine($"✓ '{itemName}' x{quantity} added to order.");
        }

        /// <summary>
        /// Implements the correction feature by removing the last added item from the stack.
        /// This demonstrates the LIFO principle - addressing the need to "correct or remove recent entries."
        /// </summary>
        public OrderItem RemoveLastItem()
        {
            if (orderItems.Count == 0)
            {
                Console.WriteLine("Order is empty. Nothing to remove.");
                return null;
            }

            OrderItem removedItem = orderItems.Pop();
            Console.WriteLine($"✓ '{removedItem.ItemName}' x{removedItem.Quantity} removed from order.");
            return removedItem;
        }

        /// <summary>
        /// Displays all items currently in the order.
        /// </summary>
        public void ViewOrder()
        {
            if (orderItems.Count == 0)
            {
                Console.WriteLine("Order is empty.");
                return;
            }

            Console.WriteLine($"\n===== Order #{orderNumber} =====");
            Console.WriteLine($"Total Items: {orderItems.Count}");
            Console.WriteLine("--------------------------------");

            int position = 1;
            foreach (OrderItem item in orderItems)
            {
                Console.WriteLine($"{position}. {item.ItemName} x{item.Quantity} - P{item.Subtotal:F2}");
                position++;
            }
            Console.WriteLine("================================\n");
        }

        /// <summary>
        /// Calculates the total amount for this order.
        /// </summary>
        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (OrderItem item in orderItems)
            {
                total += item.Subtotal;
            }
            return total;
        }

        /// <summary>
        /// Gets the count of items in the order.
        /// </summary>
        public int GetItemCount()
        {
            return orderItems.Count;
        }

        /// <summary>
        /// Gets the order number.
        /// </summary>
        public int GetOrderNumber()
        {
            return orderNumber;
        }

        /// <summary>
        /// Clears all items from the order.
        /// </summary>
        public void ClearOrder()
        {
            orderItems.Clear();
            Console.WriteLine("Order has been cleared.");
        }

        /// <summary>
        /// Gets a copy of all items in the order as a list.
        /// </summary>
        public List<OrderItem> GetAllItems()
        {
            return orderItems.ToList();
        }

        /// <summary>
        /// Peeks at the top item without removing it.
        /// </summary>
        public OrderItem PeekTopItem()
        {
            if (orderItems.Count == 0)
            {
                return null;
            }
            return orderItems.Peek();
        }
    }

    /// <summary>
    /// Represents a single item in an order with quantity and price.
    /// </summary>
    public class OrderItem
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => Price * Quantity;

        public OrderItem(string itemName, decimal price, int quantity)
        {
            ItemName = itemName;
            Price = price;
            Quantity = quantity;
        }
    }
}