using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.Model.Round;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Controller
{
    public class RoundController
    {
        private static RoundView view;
        private static RoundModel model;


        public static void SetView(RoundView newView)
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
    }
}
