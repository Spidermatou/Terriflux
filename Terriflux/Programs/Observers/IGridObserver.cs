using Terriflux.Programs.Model.Grid;

namespace Terriflux.Programs.Observers
{
    public interface IGridObserver
    {
        public void UpdateMap(GridModel grid);
    }
}