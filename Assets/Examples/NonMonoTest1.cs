using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public class NonMonoTest1
{
	[HelperInfoAttribute( "NonMonoTest1", 3)]
	public static void DrawHelper( GameObject obj )
	{
		Rect rect = HierarchyHelperManager.GetControlRect( 30f );
		GUI.Label(rect, "N1");
	}
}
