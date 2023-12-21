using Godot;
using System;
using System.Collections.Generic;
using System.Text;
using Terriflux.Programs.GameContext.OurPath;
using Terriflux.Programs.Gauges;
using Terriflux.Programs.Model.Grid;
using Terriflux.Programs.Model.Placeables;
using Terriflux.Programs.Model.Round;
using Terriflux.Programs.View;

namespace Terriflux.Programs.Controller
{
	public class RoundController
	{
		private static RoundCounter view;
		private static RoundModel model;
		public static InventoryController inventoryController;
		public static Impacts impactsManager;
		public static GridModel grid;

		public static void SetInventoryController(InventoryController inventoryManager) 
		{ 
			inventoryController = inventoryManager; 
		}

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
			if (view != null && model != null && inventoryController != null && grid != null && impactsManager != null)
			{
				model.NextTurn();

				List<BuildingModel> buildlist = new();
				
				foreach (IPlaceable placeable in grid.GetAllPlacements().Values)
				{
					if (placeable is BuildingModel building)
					{
						buildlist.Add(building);
					}
				}

				// update inventory
				inventoryController.UpdateInventoryFromGrid(buildlist.ToArray());

				// notice player from debts
				PopUp.Say(inventoryController.GetMessage());

				// apply maluses
				double[] maluses = inventoryController.GetMaluses();
				impactsManager.AddSocial(maluses[0]);
				impactsManager.AddEcology(maluses[1]);
				impactsManager.AddEconomy(maluses[2]);
			}
			else
			{
				GD.Print("RoundController not set correctly");
			}
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
