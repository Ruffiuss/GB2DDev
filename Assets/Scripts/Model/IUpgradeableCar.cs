namespace Model
{
    public interface IUpgradeableCar
    {
        float Speed { get; set; }
        void Restore();
    }
}