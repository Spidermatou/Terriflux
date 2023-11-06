using Godot;
using Terriflux.Programs.Model.Placeables;

namespace Terriflux.Programs.Model.Cell
{
    /// <summary>
    /// Represent a cell.
    /// All cell have the exact same size. If one changes size, everything else adapts. You can access to these
    /// dimensions with Get/SetGlobalSize
    /// </summary>
    public partial class CellModel : IPlaceable
    {
        private readonly string name = "Cell";  // default
        private readonly CellKind kind = CellKind.WASTELAND;   // default

        // CONSTRUCT
        /// <summary>
        /// Create a cell model.
        /// </summary>
        private CellModel() { }

        /// <summary>
        /// Create a cell model.
        /// </summary>
        public CellModel(string name, CellKind kind)
        {
            this.name = name.ToPascalCase();
            this.kind = kind;
        }

        // GET SET
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

        public CellModel GetComposition()
        {
            return this;
        }
    }
}