using Godot;
using System;
using Terriflux.Programs.Model.Placeables;

public interface IIventory
{
	void Show();
	void Hide();
	void Add(FlowKind flow, int amount);
	void Remove(FlowKind flow, int amount);
	void Contains(FlowKind flow);

    /// <param name="flow"></param>
    /// <param name="amount"></param>
    /// <returns>True if the inventory contains at least the quantity "amount", false otherwise </returns>
    bool ContainsEnough(FlowKind flow, int amount);
}
