using System;
using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Would you like to change the console color scheme? Or just press enter to keep the standard scheme.");
            Console.WriteLine("1. Ocean Waves");
            Console.WriteLine("2. Vampire");
            Console.WriteLine("3. Bumblebee");
            string colorSelection = Console.ReadLine();

            var backgroundColor = ConsoleColor.Black;
            var textColor = ConsoleColor.Gray;

            switch (colorSelection)
            {
                case "1":
                    backgroundColor = ConsoleColor.DarkCyan;
                    textColor = ConsoleColor.White;
                    break;
                case "2":
                    backgroundColor = ConsoleColor.DarkRed;
                    textColor = ConsoleColor.Black;
                    break;
                case "3":
                    backgroundColor = ConsoleColor.Yellow;
                    textColor = ConsoleColor.Black;
                    break;
                default:
                    break;
            }
            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {

                Console.BackgroundColor = backgroundColor;
                Console.ForegroundColor = textColor;
                Console.Clear();

                Console.WriteLine("Pleasant Greeting!");
                Console.WriteLine("------------------");
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
    }
}
