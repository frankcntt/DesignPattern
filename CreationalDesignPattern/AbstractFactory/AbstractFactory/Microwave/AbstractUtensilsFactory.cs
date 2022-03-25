using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Microwave
{
    public abstract class AbstractUtensilsFactory
    {
        public abstract IUtensils GetPlate();
        public abstract IUtensils GetBowl();
    }
    public class MicrowaveSafeFactory : AbstractUtensilsFactory
    {
        public override IUtensils GetBowl()
        {
            return new BowlMV();
        }

        public override IUtensils GetPlate()
        {
            return new PlateMV();
        }
    }

    public class MicrowaveNoSafeFactory : AbstractUtensilsFactory
    {
        public override IUtensils GetBowl()
        {
            return new Bowl();
        }

        public override IUtensils GetPlate()
        {
            return new Plate();
        }
    }
}
