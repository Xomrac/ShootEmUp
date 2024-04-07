using UnityEngine;

namespace Xomrac.Shmups.Utilities.Extensions
{

	public static class GameObjectExtensions
	{
		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			var component = gameObject.GetComponent<T>();
			return component != null ? component : gameObject.AddComponent<T>();
		}
	}

}