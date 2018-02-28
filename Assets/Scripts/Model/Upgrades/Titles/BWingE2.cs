using Ship;
using Ship.BWing;
using Upgrade;

namespace UpgradesList
{
    public class BWingE2 : GenericUpgrade
    {
        public BWingE2() : base()
        {
            Type = UpgradeType.Title;
            Name = "B-Wing/E2";
            Cost = 1;
            isUnique = true;

            AddedSlots = new List<UpgradeSlot>
            {
                new UpgradeSlot(UpgradeType.Crew)
            };
        }

        public override bool IsAllowedForShip(GenericShip ship)
        {
            return ship is BWing;
        }

    }
}
