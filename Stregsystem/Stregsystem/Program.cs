using System;
using System.Collections.Generic;

namespace Stregsystem
{
    partial class Program
    {
        static void Main(string[] args)
        {

            Stregsystem system = new Stregsystem();

            StregsystemCLI cli = new StregsystemCLI(system);

            cli.Start();
        }

    }
}
