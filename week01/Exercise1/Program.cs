using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your first name?");
        string firstName = Console.ReadLine();
        Console.WriteLine($"Hello {firstName}!");
        Console.WriteLine("What is your last name?");
        string lastName = Console.ReadLine();
        Console.WriteLine($"Your Name is {firstName}, {lastName}!");
    }
}