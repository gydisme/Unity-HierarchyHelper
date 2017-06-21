using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public class MonoTest1 : MonoBehaviour
{
	bool toggle = false;
	[HelperInfoAttribute( "MonoTest1", 9)]
	public void DrawHelper( )
	{
		Rect rect = HierarchyHelperManager.GetControlRect( 16f );
		toggle = GUI.Toggle( rect, toggle, "" );
	}

	#if UNITY_EDITOR
	static float _sliderValue = 0;
	[HelperInfoSetting( "MonoTest1" )]
	public static void DrawHelperSetting( )
	{
		_sliderValue = UnityEditor.EditorGUILayout.Slider( _sliderValue, 0, 100 );
	}
	#endif
}
