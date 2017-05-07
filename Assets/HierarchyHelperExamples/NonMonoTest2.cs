using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public class NonMonoTest2
{
	[HelperInfoAttribute( "NonMonoTest2", 0)]
	public static void DrawHelper( GameObject obj )
	{
		Rect rect = HierarchyHelperManager.GetControlRect( 20f );
		GUI.Label(rect, "N2");
	}
}
