using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MEC;

namespace Xomrac.Shmups._project._scripts.UI
{

	[Serializable]
	public class ScoreManager
	{
		[SerializeField] private AudioData _pingSound;

		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private int _multipleOf = 1000;
		[SerializeField] private float _animationTime;
		[SerializeField] private float _baseScale = 1f;
		[SerializeField] private float _maxScale = 1.3f;
		[SerializeField] private AnimationCurve _animationCurve;
		[SerializeField] private int _score = 0;
		private CoroutineHandle _animationCoroutine;

		public int Score
		{
			get => _score;
			private set{
				_score = value;
				UpdateScoreText();
			}
		}

		private bool ShouldAnimate => Score % _multipleOf == 0;

		public void AddScore(object _, int scoreToAdd)
		{
			Score += scoreToAdd;
		}

		private void UpdateScoreText()
		{
			_scoreText.text = $"{Score}";
			if (ShouldAnimate)
			{
				SoundManager.Instance.PlaySFX(_pingSound);
				AnimateScoreText(_scoreText.transform.localScale, Vector3.one * _maxScale);
			}
		}

		private void AnimateScoreText(Vector3 startScale, Vector3 endScale)
		{
			Timing.KillCoroutines(_animationCoroutine);
			_animationCoroutine = Timing.RunCoroutine(AnimationCoroutine(startScale, endScale, _scoreText));
		}

		private IEnumerator<float> AnimationCoroutine(Vector3 startScale, Vector3 endScale, TextMeshProUGUI scoreTextToAnimate)
		{
			IEnumerator<float> scaleAnimation = ScaleAnimationCoroutine(startScale, endScale, scoreTextToAnimate);
			while (scaleAnimation.MoveNext()) yield return scaleAnimation.Current;
			IEnumerator<float> scaleBackAnimation = ScaleAnimationCoroutine(endScale, Vector3.one * _baseScale, scoreTextToAnimate);
			while (scaleBackAnimation.MoveNext()) yield return scaleBackAnimation.Current;
		}

		private IEnumerator<float> ScaleAnimationCoroutine(Vector3 startScale, Vector3 endScale, TextMeshProUGUI scoreTextToAnimate)
		{
			float elapsedTime = 0;
			while (elapsedTime < _animationTime)
			{
				float t = elapsedTime / _animationTime;
				float curveValue = _animationCurve.Evaluate(t);
				scoreTextToAnimate.transform.localScale = Vector3.Lerp(startScale, endScale, curveValue);
				elapsedTime += Time.deltaTime;
				yield return Timing.WaitForOneFrame;
			}
		}
	}

}