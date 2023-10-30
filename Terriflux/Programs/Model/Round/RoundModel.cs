using Godot;
using System;
using System.Collections.Generic;
using System.Text;
using Terriflux.Programs.Observers;

namespace Terriflux.Programs.Model.Round
{
    public partial class RoundModel
    {
        private int roundNumber = 0;
        private int buildedThisTurn = 0;
        private int maxPerTurn = 0;

        private readonly List<IRoundObserver> observers = new();

        public RoundModel() { }

        public void NextTurn()
        {
            roundNumber++;

            // reset
            buildedThisTurn = 0;

            // update
            Notify();
        }

        /// <returns> Maximum number of buildings a player can build in one turn. </returns>
        public int GetMaxPerTurn() { return maxPerTurn; }

        /// <returns> Number of buildings already created this round. </returns>
        public int GetThisTurn() { return buildedThisTurn; }

        /// <returns> Adds one to the counter of buildings created this round. </returns>
        public void PlusOneBuilded() { buildedThisTurn++; }

        /// <returns> Set the maximum number of buildings a player can build in one turn. </returns>
        public void SetMaxPerTurn(int newMax) { maxPerTurn = newMax; }

        // OBSERVERS
        public void AddObserver(IRoundObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void RemoveObserver(IRoundObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        private void Notify()
        {
            foreach (IRoundObserver observer in observers)
            {
                observer.Update(roundNumber);
            }
        }

    }
}