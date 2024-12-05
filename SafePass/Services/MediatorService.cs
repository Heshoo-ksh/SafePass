using System.Collections.Generic;

namespace SafePass.Services
{
    public class MediatorService : IMediatorService
    {
        private readonly Dictionary<string, List<Action<object>>> _callbacks = new();
        private object _lastPasswordHealth;

        public void Notify(string eventName, object data = null)
        {

            if (eventName == "PasswordHealthChanged")
            {
                _lastPasswordHealth = data; // Save the latest password health
            }

            if (_callbacks.ContainsKey(eventName))
            {
                foreach (var callback in _callbacks[eventName])
                {
                    callback.Invoke(data);
                }
            }
        }

        public void Register(string eventName, Action<object> callback)
        {
            if (!_callbacks.ContainsKey(eventName))
            {
                _callbacks[eventName] = new List<Action<object>>();
            }
            _callbacks[eventName].Add(callback);
            // Deliver the latest password health to newly registered listeners
            if (eventName == "PasswordHealthChanged" && _lastPasswordHealth != null)
            {
                callback.Invoke(_lastPasswordHealth);
            }
        }
    }
}
