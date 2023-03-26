class Program
{
    static void Main(string[] args)
    {
        ParkSystem parkSystem = new ParkSystem(0);

        while (true)
        {
            Console.Write("Enter command: ");
            string? command = Console.ReadLine();

            if (command?.Length > 0 && command.StartsWith("$"))
            {
                string[] commandArr = command.Split("$ ", StringSplitOptions.RemoveEmptyEntries);
                string actionWithParam = commandArr[0];

                parkSystem.ProcessCommand(actionWithParam);
            }
            else
            {
                Console.WriteLine("Invalid command");
            }
        }
    }
}