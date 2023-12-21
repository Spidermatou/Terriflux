using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terriflux.Programs.Model.Cell;
using Terriflux.Programs.Model.Grid;
using Terriflux.Programs.Model.Placeables;

namespace Terriflux.Programs.Controller
{
	public partial class InventoryController
	{
		private readonly IInventory inventory;
		private readonly List<BuildingModel> blockedBuildings = new();

		public InventoryController(IInventory inventory) { this.inventory = inventory; }

		/// <summary>
		/// Update the quantities of each flow in the inventory according to needs and production.
		/// And indicate which buildings are blocked(cannot work this turn).
		/// </summary>
		/// <param name="grid"></param>
		
		public IInventory GetInventory()
		{
			return this.inventory;
		}
		public void UpdateInventoryFromGrid(BuildingModel[] buildings)
		{
			foreach (BuildingModel building in buildings)
			{
				bool canWork;
				if(this.inventory.GetWarehouses().Contains(building))
				{
					canWork = true;
				}
				else
				{
					canWork = false;
					this.blockedBuildings.Add(building); // Ajout du bâtiment bloqué
				}

				// Vérification si le bâtiment est déjà dans la liste des bâtiments bloqués
				if (!blockedBuildings.Contains(building))
				{
					// Vérification des besoins du bâtiment
					foreach (FlowKind flow in building.GetNeedsKind())
					{
						if (!this.inventory.ContainsEnough(flow, building.GetQuantityNeeded(flow)))
						{
							canWork = false;
							this.blockedBuildings.Add(building); // Ajout du bâtiment bloqué
							break; // Sortir de la boucle dès qu'un besoin n'est pas satisfait
						}
					}

					// Traitement si le bâtiment peut travailler
					if (canWork)
					{
						// Suppression des besoins du bâtiment de l'inventaire
						foreach (FlowKind flow in building.GetNeedsKind())
						{
							this.inventory.Remove(flow, building.GetQuantityNeeded(flow));
						}

						// Ajout des produits du bâtiment à l'inventaire
						foreach (FlowKind flow in building.GetProductionKind())
						{
							this.inventory.Add(flow, building.GetQuantityProduced(flow));
						}
					}
				}
			}
		}


		/// <returns>Table size 3 representing the malus for sociabilty, 
		/// then ecology, then economy.</returns>
		public double[] GetMaluses()
		{
			const int STANDAR_MALUS = -1;
			int nbOfBlockedBuilding = blockedBuildings.Count;
			double[] malus = new double[3];

			malus[0] = STANDAR_MALUS * nbOfBlockedBuilding;
			malus[1] = STANDAR_MALUS * nbOfBlockedBuilding;
			malus[2] = STANDAR_MALUS * nbOfBlockedBuilding;

			return malus;
		}

		public string GetMessage()  // TODO show also their coordinates
		{
			StringBuilder sb = new();
			sb.AppendLine("Les batiments suivants n'ont pas acces a suffisament de flux necessaire a leur fonctionnement :");

			foreach (BuildingModel building in this.blockedBuildings)
			{
				sb.Append(building.GetName())
					.Append( " in ")
					.Append(" necessite ");

				foreach (FlowKind flow in building.GetNeedsKind() )
				{
					sb.Append(building.GetQuantityNeeded(flow))
						.Append(" " + flow)
						.Append(" but you have only ")
						.Append(inventory.GetQuantityOf(flow))
						.Append(" | ");
				}

				sb.Append('\n');
			}

			return sb.ToString();
		}
	}
}
