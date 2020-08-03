using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public static class NonMonoTest1
{
	[HelperInfo( "NonMonoTest1", 3)]
	public static void DrawHelper( GameObject obj )
	{
		Rect rect = HierarchyHelperManager.GetControlRect( 30f );
		GUI.Label(rect, "N1");
	}
}
