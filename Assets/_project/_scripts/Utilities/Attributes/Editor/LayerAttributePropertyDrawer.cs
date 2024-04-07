
using UnityEditor;
using UnityEngine;

namespace Xomrac.Shmups.Utilities.Attributes.editor
{
	[CustomPropertyDrawer(typeof(LayerAttribute))]
	public class LayerAttributePropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			property.intValue = EditorGUI.LayerField(position, label, property.intValue);
		}
	}
}