using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public static class NonMonoTest2
{
	[HelperInfo( "NonMonoTest2", 0)]
	public static void DrawHelper( GameObject obj )
	{
		Rect rect = HierarchyHelperManager.GetControlRect( 20f );
		GUI.Label(rect, "N2");
	}
}
