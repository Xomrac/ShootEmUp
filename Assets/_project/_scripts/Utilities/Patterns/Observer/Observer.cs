namespace Xomrac.Shmups._project._scripts.Utilities.Patterns.Observer
{

	using System;
	using System.Reflection;
	using UnityEngine;
	using UnityEngine.Events;

#if UNITY_EDITOR
	using UnityEditor.Events;
#endif

	[Serializable]
	public class Observer<T>
	{
		[SerializeField] private T value;
		[SerializeField] private UnityEvent<T> onValueChanged;

		public T Value
		{
			get => value;
			set => Set(value);
		}

		public static implicit operator T(Observer<T> observer) => observer.value;

		public Observer(T value, UnityAction<T> callback = null)
		{
			this.value = value;
			onValueChanged = new UnityEvent<T>();
			if (callback != null) onValueChanged.AddListener(callback);
		}

		public void Set(T value, bool invoke = true)
		{
			// if (Equals(this.value, value)) return;
			this.value = value;
			if (invoke)
			{
				Invoke();
			}
		}

		public void LogObserver(GameObject context, string variableName)
		{
			Debug.Log($"<color=\"green\"><b>{context.name} - {variableName} Obeserver</b></color>: Invoking {onValueChanged.GetPersistentEventCount()} persistent listeners", context);
		}

		public void Invoke()
		{
			onValueChanged.Invoke(value);
		}

		public void AddListener(UnityAction<T> callback)
		{
			if (callback == null) return;
			onValueChanged ??= new UnityEvent<T>();

		#if UNITY_EDITOR
			UnityEventTools.AddPersistentListener(onValueChanged, callback);
		#else
        onValueChanged.AddListener(callback);
		#endif
		}

		public void RemoveListener(UnityAction<T> callback)
		{
			if (callback == null) return;
			if (onValueChanged == null) return;

		#if UNITY_EDITOR
			UnityEventTools.RemovePersistentListener(onValueChanged, callback);
		#else
        onValueChanged.RemoveListener(callback);
		#endif
		}

		public void RemoveAllListeners()
		{
			if (onValueChanged == null) return;

		#if UNITY_EDITOR
			FieldInfo fieldInfo = typeof(UnityEventBase).GetField("m_PersistentCalls", BindingFlags.Instance | BindingFlags.NonPublic);
			object obj = fieldInfo.GetValue(onValueChanged);
			obj.GetType().GetMethod("Clear").Invoke(obj, null);
		#else
        onValueChanged.RemoveAllListeners();
		#endif
		}
	#if UNITY_EDITOR

	#endif
		public void Dispose()
		{
			RemoveAllListeners();
			onValueChanged = null;
			value = default;
		}
	}

}