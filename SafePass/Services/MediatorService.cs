using System.Collections.Generic;

namespace SafePass.Services
{
    public class MediatorService : IMediatorService
    {
        private readonly Dictionary<string, List<Action<object>>> _callbacks = new();

        public void Notify(string eventName, object data = null)
        {
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
        }
    }
}
