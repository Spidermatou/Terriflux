using Godot;
using System;
using System.Collections.Generic;
using Terriflux.Programs;

namespace Terriflux.Programs.Model.Placeables{
	/// <summary>
	/// Represents a warehouse.
	/// </summary>
	public partial class Warehouse : BuildingModel
	{
		public Warehouse (string name) : base(name) { }
		
		//l'inventaire qui contient les ressources
		private IInventory inventory;

		//liste des éléments qui ont accès à l'entrepot
		private List<BuildingModel> voisins = new List<BuildingModel>();
		
		public List<BuildingModel> GetVoisins(){
			return this.voisins;
		}

		public void AddNeighbour(BuildingModel neigh){
			this.voisins.Add(neigh);
//			foreach (BuildingModel aneigh in this.voisins){
//				GD.Print(aneigh.Verbose());
//			}
		}
		
		public IInventory GetInventory(){
			return this.inventory;
		}
		
		public void SetInventory(IInventory inventory)
		{
			this.inventory = inventory;
			inventory.GetWarehouses().Add(this);
		}
	}
}

