using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public static class MonoStaticTest1
{
	[HelperInfo( typeof(Transform), "MonoStatic1", 1000 )]
	public static void test( Transform t )
	{
		GUIContent c = new GUIContent( t.position.ToString() );
		Vector2 l = HierarchyHelperTools.GetContentSize( c );
		Rect rect = HierarchyHelperManager.GetControlRect( l.x + _sliderValue);
		GUI.Label( rect, c );
	}
	
	static float _sliderValue = 0;
	[HelperInfoSetting("MonoStatic1")]
	public static void DrawHelperSetting()
	{
		_sliderValue = UnityEditor.EditorGUILayout.Slider(_sliderValue, 0, 100);
	}
}
