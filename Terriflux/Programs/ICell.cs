using Godot;
using System;

namespace Terriflux.Programs
{
    public interface ICell
    {
        /// <returns>The size of the cell.</returns>
        Vector2I GetDimensions();

        bool IsSelected();

        /// <summary>
        /// Starts cell selection feedback.
        /// </summary>
        void Select();

        /// <summary>
        /// Ends cell selection feedback.
        /// </summary>
        void Unselect();

        void AddObserver(ICellObserver observer);
        void RemoveObserver(ICellObserver observer);
    }
}