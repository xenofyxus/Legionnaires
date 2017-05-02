using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Game.Units.UnitStat))]
public class UnitStatPropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), label);

		EditorGUI.EndProperty();
	}

}
