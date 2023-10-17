using Godot;
using System.Collections.Generic;
using System.ComponentModel;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.Model
{
    /// <summary>
    /// Represent a cell.
    /// All cell have the exact same size. If one changes size, everything else adapts. You can access to these
    /// dimensions with Get/SetGlobalSize
    /// </summary>
    [ImmutableObject(true)]
    public partial class CellModel // Reworked
    {
        private static readonly double globalSize = 128;   //px

        private readonly string name = "Cell";  // default
        private readonly CellKind kind = CellKind.PRIMARY;   // default

        // CONSTRUCT
        /// <summary>
        /// Create a cell model.
        /// </summary>
        public CellModel() { }

        /// <summary>
        /// Create a cell model.
        /// </summary>
        public CellModel(string name, CellKind kind)
        {
            this.name = name;
            this.kind = kind;
        }

        // Global dimensions
        /// <returns> The theoretic size of any cell.</returns>
        public static double GetGlobalSize()
        {
            return globalSize;
        }

        /// <returns>The name of this cell.</returns>
        public string GetName()
        {
            return name;
        }

        /// <returns>The kind of this cell.</returns>
        public CellKind GetKind()
        {
            return kind;
        }
    }
}