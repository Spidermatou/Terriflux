using System.Collections.Generic;

public interface IBuildingObserver 
{
    void UpdateImpacts(int[] impacts);
    void UpdateInfluenceScale(InfluenceScale actualInfluenceScale);
    void UpdateProducts(Dictionary<FlowKind, int> products);
    void UpdateNeeds(Dictionary<FlowKind, int> needs);
    void UpdateOccupation(int occupation);
}