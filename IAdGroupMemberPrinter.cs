namespace ActiveDirectoryScanner
{
    /// <summary>
    /// Represents a class that can print the members of an Active Directory group.
    /// </summary>
    public interface IAdGroupMemberPrinter
    {
        /// <summary>
        /// Prints the members of a specified Active Directory group.
        /// </summary>
        /// <param name="groupName">The name of the Active Directory whose members should be printed.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="groupName"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="groupName"/> is empty or whitespace.</exception>
        public void PrintGroupMembers(string groupName);
    }
}