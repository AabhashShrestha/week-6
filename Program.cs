using System;
using System.Collections.Generic;
using System.Linq;

namespace SydneyHotel
{
    public class Customer
    {
        public string Name { get; set; }
        public int Nights { get; set; }
        public string RoomService { get; set; }
        public double Cost { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\tWelcome to Sydney Hotel Reservation System");
            List<Customer> customers = new List<Customer>();

            while (true)
            {
                Customer customer = new Customer();

                // Take customer inputs
                customer.Name = GetCustomerName();
                customer.Nights = GetValidNights();
                customer.RoomService = GetRoomServicePreference();

                // Calculate cost
                customer.Cost = CalculateCost(customer.Nights, customer.RoomService);

                customers.Add(customer);

                // Display total price
                Console.WriteLine($"Total price for {customer.Name} is ${customer.Cost:F2}");
                Console.WriteLine("________________________________________");

                // Exit or continue
                Console.WriteLine("Press 'q' to exit or any other key to add another reservation:");
                string choice = Console.ReadLine();
                if (choice.Equals("q", StringComparison.OrdinalIgnoreCase))
                    break;

                Console.WriteLine("________________________________________");
            }

            // Display summary of the customer
            DisplaySummary(customers);

            // Pause to allow the user to view the summary
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static string GetCustomerName()
        {
            Console.WriteLine("Please Enter Customer Name:");
            return Console.ReadLine();
        }

        static int GetValidNights()
        {
            while (true)
            {
                Console.WriteLine("Enter number of nights from 1 - 20 :");
                if (int.TryParse(Console.ReadLine(), out int nights) && nights >= 1 && nights <= 20)
                    return nights;

                Console.WriteLine("Invalid input. Please enter a number between 1 and 20.");
            }
        }

        static string GetRoomServicePreference()
        {
            while (true)
            {
                Console.WriteLine("Enter 'yes' or 'no' to indicate whether you want room service:");
                string input = Console.ReadLine().ToLower();
                if (input == "yes" || input == "no")
                    return input;

                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
            }
        }

        static double CalculateCost(int nights, string roomService)
        {
            double cost = nights <= 3 ? nights * 100 :
                          nights <= 10 ? nights * 80.5 :
                          nights * 75.3;

            if (roomService == "yes")
                cost += cost * 0.10; // Add 10% for room service

            return cost;
        }

        static void DisplaySummary(List<Customer> customers)
        {
            Console.WriteLine("\t\t\tSummary of Reservations");
            Console.WriteLine("Name\t\tNights\t\tRoom Service\t\tCharge");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Name}\t\t{customer.Nights}\t\t{customer.RoomService}\t\t${customer.Cost:F2}");
            }

            var highestSpender = customers.OrderByDescending(c => c.Cost).First();
            var lowestSpender = customers.OrderBy(c => c.Cost).First();

            Console.WriteLine("________________________________________");
            Console.WriteLine($"Customer spending the most: {highestSpender.Name} - ${highestSpender.Cost:F2}");
            Console.WriteLine($"Customer spending the least: {lowestSpender.Name} - ${lowestSpender.Cost:F2}");
        }
    }
}
