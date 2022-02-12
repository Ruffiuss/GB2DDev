using System;

namespace Tools
{
    internal class SubscriptionActionT<T> : IReadOnlySubscriptionActionT<T>
    {
        private Action<T> _action;

        public void Invoke(T id)
        {
            _action?.Invoke(id);
        }

        public void SubscribeOnChangeT(Action<T> subscriptionAction)
        {
            _action += subscriptionAction;
        }

        public void UnSubscriptionOnChangeT(Action<T> unsubscriptionAction)
        {
            _action -= unsubscriptionAction;
        }
    }
}