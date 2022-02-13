using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MobilityAbility : IAbility
{
    private readonly UnityAction<float> _abilityListener;
    private readonly float _upgradeSpeed;
    private readonly float _upgradeTime;

    public MobilityAbility(UnityAction<float> abilityListener, float upgradeSpeed, float upgradeTime = 5.0f) //transfer to model
    {
        _abilityListener = abilityListener;
        _upgradeSpeed = upgradeSpeed;
        _upgradeTime = upgradeTime;
    }

    public void Apply(IAbilityActivator activator)
    {
        _abilityListener.Invoke(_upgradeSpeed);
        DecreaseSpeed(_upgradeTime);
    }

    IEnumerator DecreaseSpeed(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        _abilityListener.Invoke(_upgradeSpeed * -1);
    }
}
