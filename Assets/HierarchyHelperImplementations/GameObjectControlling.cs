#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public class GameObjectControlling
{
	[HelperInfoAttribute( "GameObject Controlling", 100 )]
	public static void DrawHelper( GameObject obj )
	{
		Rect rect = HierarchyHelperManager.GetControlRect( 10f );
		bool active = GUI.Toggle( rect, obj.activeSelf, string.Empty );
		if( GUI.changed )
			obj.SetActive( active );
	}
}
#endif