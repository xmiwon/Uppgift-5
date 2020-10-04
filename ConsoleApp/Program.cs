using SharedLibrary;
using System;

//OBS! Denna projekt är till testet

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {          
            //Gör en ny instance av SharedLibraryDevice
            new SharedLibraryDevice();
            Console.ReadKey();
        }
    }
}
