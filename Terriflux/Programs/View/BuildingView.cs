using Godot;
using System.Collections.Generic;
using System.IO;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.View
{
    /// <summary>
    /// Creates a building visible to the player (IMMUABLE!)
    /// </summary>
    public partial class BuildingView : Node2D, IBuildingObserver
    {
        /// <summary>
        /// Simple class construction not allowed. Please use the associated Design() function.
        /// </summary>
        private BuildingView() : base() { }

        /// <param name="buildingName"></param>
        /// <returns>The created BuildingView node </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static BuildingView Design()
        {
            return (BuildingView)GD.Load<PackedScene>(Paths.VIEW_NODES + "BuildingView" + Paths.GDEXT)
                    .Instantiate();
        }

        public void UpdateName(string name)
        {
            foreach (Node child in this.GetChildren())
            {
                if (child is CellView cellChild)
                {
                    cellChild.UpdateCellName(name);
                }
            }
        }
    }
}