using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terriflux.Programs
{
    public class BuildingFactory
    {
        public BuildingFactory() { }

        public Building CreateField() { return (Building)RawNode.Instantiate("Field"); }
        public Building CreateFactory() { return (Building)RawNode.Instantiate("Factory"); }
        public Building CreateBakery() { return (Building)RawNode.Instantiate("Bakery"); }
        public Building CreateEnergySupplier() { return (Building)RawNode.Instantiate("EnergySupplier"); }
        public Building CreateShaft() { return (Building)RawNode.Instantiate("Shaft"); }
        public Building CreateGrocery() { return (Building)RawNode.Instantiate("Grocery"); }
        public Building CreateWarehouse() { return (Building)RawNode.Instantiate("Warehouse"); }
    }
}
