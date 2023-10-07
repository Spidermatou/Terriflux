using Godot;
using System;

public interface IVerbosable 
{
    /// <summary>
    /// Provides further information on the object in question.
    /// </summary>
    /// <returns></returns>
    public string Verbose();
}
