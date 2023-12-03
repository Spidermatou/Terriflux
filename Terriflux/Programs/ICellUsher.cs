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

        /// <summary>
        /// Recovers malus based on inactive buildings.
        /// </summary>
        /// <returns> A double[3] of sociability, economy, ecology maluses.</returns>
        double[] GetMaluses();

        void NextTurn();

        /// <summary>
        /// Confirm the placement.
        /// </summary>
        void Place();       
    }
}