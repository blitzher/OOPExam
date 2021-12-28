using System;
using System.Collections.Generic;

namespace Stregsystem
{
    partial class Program
    {
        static void Main(string[] args)
        {

            /* Given that the Stregsystem exposes all methods for controlling the system
             * I decided that the CLI only needs to implement these, along with the interface
             * and decided that the StregsystemController class was unnecessary complexity */
            Stregsystem system = new Stregsystem();
            StregsystemCLI cli = new StregsystemCLI(system);

            cli.Start();
        }

    }
}
