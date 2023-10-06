using System.Collections.Generic;
using Terriflux.Programs.Model;

namespace Terriflux.Programs.Observers
{
    public interface IBuildingObserver
    {
        void UpdateName(string name);
        void UpdateImpacts(int[] impacts);
        void UpdateInfluence(InfluenceScale actualInfluenceScale);
        void UpdateProducts(Dictionary<FlowKind, int> products);
        void UpdateNeeds(Dictionary<FlowKind, int> needs);
        void UpdateOccupation(int occupation);

        void UpdateDirection(Direction2D direction);
    }
}