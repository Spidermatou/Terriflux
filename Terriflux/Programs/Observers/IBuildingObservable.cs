using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Terriflux.Programs.Observers
{
    public interface IBuildingObservable
    {
        void RegisterBuildingObserver(IBuildingObserver observer);
        void UnregisterBuildingObserver(IBuildingObserver observer);
        /// <summary>
        /// Force all notifies call for buildings informations
        /// </summary>
        void NotifyAllBuildingInfos();
        void NotifyImpacts();
        void NotifyProducts();
        void NotifyNeeds();
        void NotifyInfluenceScale();

    }
}
