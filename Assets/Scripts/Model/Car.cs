public class Car: IUpgradeableCar
{
    public float Speed { get; set; }

    private float _defaultSpeed;

    public Car(float speed)
    {
        _defaultSpeed = speed;
        Restore();
    }

    public void Restore()
    {
        Speed = _defaultSpeed;
    }

}