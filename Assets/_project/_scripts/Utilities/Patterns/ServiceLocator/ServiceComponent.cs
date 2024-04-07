using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xomrac.Shmups.Utilities.Patterns.ServiceLocator
{
    public abstract class ServiceComponent : MonoBehaviour
    { }

    public abstract class ServiceComponent<T> : ServiceComponent where T : ServiceLocator
    {
        public T ServiceLocator { get; private set; }

        protected virtual void Start()
        {
        }

        protected virtual void Awake()
        {
            ServiceLocator = GetComponent<T>();

            if (ServiceLocator == null)
            {
                Debug.LogWarning($"No ServiceLocator found of type {typeof(T)}", this);
                enabled = false;
            }
            else
            {
                ServiceLocator.components.Add(GetType(), this);
            }
        }
    }
}
