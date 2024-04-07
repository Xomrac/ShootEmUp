using System;
using System.Collections.Generic;
using UnityEngine;

namespace Xomrac.Shmups
{

	public class Pool<T> where T : MonoBehaviour
	{
		private readonly Func<T> _factory;
		private readonly Action<T> _onGet;
		private readonly Action<T> _onRelease;
		private readonly Action<T> _onDestroy;
		private readonly bool _autoExpand;
		private readonly int _defaultSize;
		private readonly int _maxSize;
		private readonly Queue<T> _pool;
		private int _currentSize;

		public Pool(Func<T> factory, Action<T> onGet, Action<T> onRelease, Action<T> onDestroy, bool autoExpand, int defaultSize, int maxSize)
		{
			_factory = factory;
			_onGet = onGet;
			_onRelease = onRelease;
			_onDestroy = onDestroy;
			_autoExpand = autoExpand;
			_defaultSize = defaultSize;
			_maxSize = maxSize;
			_pool = new Queue<T>();
			_currentSize = 0;
		}

		public T Get()
		{
			if (_pool.Count == 0)
			{
				if (_currentSize < _maxSize)
				{
					_currentSize++;
					return _factory();
				}

				if (_autoExpand)
				{
					_currentSize++;
					return _factory();
				}

				return null;
			}

			T item = _pool.Dequeue();
			_onGet(item);
			return item;
		}

		public void Release(T item)
		{
			_onRelease(item);
			_pool.Enqueue(item);
		}

		public void Destroy(T item)
		{
			_onDestroy(item);
			_currentSize--;
		}
	}

}