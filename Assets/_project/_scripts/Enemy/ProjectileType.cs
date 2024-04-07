using UnityEngine;

namespace Xomrac.Shmups.Enemy
{

	[CreateAssetMenu(menuName = "ProjectileType", fileName = "Data/Enemy/Projectile Type")]
	public class ProjectileType : ScriptableObject
	{
		[SerializeField] private float movementSpeed;
		public float MovementSpeed => movementSpeed;
		
		[SerializeField] private float damage;
		public float Damage => damage;
		
		[SerializeField] private Mesh modelMesh;
		public Mesh ModelMesh => modelMesh;

		[SerializeField] private Material modelMaterial;
		public Material ModelMaterial => modelMaterial;

		[SerializeField] private float lifeTime;
		public float LifeTime => lifeTime;
		
	}

}