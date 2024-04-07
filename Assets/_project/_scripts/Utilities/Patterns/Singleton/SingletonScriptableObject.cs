using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Xomrac.Shmups.Utilities.Patterns.Singleton
{

	public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
	{
		private static T instance = null;

		public static T Instance
		{
			get{
				if (!instance)
				{
					instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
				}
			#if UNITY_EDITOR
				if (!instance)
				{
					string[] configsGUIDs = AssetDatabase.FindAssets("t:" + typeof(T).Name);
					if (configsGUIDs.Length > 0)
					{
						instance = AssetDatabase.LoadAssetAtPath<T>(
							AssetDatabase.GUIDToAssetPath(configsGUIDs[0]));
					}
				}
			#endif
				return instance;
			}
		}

	#if UNITY_EDITOR
		public void OnEnable()
		{
			AddToPreloaded();
		}

		private void AddToPreloaded()
		{
			if (Instance != null)
			{
				List<Object> preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
				if (!preloadedAssets.Contains(Instance))
				{
					preloadedAssets.Add(Instance);
					PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
				}
			}
		}
	#endif
	}
}