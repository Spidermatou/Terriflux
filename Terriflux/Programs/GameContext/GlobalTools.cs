using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.Model;

namespace Terriflux.Programs.GameContext
{
    public static partial class GlobalTools
    {
        public static FlowKind TranslateToFlowKind(string what) 
        {
            what = what.ToUpper().Replace(" ", "");
            foreach (FlowKind kind in Enum.GetValues(typeof(FlowKind)))
            {
                if (kind.ToString().ToUpper().Replace(" ", "").CompareTo(what)==0)
                {
                    return kind;
                }
            }

            // not found
            throw new ArgumentException($"FlowKind '{what}' does not exists!");
        }

    }
}
