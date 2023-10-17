using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Cell;

namespace Terriflux.Programs.Model.Placeables
{
    /// <summary>
    /// Represents a building.
    /// </summary>
    public partial class BuildingModel : CellModel, IPlaceable, IVerbosable
    {
        private static readonly Dictionary<InfluenceScale, int> MULTIPLICATION_RATE = new()
        {
            { InfluenceScale.REGIONAL, 1 },
            { InfluenceScale.NATIONAL, 3 },
            { InfluenceScale.WORLDWIDE, 5 },

        }; // the larger the scale, the larger the needs and production will be 

        private readonly double[] impacts;
        private readonly InfluenceScale actualInfluenceScale;

        private readonly Dictionary<FlowKind, int> needs;
        private readonly Dictionary<FlowKind, int> production;

        // CREATION
        public BuildingModel(string name, double[] impacts, InfluenceScale influence,
            Dictionary<FlowKind, int> needs, Dictionary<FlowKind, int> production) : base(name.ToPascalCase(), CellKind.BUILDING)
        {
            // securities
            if (impacts.Length != 3)
            {
                throw new ArgumentException(nameof(impacts) + "'s length must be 3.");
            }

            // basic attributes
            this.impacts = impacts;
            actualInfluenceScale = influence;

            // needs, products
            this.needs = needs.ToDictionary(entry => entry.Key, entry => entry.Value);  // clone to avoid external modifications
            this.production = production.ToDictionary(entry => entry.Key, entry => entry.Value);

            // apply influence multiplicators
            foreach (KeyValuePair<FlowKind, int> kvp in this.production)    // for products
            {
                this.production[kvp.Key] = kvp.Value * MULTIPLICATION_RATE[GetInfluence()];
            }

            foreach (KeyValuePair<FlowKind, int> kvp in this.needs)     // for needs
            {
                this.needs[kvp.Key] = kvp.Value * MULTIPLICATION_RATE[GetInfluence()];
            }
        }

        // Basic informations
        public InfluenceScale GetInfluence()
        {
            return actualInfluenceScale;
        }

        // Impacts
        public double[] GetImpacts()
        {
            return impacts;
        }

        // Composition
        public CellModel GetComposition()
        {
            return this;
        }


        // Needs
        public int GetQuantityNeeded(FlowKind kind)
        {
            if (needs.ContainsKey(kind))
            {
                return needs[kind];
            }
            else
            {
                return 0;
            }
        }

        public FlowKind[] GetNeedsKind()
        {
            return needs.Keys.ToArray();
        }

        // Production
        /// <summary>
        /// Modify building productions, then apply influence scale's modificators
        /// </summary>
        /// <param name="products">Default building productions, at lowest scale of influence</param>
        public FlowKind[] GetProductionKind()
        {
            return production.Keys.ToArray();
        }

        public int GetQuantityProduced(FlowKind kind)
        {
            if (production.ContainsKey(kind))
            {
                return production[kind];
            }
            else
            {
                return 0;
            }
        }

        // Verbose      
        public string Verbose()
        {
            StringBuilder sb = new();

            // basis
            sb.Append($"Name: {GetName()}\n");
            sb.Append($"Influence: {GetInfluence()}\n");

            // impacts
            sb.Append($"Impacts:\n");
            sb.Append($"    Sociability:{impacts[0]}\n");
            sb.Append($"    Economy:{impacts[1]}\n");
            sb.Append($"    Ecology:{impacts[2]}\n");

            // needs
            sb.Append($"Needs:\n");
            foreach (KeyValuePair<FlowKind, int> kvp in needs)
            {
                sb.Append($"    Key={kvp.Key}, Value={kvp.Value}\n");
            }

            // production
            sb.Append($"Products:\n");
            foreach (KeyValuePair<FlowKind, int> kvp in production)
            {
                sb.Append($"    Key={kvp.Key}, Value={kvp.Value}\n");
            }

            return sb.ToString();
        }
    }
}