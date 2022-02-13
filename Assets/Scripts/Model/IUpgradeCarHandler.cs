namespace Model
{
    public interface IUpgradeCarHandler
    {
        IUpgradeableCar Upgrade(IUpgradeableCar car);
    }
}