using Core;
using Data;
using System;
using System.Collections.Generic;
using Tools;

namespace Features.AbilitiesFeature
{
    public class AbilityRepository : BaseController, IRepository<int, IAbility>
    {
        public IReadOnlyDictionary<int, IAbility> Content { get => _abilitiesMap; }

        private Dictionary<int, IAbility> _abilitiesMap = new Dictionary<int, IAbility>();

        public AbilityRepository(IReadOnlyList<AbilityItemConfig> abilities)
        {
            foreach (var config in abilities)
            {
                _abilitiesMap[config.Id] = CreateAbility(config);
            }
        }

        private IAbility CreateAbility(AbilityItemConfig config)
        {
            switch (config.Type)
            {
                case AbilityType.None:
                    return AbilityStub.Default;
                case AbilityType.Gun:
                    return new GunAbility(config.View, config.value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class AbilityStub : IAbility
    {
        public static AbilityStub Default { get; } = new AbilityStub();

        public void Apply(IAbilityActivator activator)
        {
        }
    }
}