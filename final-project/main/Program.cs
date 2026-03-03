namespace main;

class Program
{
    static void Main(string[] args)
    {
    
        string choice;
        do {
            Console.WriteLine("What would you like to do?");
            choice = Console.ReadLine();
            Console.WriteLine(choice);
        } while (choice != "Exit");
    }
}
