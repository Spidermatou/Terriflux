using Godot;
using System;

public interface IVerbosable 
{
    /// <summary>
    /// Provides further information about this object.
    /// </summary>
    /// <returns></returns>
    public string Verbose();
}
