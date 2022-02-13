using Data;

namespace Model
{
    public class SpeedUpgradeHandler : IUpgradeCarHandler
    {
        private readonly UpgradeItemConfig _config;

        public SpeedUpgradeHandler(UpgradeItemConfig config)
        {
            _config = config;
        }

        public IUpgradeableCar Upgrade(IUpgradeableCar car)
        {
            car.Speed += _config.ValueUpgrade;
            return car;
        }
    }
}