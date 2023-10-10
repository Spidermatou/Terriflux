using System;
using Terriflux.Programs.Model;

namespace Terriflux.Programs.Exceptions
{
    public class UnplaceableException : Exception
    {
        /// <summary>
        /// Exception for impossible placement issue, with message "Unable to place item on grid!".
        /// </summary>
        public UnplaceableException() : base("Unable to place item on grid!") { }

        /// <summary>
        /// Exception for impossible placement issue, with message "Unable to place item on grid, necesary is '{necessary}' but actual is '{actual}'!".
        /// </summary>
        public UnplaceableException(CellKind actual, CellKind necessary) : base($"Unable to place item on grid, necesary is '{necessary}' but actual is '{actual}'!") { }
    }
}
