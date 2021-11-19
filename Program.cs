using System.Runtime.InteropServices;
using System;
using System.Linq;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
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

            // Set up a domain context.
            try
            {
                using (var principalContext = new PrincipalContext(ContextType.Domain))
                {
                    // Search for the specified group.
                    GroupPrincipal group = null;
                    
                    try
                    {
                        group = GroupPrincipal.FindByIdentity(principalContext, args[0]);
                    }
                    catch (MultipleMatchesException)
                    {
                        Console.WriteLine($"Error: Multiple matches found for '{args[0]}'.");
                        return;
                    } 

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
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
