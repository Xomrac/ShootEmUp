using System;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;

namespace Xomrac.Shmups._project._scripts.UI
{

	public class GameOverUIController : MonoBehaviour
	{
		[SerializeField] private SceneReference _loadingScene;
		[SerializeField] private SceneReference _menuScene;
		[SerializeField] private SceneReference _firstLevelScene;
		[SerializeField] private GameObject _hidableGroup;
		[SerializeField] private Button _playAgainButton;
		[SerializeField] private Button _toMenuButton;
		private void Awake()
		{
			_hidableGroup.SetActive(false);
			_playAgainButton.onClick.AddListener(()=>Loader.Load(_loadingScene,_firstLevelScene));
			_toMenuButton.onClick.AddListener(() => Loader.Load(_loadingScene, _menuScene));
		}
		private void DispalyGameOverUI()
		{
			_hidableGroup.SetActive(true);
		}

		private void OnEnable()
		{
			GameManager.OnGameOver += DispalyGameOverUI;
		}

		private void OnDisable()
		{
			GameManager.OnGameOver -= DispalyGameOverUI;
		}

	}

}