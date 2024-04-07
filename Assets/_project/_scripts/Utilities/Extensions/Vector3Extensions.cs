using UnityEngine;

namespace Xomrac.Shmups.Utilities.Extensions
{

	public static class Vector3Extensions
	{
		public static Vector3 With(Vector3 thisVector, float? x = null, float? y = null, float? z = null)
		{
			return new Vector3(x ?? thisVector.x, y ?? thisVector.y, z ?? thisVector.z);
		}
		
		public static Vector3 WithX(this Vector3 thisVector, float x)
		{
			return new Vector3(x, thisVector.y, thisVector.z);
		}
		
		public static Vector3 WithY(this Vector3 thisVector, float y)
		{
			return new Vector3(thisVector.x, y, thisVector.z);
		}
		
		public static Vector3 WithZ(this Vector3 thisVector, float z)
		{
			return new Vector3(thisVector.x, thisVector.y, z);
		}
		
		public static Vector3 Add(this Vector3 thisVector, float? x = null, float? y = null, float? z = null)
		{
			return new Vector3(thisVector.x + (x ?? 0), thisVector.y + (y ?? 0), thisVector.z + (z ?? 0));
		}
		
		public static Vector3 AddX(this Vector3 thisVector, float x)
		{
			return new Vector3(thisVector.x + x, thisVector.y, thisVector.z);
		}
		
		public static Vector3 AddY(this Vector3 thisVector, float y)
		{
			return new Vector3(thisVector.x, thisVector.y + y, thisVector.z);
		}
		
		public static Vector3 AddZ(this Vector3 thisVector, float z)
		{
			return new Vector3(thisVector.x, thisVector.y, thisVector.z + z);
		}
	}

}