﻿using System.Collections.Generic;
using Eflatun.SceneReference;
using MEC;
using UnityEngine.SceneManagement;

namespace Xomrac.Shmups
{

	public static class Loader
	{
		private static SceneReference _targetScene;

		public static void Load(SceneReference loadingScene,SceneReference targetScene)
		{
			_targetScene = targetScene;

			SceneManager.LoadScene(loadingScene.Name);
			Timing.RunCoroutine(LoadTargetScene());
		}

		static IEnumerator<float> LoadTargetScene()
		{
			yield return Timing.WaitForOneFrame;
			SceneManager.LoadScene(_targetScene.Name);
		}
	}

}