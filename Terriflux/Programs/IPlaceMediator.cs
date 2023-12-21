using Godot;
using System;

public interface IPlaceMediator
{
    /// <summary>
    /// As a colleague, call request the appropriate response from the mediator. <br></br>
    /// As a mediator, notifies that the cell has been placed on the grid and 
    /// that the consequences have been taken into account.
    /// </summary>
    /// <param name="sender"></param>
    void Notify(IPlaceMediator sender);
}
