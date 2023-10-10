using Godot;

namespace Terriflux.Programs.Observers
{
    public interface ICellObserver
    {
        void UpdateCellName(string name);

        void UpdatePlacement(Vector2 coordinates);

    }
}