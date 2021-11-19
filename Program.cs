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
                    var members = group.GetMembers();

                    if (members.Count() == 0)
                    {
                        Console.WriteLine($"Group '{args[0]}' contains no members.");
                    }
                    else
                    {
                        Console.WriteLine($"Members of group '{args[0]}':")
                        foreach (var member in members)
                        {
                            Console.WriteLine(member.DisplayName);
                        }
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
