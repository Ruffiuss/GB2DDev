public class UpgradeHandelrStub : IUpgradeCarHandler
{
    public static IUpgradeCarHandler Default { get; } = new UpgradeHandelrStub();

    public IUpgradeableCar Upgrade(IUpgradeableCar car)
    {
        return car;
    }
}