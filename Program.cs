using System.Runtime.InteropServices;
using System;
using System.Linq;

namespace ActiveDirectoryScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            // If this app isn't running on Windows, display an error message.
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("Error: This app is only compatible with Windows.");
                return;
            }

#if DEBUG
            // If we're debugging, set up some test args.
            args = new string[] { "Test" };
#endif

            // Validate input.
            if (args == null || args.Count() != 1)
            {
                Console.WriteLine("Error: Expected a single argument.");
                return;
            }

            // Print the members of the specified group.
            var adGroupMemberPrinter = new AdGroupMemberPrinter();

            adGroupMemberPrinter.PrintGroupMembers(args[0]);
        }
    }
}
