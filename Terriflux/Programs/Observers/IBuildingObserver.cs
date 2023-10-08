using System.Collections.Generic;
using Terriflux.Programs.Model;

namespace Terriflux.Programs.Observers
{
    public interface IBuildingObserver // TODO- apart from the management, nothing will change, so is it useful?
    {
        void UpdateDirection(Direction2D direction);
    }
}