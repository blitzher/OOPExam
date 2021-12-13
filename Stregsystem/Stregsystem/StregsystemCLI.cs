using System;
using System.Collections.Generic;

namespace Stregsystem
{
    class StregsystemCLI
    {

        Stregsystem System;
        private bool Running = false;

        public StregsystemCLI(Stregsystem system)
        {
            System = system;
        }

        public void Start()
        {
            Running = true;
            Run();
            
        }

        private void Run()
        {
            while (Running)
            {
                PrintHeader();
                PrintProducts();

                string input = GetInput();
                HandleInput(input);

                Console.Clear();
            }
        }

        void HandleInput(string input)
        {
            if (input == "exit" || input == "q")
            {
                Running = false;
            }
        }

        private void Seperator(char ch)
        {
            Console.WriteLine("".PadLeft(Console.WindowWidth, ch));
        }

        private void Center(string msg)
        {
            Console.WriteLine(msg.PadLeft((Console.WindowWidth - msg.Length) / 2, ' '));
        }

        private void Center(List<string> msgs)
        {
            int longest = -1;
            foreach (string msg in msgs)
            {
                if (msg.Length > longest)
                {
                    longest = msg.Length;
                }
            }
            string spacer = "".PadLeft((Console.WindowWidth - longest) / 2, ' ');

            foreach(string msg in msgs)
            {
                Console.WriteLine(spacer + msg);
            }
        }

        void PrintHeader()
        {
            Seperator('=');
            Center("Stregsystem");
            Seperator('=');
        }

        void PrintProducts()
        {
            var products = System.ActiveProducts().ConvertAll((p) => p.ToString());
            Center(products);
        }

        string GetInput(string query = "Enter your command > ")
        {
            Console.WriteLine(query);
            string rawInput = Console.ReadLine();

            return rawInput.Trim().ToLower();
        }

        

    }
}
