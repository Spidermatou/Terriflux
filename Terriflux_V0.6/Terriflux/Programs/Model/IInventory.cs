using Godot;
using System;
using System.Collections.Generic;
using Terriflux.Programs.Model.Placeables;

public interface IInventory
{
	void Show();
	void Hide();
	bool IsVisibleInTree();
	void Add(FlowKind flow, int amount);
	void Remove(FlowKind flow, int amount);
	bool Contains(FlowKind flow);
	int GetQuantityOf(FlowKind flow);

	/// <param name="flow"></param>
	/// <param name="amount"></param>
	/// <returns>True if the inventory contains at least the quantity "amount", false otherwise </returns>
	bool ContainsEnough(FlowKind flow, int amount);
	List<Warehouse> GetWarehouses();
}
