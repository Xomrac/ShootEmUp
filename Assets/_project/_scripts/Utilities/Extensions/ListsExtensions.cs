using System.Collections.Generic;
using UnityEngine;

namespace Xomrac.Shmups.Utilities.Extensions
{

	public static class ListsExtensions
	{
		
		public static T Random<T>(this List<T> elements) where T : class
		{
			return elements.Count == 0 ? null : elements[UnityEngine.Random.Range(0, elements.Count)];
		}
	}

}