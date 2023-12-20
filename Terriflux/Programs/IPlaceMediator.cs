using Godot;
using System;

public interface IPlaceMediator
{
    /// <summary>
    /// Request the appropriate response from the mediator.
    /// </summary>
    /// <param name="sender"></param>
    void Notify(IPlaceMediator sender);
}
