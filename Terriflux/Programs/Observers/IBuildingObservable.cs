using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terriflux.Programs.Observers
{
    public interface IBuildingObservable
    {
        public void SetObserver(IBuildingObserver observer);
        public void NotifyImpacts();
    }
}
