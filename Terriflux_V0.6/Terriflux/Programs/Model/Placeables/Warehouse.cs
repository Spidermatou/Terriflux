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
		//double[] impacts = new double[]{2.0, 2.0, 2.0};
		public Warehouse () : base("Warehouse") { }

		private List<BuildingModel> voisins = new List<BuildingModel>();
		
		public List<BuildingModel> GetVoisins(){
			return this.voisins;
		}

		public void AddNeighbour(BuildingModel neigh){
			this.voisins.Add(neigh);
			GD.Print(neigh.Verbose());
		}
	}
}

