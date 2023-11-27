using Godot;
using System;
using System.Collections.Generic;
using System.Text;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.GameContext.OurPath;
using Terriflux.Programs.Gauges;
using Terriflux.Programs.Observers;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Model.Round
{
    public partial class RoundModel : IVerbosable
    {
        private int roundNumber = 1;
        private int maxPerTurn = 3;
        private int buildedThisTurn = 0;

        public Impacts impactsManager;

        private readonly List<IRoundObserver> observers = new();


        // for test
        public text_box TxtBox;
        public string endmess;

        public RoundModel() { }

        public void NextTurn()
        {
            if (impactsManager == null)
            {
                GD.Print("RoundModel not set correctly");
                throw new Exception();
            }

            roundNumber++;

            // game end?    // TODO with strategy pattern 
            if (impactsManager.GetImpacts()[0] == 0)
            {
                Ends endmessage = new();
                text_box tb = text_box.Design();
                this.endmess = endmessage.GetSocialZero();

                this.TxtBox = tb;
            }
            else if (impactsManager.GetImpacts()[1] == 0)
            {
                Ends endmessage = new();
                text_box tb = text_box.Design();

                this.endmess = endmessage.GetEconomyZero();

                this.TxtBox = tb;

            }
            else if (impactsManager.GetImpacts()[2] == 0)
            {
                Ends endmessage = new();
                text_box tb = text_box.Design();

                this.endmess = endmessage.GetEcologyZero();

                this.TxtBox = tb;

            }
            else
            {
                // reset
                buildedThisTurn = 0;

                // update
                Notify();
            }
        }

        /// <returns> The actual round number. </returns>
        public int GetRoundNumber() { return roundNumber; }

        /// <returns> Maximum number of buildings a player can build in one turn. </returns>
        public int GetMaxPerTurn() { return maxPerTurn; }

        /// <returns> Number of buildings already created this round. </returns>
        public int GetThisTurn() { return buildedThisTurn; }

        /// <returns> Adds one to the counter of buildings created this round. </returns>
        public void PlusOneBuilded()
        {
            // can build?
            if (buildedThisTurn + 1 <= maxPerTurn)
            {
                buildedThisTurn++;
            }
            else
            {
                throw new Exception("Try to build more buildings than allowed!");
            }
        }

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

        public string Verbose()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Round number: {roundNumber}");
            sb.AppendLine($"Max: {GetMaxPerTurn()}");
            sb.AppendLine($"Actual: {GetThisTurn()}");
            return sb.ToString();
        }
    }
}