using System;
using System.Linq;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryScanner
{
    /// <summary>
    /// A class that can print the members of an Active Directory group.
    /// </summary>
    public class AdGroupMemberPrinter : IAdGroupMemberPrinter
    {
        /// <summary>
        /// Prints the members of a specified Active Directory group.
        /// </summary>
        /// <param name="groupName">The name of the Active Directory whose members should be printed.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="groupName"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="groupName"/> is empty or whitespace.</exception>
        public void PrintGroupMembers(string groupName)
        {
            // Validate input.
            if (groupName == null)
            {
                throw new ArgumentNullException(nameof(groupName));
            }

            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentException($"'{nameof(groupName)}' cannot be empty or whitespace.", nameof(groupName));
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
                        group = GroupPrincipal.FindByIdentity(principalContext, groupName);
                    }
                    catch (MultipleMatchesException)
                    {
                        Console.WriteLine($"Error: Multiple matches found for '{groupName}'.");
                        return;
                    } 

                    // If it wasn't found, display an error message.
                    if (group == null)
                    {
                        Console.WriteLine($"Error: Group '{groupName}' not found.");
                        return;
                    }

                    // Else, print the group's members.
                    var members = group.GetMembers();

                    if (members.Count() == 0)
                    {
                        Console.WriteLine($"Group '{groupName}' contains no members.");
                    }
                    else
                    {
                        Console.WriteLine($"Members of group '{groupName}':");
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
