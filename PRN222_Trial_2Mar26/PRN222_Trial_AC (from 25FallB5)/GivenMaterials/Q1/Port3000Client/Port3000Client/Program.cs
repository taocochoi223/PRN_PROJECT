using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Port3000Client
{
    public class BorrowRecord
    {
        public string BookID { get; set; } = "";
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "";
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter Reader ID (or press Enter to exit): ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Goodbye! Library client is shutting down.");
                    break;
                }

                if (!int.TryParse(input, out int readerId) || readerId <= 0)
                {
                    Console.WriteLine("Invalid input! Please enter a valid Reader ID (positive integer).");
                    continue;
                }

                ConnectToServer(readerId);
            }
        }
        static void ConnectToServer(int readerId)
        {
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 3000))
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(readerId.ToString());
                    stream.Write(data, 0, data.Length);

                    byte[] buffer = new byte[1024 * 10];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string jsonResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    DisplayResult(readerId, jsonResponse);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Library server is not running. Please try again later.");
            }
        }

        static void DisplayResult(int readerId, string jsonResponse)
        {


            var records = JsonSerializer.Deserialize<List<BorrowRecord>>(jsonResponse);

            if (records == null || records.Count == 0)
            {

                Console.WriteLine($"No borrow records found for Reader ID {readerId}.");
            }
            else
            {
                Console.WriteLine($"=== Borrow History for Reader ID: {readerId}");
                foreach (var record in records)
                {
                    Console.WriteLine($"Book ID: {record.BookID}");
                    Console.WriteLine($"Title: {record.Title}");
                    Console.WriteLine($"Author: {record.Author}");
                    Console.WriteLine($"Borrow Date: {record.BorrowDate:yyyy-MM-dd}");
                    string returnDt = record.ReturnDate.HasValue ? record.ReturnDate.Value.ToString("yyyy-MM-dd") : "Not returned yet";
                    Console.WriteLine($"Return Date: {returnDt}");
                    Console.WriteLine($"Status: {record.Status}");
                    Console.WriteLine("---");
                }
            }
        }
    }
}
