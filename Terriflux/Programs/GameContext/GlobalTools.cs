using System;
using Terriflux.Programs.Model.Placeables;

namespace Terriflux.Programs.GameContext
{
    public static partial class GlobalTools
    {
        public static FlowKind TranslateToFlowKind(string what)
        {
            Enum.TryParse<FlowKind>(what, true, out FlowKind flow);

            // not found?
            if (flow == FlowKind.NOTHING)
            {
                throw new ArgumentException($"FlowKind '{what}' does not exists!");
            }

            return flow;
        }
    }
}
