using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xomrac.Shmups._project._scripts.UI;
using Xomrac.Shmups._project._scripts.Utilities;

namespace Xomrac.Shmups
{
    public class GameManager : MonoBehaviour
    {
        public static Action OnGameOver;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private EntityHealth _playerHealth;
        private void Awake()
        {
            Time.timeScale = 1;
        }

        private void Start()
        {
            _playerHealth.EntityDiedEvent += OnPlayerDeath;
            EntityReward.EntityRewardEvent += _scoreManager.AddScore;
        }

        private void OnDestroy()
        {
            EntityReward.EntityRewardEvent -= _scoreManager.AddScore;
        }

        private void OnPlayerDeath(object sender, EventArgs e)
        {
            OnGameOver?.Invoke();
            Time.timeScale = 0;
        }

    }
}
