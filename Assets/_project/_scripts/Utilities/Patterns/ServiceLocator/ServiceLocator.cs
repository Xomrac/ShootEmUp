using System;
using System.Collections.Generic;
using UnityEngine;

namespace Xomrac.Shmups.Utilities.Patterns.ServiceLocator
{

    public abstract class ServiceLocator : MonoBehaviour
    {
        public readonly Dictionary<Type, ServiceComponent> components = new();

        public T GetService<T>() where T : ServiceComponent
        {
            return GetComponentFromDictionary<T>();
        }

        public T GetEnabledService<T>() where T : ServiceComponent
        {
            var component = GetComponentFromDictionary<T>();
            return (component != null && component.enabled) ? component : null;
        }

        public T AddService<T>(bool addEnabled = true) where T : ServiceComponent
        {
            var component = GetComponentFromDictionary<T>();
            if (component != null)
            {
                return component;
            }

            var newService = gameObject.AddComponent<T>();
            newService.enabled = addEnabled;
            components.Add(typeof(T), newService);
            return newService;
        }

        public void RemoveService<T>() where T : ServiceComponent
        {
            var component = GetComponentFromDictionary<T>();
            if (component == null)
            {
                return;
            }

            components.Remove(typeof(T));
            Destroy(component);
        }

        private T GetComponentFromDictionary<T>() where T : ServiceComponent
        {
            // Try to get the exact type T from the components dictionary
            if (components.TryGetValue(typeof(T), out ServiceComponent serviceComponent))
            {
                return (T)serviceComponent;
            }
            else
            {
                // If not found, look for types that derive from T
                foreach (var pair in components)
                {
                    if (pair.Key.IsSubclassOf(typeof(T)))
                    {
                        // cast the component to T before returning
                        return (T)pair.Value;
                    }
                }
            }

            return null; // Return null if we haven't found anything
        }
    }

}
