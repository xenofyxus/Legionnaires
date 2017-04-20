using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace AssemblyCSharpEditor
{
	/*[CustomPropertyDrawer (typeof(AssemblyCSharp.UnitBehaviour.OnHitItem))]
	public class OnHitItemEditor : PropertyDrawer
	{
		static Type[] onHitEffects = System.Reflection.Assembly.GetAssembly(typeof(AssemblyCSharp.Spells.OnHit)).GetTypes().Where((type) => type.IsSubclassOf(typeof(AssemblyCSharp.Spells.OnHit))).ToArray();
		static string[] options = onHitEffects.Select((element) => element.Name).ToArray();

		public override void OnGUI (UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label)
		{
			EditorGUI.BeginProperty (position, label, property);

			position = EditorGUI.PrefixLabel(position,EditorGUIUtility.GetControlID(UnityEngine.FocusType.Passive),label);

			//property.objectReferenceValue = Activator.CreateInstance(onHitEffects[EditorGUI.Popup(new UnityEngine.Rect(position.x,position.y,120,position.height), Array.IndexOf(onHitEffects,property.objectReferenceValue), options)]);

			EditorGUI.EndProperty ();
		}

	}*/
}

