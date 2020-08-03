using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public static class NonMonoTestSub1
{
    static Texture2D t = null;
	[HelperInfo( "NonMonoTest1", 1)]
	public static void DrawHelper( GameObject obj )
	{
		if( t == null )
			t = new Texture2D(16, 16);
		Rect rect = HierarchyHelperManager.GetControlRect( 30f );
		GUI.Label( rect, t );
	}

	[HelperInfo( "NonMonoTest2", 2)]
	public static void DrawHelper2( GameObject obj )
	{
		if( t == null )
			t = new Texture2D(16, 16);
		Rect rect = HierarchyHelperManager.GetControlRect( 16f );
		GUI.Label( rect, t );
	}
}
