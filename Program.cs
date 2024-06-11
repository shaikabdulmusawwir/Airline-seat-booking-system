using System;
using System.IO;
using System.Threading;

public class AirlineBooking
{
    public class Passenger
    {
        public string Passport;
        public string Name;
        public string Destination;
        public int SeatNumber;
        public string Email;
        public Passenger Following;
    }

    static Passenger begin, stream;

    static void Main()
    {
        int choice;
        begin = stream = new Passenger();

        int num = 1;
        do
        {
            Console.WriteLine("\n\n\t\t ***************************************************************");
            Console.WriteLine("\n\t\t                   WELCOME TO  AIRLINES SYSTEM                   ");
            Console.WriteLine("\n\t\t   **************************************************************");
            Console.WriteLine("\n\n\n\t\t Please enter your choice from below (1-4):");
            Console.WriteLine("\n\n\t\t 1. Reservation");
            Console.WriteLine("\n\n\t\t 2. Cancel");
            Console.WriteLine("\n\n\t\t 3. DISPLAY RECORDS");
            Console.WriteLine("\n\n\t\t 4. EXIT");
            Console.WriteLine("\n\n\t\t feel free to contact us");
            Console.Write("\n\n\t\t Enter your choice: ");

            choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (choice)
            {
                case 1:
                    Reserve(num);
                    num++;
                    break;
                case 2:
                    Cancel();
                    break;
                case 3:
                    Display();
                    break;
                case 4:
                    SaveFile();
                    break;
                default:
                    Console.WriteLine("\n\n\t SORRY INVALID CHOICE!");
                    Console.WriteLine("\n\n\t PLEASE CHOOSE FROM 1-4");
                    Console.WriteLine("\n\n\t Do not forget to choose from 1-4");
                    break;
            }
            Console.ReadKey();
        } while (choice != 4);
    }

    static void Details()
    {
        Console.Write("\n\t Enter your passport number:");
        stream.Passport = Console.ReadLine();
        Console.Write("\n\t Enter your name:");
        stream.Name = Console.ReadLine();
        Console.Write("\n\t Enter your email address:");
        stream.Email = Console.ReadLine();
        Console.Write("\n\t Enter the Destination : ");
        stream.Destination = Console.ReadLine();
    }

    static void Reserve(int x)
    {
        stream = begin;
        if (begin.Passport == null)
        {
            begin = stream = new Passenger();
            Details();
            stream.Following = null;
            Console.WriteLine("\n\t Seat booking successful!");
            Console.WriteLine($"\n\t your seat number is: Seat A-{x}");
            stream.SeatNumber = x;
            return;
        }
        else if (x > 15) // Assuming maximum 15 seats
        {
            Console.WriteLine("\n\t\t Seat Full.");
            return;
        }
        else
        {
            while (stream.Following != null)
                stream = stream.Following;
            stream.Following = new Passenger();
            stream = stream.Following;
            Details();
            stream.Following = null;
            Console.WriteLine("\n\t Seat booking successful!");
            Console.WriteLine($"\n\t your seat number is: Seat A-{x}");
            stream.SeatNumber = x;
            return;
        }
    }

    static void SaveFile()
    {
        using (StreamWriter writer = new StreamWriter("Air_records.txt"))
        {
            Passenger currentPassenger = begin;
            while (currentPassenger != null)
            {
                writer.Write($"{currentPassenger.Passport,-6}");
                writer.Write($"{currentPassenger.Name,-15}");
                writer.Write($"{currentPassenger.Email,-15}");
                writer.Write($"{currentPassenger.Destination,-15}");
                writer.WriteLine();
                currentPassenger = currentPassenger.Following;
            }
        }
        Console.WriteLine("\n\n\t Details have been saved to a file (Air_records.txt)");
    }

    static void Display()
    {
        if (begin == null)
        {
            Console.WriteLine("\n\n\t No bookings found.");
            return;
        }

        stream = begin;
        while (stream != null)
        {
            Console.WriteLine($"\n\n Passport Number : {stream.Passport,-6}");
            Console.WriteLine($" name : {stream.Name,-15}");
            Console.WriteLine($" email address: {stream.Email,-15}");
            Console.WriteLine($" Seat number: A-{stream.SeatNumber}");
            Console.WriteLine($" Destination:{stream.Destination,-15}");
            Console.WriteLine("\n\n++=====================================================++");
            stream = stream.Following;
        }
    }


    static void Cancel()
    {
        stream = begin;
        Console.Clear();
        Console.Write("\n\n Enter passport number to delete record?:");
        string passport = Console.ReadLine();
        if (begin.Passport == passport)
        {
            Passenger temp = begin;
            begin = begin.Following;
            temp.Following = null;
            Console.WriteLine(" Booking has been deleted");
            Thread.Sleep(800);
            return;
        }

        while (stream.Following != null)
        {
            if (stream.Following.Passport == passport)
            {
                Passenger temp = stream.Following;
                stream.Following = stream.Following.Following;
                temp.Following = null;
                Console.WriteLine(" Booking has been deleted ");
                Console.ReadKey();
                Thread.Sleep(800);
                return;
            }
            stream = stream.Following;
        }
        Console.WriteLine(" Passport number is wrong please check your passport");
    }
}
