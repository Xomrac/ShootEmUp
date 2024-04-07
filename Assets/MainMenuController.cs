using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Xomrac.Shmups._project._scripts.Utilities;

namespace Xomrac.Shmups
{

	public class MainMenuController : MonoBehaviour
	{
		private const string SOURCE_URL = "";
		[SerializeField] private SceneReference _loadingScene;
		[SerializeField] private SceneReference _firstLevelScene;
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _sourceButton;
		[SerializeField] private Button _exitButton;

		private void Awake()
		{
			_exitButton.onClick.AddListener(Helpers.QuitGame);
			_playButton.onClick.AddListener(() => Loader.Load(_loadingScene,_firstLevelScene));
			_sourceButton.onClick.AddListener(() => Application.OpenURL(SOURCE_URL));
		}

	}

}