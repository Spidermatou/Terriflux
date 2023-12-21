using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terriflux.Programs
{
    public interface ICellUsher
    {
        void SetWantedCell(ICell cell);
        void SetWantedCoordinates(Vector2I coordinates);

        int GetMaxPlacementPerTurn();

        int GetPlacedThisTurn();

        void NextTurn();

        /// <summary>
        /// Confirm the placement.
        /// </summary>
        void Place();       
    }
}