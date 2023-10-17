using System;
using Terriflux.Programs.Model.Placeables;

namespace Terriflux.Programs.GameContext
{
    public static partial class GlobalTools
    {
        public static FlowKind TranslateToFlowKind(string what)
        {
            what = what.ToUpper().Replace(" ", "");
            foreach (FlowKind kind in Enum.GetValues(typeof(FlowKind)))
            {
                if (kind.ToString().ToUpper().Replace(" ", "").CompareTo(what) == 0)
                {
                    return kind;
                }
            }

            // not found
            throw new ArgumentException($"FlowKind '{what}' does not exists!");
        }

    }
}
