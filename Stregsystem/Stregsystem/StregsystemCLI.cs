using System;
using System.Linq;
using System.Collections.Generic;

namespace Stregsystem
{
    class StregsystemCLI
    {

        Stregsystem System;
        private bool Running = false;

        private Dictionary<string[], Action<string[]>> Commands = new();

        public StregsystemCLI(Stregsystem system)
        {
            System = system;
            System.LowBalanceEvent += (object _system, LowBalanceEventArgs e) => { System.SystemLogger.Warn($"Low balance on {e.User} of ${e.Balance}"); };

            Commands.Add(new[] { ":q", ":quit" }, (string[] args) => { Running = false; });
            Commands.Add(new[] { ":active", ":deactive" }, (string[] args) => {
                Product product = System.GetProductById(int.Parse(args[1]));
                if (product == null) 
                {
                    DisplayProductNotFound(args[1]);
                    return;
                };
                product.Active = args[0] == ":active";
            });
            Commands.Add(new[] { ":addcredits", ":rmcredits"}, (string[] args) => {
                User user = System.GetUserByUsername(args[1]);
                if (user == null)
                {
                    DisplayUserNotFound(args[1]); 
                    return;
                };
                user.AddBalance(decimal.Parse(args[2]) * (args[0] == ":addcredits" ? 1 : -1));
            });
            Commands.Add(new[] { ":crediton", ":creditoff" }, (string[] args) => 
            {
                Product product = System.GetProductById(int.Parse(args[1]));
                if (product == null)
                {
                    DisplayProductNotFound(args[1]);
                    return;
                }
                product.CanBeBoughtOnCredit = args[0] == ":crediton";
            });
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

            Logger.DisposeAll();
        }

        void HandleInput(string input)
        {
            string[] args = input.Split(" ");
            foreach (KeyValuePair<string[], Action<string[]>> command in Commands)
            {
                if (args[0] == string.Empty)
                {
                    return;
                }
                if (command.Key.Contains(args[0]))
                {
                    command.Value(args);
                    return;
                }
            }

            HandlePurchase(args);

            GetInput("\nPress enter to continue... ");
        }

        private void HandlePurchase(string[] args)
        {
            User user = System.GetUserByUsername(args[0]);
            if (user == null)
            {
                DisplayUserNotFound(args[0]);
                return;

            }

            if (args.Length == 1)
            {
                DisplayUser(user);
                return;
            }


            List<Product> ProductsToBuy = new();
            for (int i = 1; i < args.Length; i++)
            {
                Product product = System.GetProductById(int.Parse(args[i]));
                if (product == null)
                {
                    DisplayProductNotFound(args[i]);
                    continue;
                }
                ProductsToBuy.Add(product);
                
            }

            /* Sort products by if they can be bought on credit,
             * such that products that can be bought on credit are last */
            ProductsToBuy.OrderByDescending(p => p.CanBeBoughtOnCredit ? 1 : 0);
            
            decimal TotalCost = 0;

            /* Check if there's sufficient amount of credits on account to buy
             * all products that can't be bought on credit first, and the remaining products after*/
            foreach(Product product in ProductsToBuy)
            {
                TotalCost += product.Price;
                if (TotalCost > user.Balance && !product.CanBeBoughtOnCredit)
                {
                    DisplayInsufficientCredits(new InsufficientCreditsException(user, product));
                    return;
                }
            }


            foreach (Product product in ProductsToBuy)
            {
                Transaction transaction = new BuyTransaction(product, user);
                try
                {
                    System.ExecuteTransaction(transaction);
                    Console.WriteLine($"Purchased {product.Name}");
                }
                catch (OutOfSeasonException e)
                {
                    DisplayOutOfSeason(e);
                }
            }
        }

        private void DisplayUser(User user)
        {
            Console.WriteLine(user.ToString());                
            Console.WriteLine($"Current balance: {user.Balance}");

        }

        private void DisplayOutOfSeason(OutOfSeasonException e)
        {
            Console.WriteLine($"Product {e.PurchasedProduct.Name} out of season!");
        }

        private void DisplayInsufficientCredits(InsufficientCreditsException e)
        {
            Console.WriteLine($"Insufficient credits to purchase {e.PurchasedProduct.Name}. Balance is ${e.Actor.Balance} credits");
        }

        private void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"User <{username}> could not be found!");
        }

        private void DisplayProductNotFound(string id)
        {
            Console.WriteLine($"Product with Id <{id}> could not be found!");
        }

        private void Seperator(char ch)
        {
            Console.WriteLine("".PadLeft(Console.WindowWidth, ch));
        }

        private void Center(string msg)
        {
            Console.WriteLine(msg.PadLeft((Console.WindowWidth + msg.Length) / 2, ' '));
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
            Console.Write(query);
            string rawInput = Console.ReadLine();

            return rawInput.Trim().ToLower();
        }

        

    }
}
