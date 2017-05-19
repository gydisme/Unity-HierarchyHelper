using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public class MonoStaticTest1 : MonoBehaviour
{
	[HelperInfoAttribute( typeof(Transform), "MonoStatic1", 1000 )]
	public static void test( Transform t )
	{
		GUIContent c = new GUIContent( t.position.ToString() );
		Vector2 l = HierarchyHelperTools.GetContentSize( c );
		Rect rect = HierarchyHelperManager.GetControlRect( l.x );
		GUI.Label( rect, c );
	}
}
