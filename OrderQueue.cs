using System;
using System.Collections.Generic;

namespace SizzleSyncPOS
{
    /// <summary>
    /// OrderQueue manages pending orders using a Queue data structure.
    /// Queue is used here because it provides FIFO (First-In-First-Out) behavior,
    /// which ensures orders are processed in the sequence they were received.
    /// This addresses the need for "organized and structured order handling" mentioned in the proposal.
    /// </summary>
    public class OrderQueue
    {
        // Queue data structure to store pending orders - FIFO ensures fair processing order
        private Queue<PendingOrder> pendingOrders;
        private int nextOrderNumber;

        public OrderQueue()
        {
            pendingOrders = new Queue<PendingOrder>();
            nextOrderNumber = 1001; // Starting order number
        }

        /// <summary>
        /// Adds a new order to the queue for processing.
        /// Uses Enqueue to add order to the back of the queue.
        /// </summary>
        public int AddOrder(string customerName, string tableNumber)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                Console.WriteLine("Error: Customer name cannot be empty.");
                return -1;
            }

            PendingOrder order = new PendingOrder(nextOrderNumber, customerName, tableNumber);
            pendingOrders.Enqueue(order);

            Console.WriteLine($"✓ Order #{nextOrderNumber} added to queue.");
            Console.WriteLine($"   Customer: {customerName}");
            Console.WriteLine($"   Table: {tableNumber}");
            Console.WriteLine($"   Position in queue: {pendingOrders.Count}");

            nextOrderNumber++;
            return order.OrderNumber;
        }

        /// <summary>
        /// Processes the next order in the queue (moves to "In Progress").
        /// Uses Dequeue to remove and return the first order in line (FIFO principle).
        /// </summary>
        public PendingOrder ProcessNextOrder()
        {
            if (pendingOrders.Count == 0)
            {
                Console.WriteLine("No pending orders to process.");
                return null;
            }

            PendingOrder order = pendingOrders.Dequeue();
            order.Status = "In Progress";

            Console.WriteLine($"✓ Now processing Order #{order.OrderNumber}");
            Console.WriteLine($"   Customer: {order.CustomerName}");
            Console.WriteLine($"   Table: {order.TableNumber}");
            Console.WriteLine($"   Orders remaining in queue: {pendingOrders.Count}");

            return order;
        }

        /// <summary>
        /// Displays all pending orders in the queue.
        /// Shows them in the order they will be processed.
        /// </summary>
        public void ViewPendingOrders()
        {
            if (pendingOrders.Count == 0)
            {
                Console.WriteLine("No pending orders in queue.");
                return;
            }

            Console.WriteLine("\n===== PENDING ORDERS QUEUE =====");
            Console.WriteLine($"Total Orders Waiting: {pendingOrders.Count}");
            Console.WriteLine("--------------------------------");

            int position = 1;
            foreach (PendingOrder order in pendingOrders)
            {
                string nextLabel = position == 1 ? " (Next to Process)" : "";
                Console.WriteLine($"{position}. Order #{order.OrderNumber} - {order.CustomerName} (Table {order.TableNumber}){nextLabel}");
                Console.WriteLine($"   Placed at: {order.OrderTime:HH:mm:ss}");
                position++;
            }
            Console.WriteLine("================================\n");
        }

        /// <summary>
        /// Checks if the order queue is empty.
        /// </summary>
        public bool IsEmpty()
        {
            return pendingOrders.Count == 0;
        }

        /// <summary>
        /// Peeks at the next order to be processed without removing it.
        /// </summary>
        public PendingOrder PeekNextOrder()
        {
            if (pendingOrders.Count == 0)
            {
                Console.WriteLine("No orders in queue.");
                return null;
            }

            PendingOrder nextOrder = pendingOrders.Peek();
            Console.WriteLine($"Next order to process: #{nextOrder.OrderNumber} - {nextOrder.CustomerName} (Table {nextOrder.TableNumber})");
            return nextOrder;
        }

        /// <summary>
        /// Gets the current number of pending orders.
        /// </summary>
        public int GetPendingCount()
        {
            return pendingOrders.Count;
        }

        /// <summary>
        /// Displays queue status information.
        /// </summary>
        public void CheckStatus()
        {
            Console.WriteLine("\n===== ORDER QUEUE STATUS =====");

            if (pendingOrders.Count == 0)
            {
                Console.WriteLine("Status: EMPTY");
                Console.WriteLine("No orders waiting.");
            }
            else
            {
                Console.WriteLine($"Status: ACTIVE");
                Console.WriteLine($"Pending Orders: {pendingOrders.Count}");
                PendingOrder next = pendingOrders.Peek();
                Console.WriteLine($"Next to Process: Order #{next.OrderNumber} - {next.CustomerName}");
            }

            Console.WriteLine("==============================\n");
        }

        /// <summary>
        /// Clears all pending orders from the queue.
        /// </summary>
        public void ClearQueue()
        {
            int count = pendingOrders.Count;
            pendingOrders.Clear();
            Console.WriteLine($"✓ Order queue cleared. {count} order(s) removed.");
        }
    }

    /// <summary>
    /// Represents a pending order in the queue.
    /// </summary>
    public class PendingOrder
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string TableNumber { get; set; }
        public DateTime OrderTime { get; set; }
        public string Status { get; set; }

        public PendingOrder(int orderNumber, string customerName, string tableNumber)
        {
            OrderNumber = orderNumber;
            CustomerName = customerName;
            TableNumber = tableNumber;
            OrderTime = DateTime.Now;
            Status = "Pending";
        }
    }
}