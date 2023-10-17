using Godot;

namespace Terriflux.Programs.Model
{
    public partial class BuildingPart : CellModel // Reworked
    {
        public BuildingPart(string buildingName) : base(buildingName.ToCamelCase(), CellKind.BUILDING) { }
    }
}
