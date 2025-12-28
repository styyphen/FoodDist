using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;

namespace AntiPatterns.SolidViolations
{
    // Single Responsibility Principle Violation:
    // This class validates, calculates, persists, emails, and renders UI.
    public class OrderManager
    {
        public object PlaceOrder(Order order)
        {
            // Validation
            if (order == null || order.Items == null || order.Items.Count == 0)
                throw new ArgumentException("Invalid order");

            // Calculate total
            decimal total = 0;
            foreach (var i in order.Items) total += i.Price;
            order.Total = total;

            // Persist (direct DB call)
            SaveOrderToDatabase(order);

            // Send confirmation (direct SMTP usage)
            using (var smtp = new SmtpClient("smtp.example.com"))
            {
                var msg = new MailMessage("from@example.com", order.CustomerEmail, "Order Received", "Thanks!");
                smtp.Send(msg);
            }

            // Render receipt (UI responsibility leaking into domain)
            return $"<html><body>Order #{order.Id} - Total: {order.Total:C}</body></html>";
        }

        private void SaveOrderToDatabase(Order order)
        {
            using (var conn = new SqlConnection("Server=.; Database=Food; Integrated Security=true"))
            {
                conn.Open();
                // naive concatenation (SQL injection risk) and mixing persistence here
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO Orders (Id, Total) VALUES ({order.Id}, {order.Total})";
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class Order { public int Id; public List<Item> Items; public decimal Total; public string CustomerEmail; }
    public class Item { public decimal Price; }
}
