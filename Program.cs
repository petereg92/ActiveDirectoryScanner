using System;
using System.Linq;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            // Validate input.
            if (args == null || args.Count() != 1)
            {
                Console.WriteLine("Error: Expected a single argument.");
                return;
            }

            // Set up a domain context.
            using (var principalContext = new PrincipalContext(ContextType.Domain))
            {
                // Search for the specified group.
                var group = GroupPrincipal.FindByIdentity(principalContext, args[0]);

                // If it wasn't found, display an error message.
                if (group == null)
                {
                    Console.WriteLine($"Error: Group '{args[0]}' not found.");
                    return;
                }

                // Else, print the group's members.
                foreach (var member in group.Members)
                {
                    Console.WriteLine(member.DisplayName);
                }
            }
        }
    }
}
