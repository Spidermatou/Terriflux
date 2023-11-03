using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Model.Round;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Controller
{
    public class RoundController 
    {
        private static RoundCounter view;
        private static RoundModel model;

        public static void SetView(RoundCounter newView)
        {
            view = newView;

            // add observer
            if (model != null)
            {
                model.AddObserver(view);
                view.Update(model.GetRoundNumber());
            }
        }

        public static void SetModel(RoundModel newModel)
        {
            model = newModel;

            // add observer
            if (view != null) 
            {
                model.AddObserver(view);
                view.Update(model.GetRoundNumber());
            }
        }

        public static void NextTurn()
        {
            if (view != null && model != null) model.NextTurn();
        }

        public static string Verbose()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Model={model}");
            sb.AppendLine($"View={view}");

            return sb.ToString();
        }
    }
}
