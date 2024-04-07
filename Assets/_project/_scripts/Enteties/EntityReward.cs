using System;
using UnityEngine;
using Xomrac.Shmups.Enteties;
using Xomrac.Shmups.Utilities.Patterns.ServiceLocator;

namespace Xomrac.Shmups
{

	public class EntityReward : ServiceComponent<EntityController>
	{
		public static event EventHandler<int> EntityRewardEvent;
		[SerializeField] private int _reward;
		public int Reward => _reward;
		
		public void GiveReward()
		{
			EntityRewardEvent?.Invoke(this, _reward);
		}

		public void SetReward(int scoreReward)
		{
			_reward = scoreReward;
		}
	}

}