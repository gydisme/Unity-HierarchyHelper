using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public class MonoTestSub1 : MonoBehaviour
{
	[HelperInfoAttribute( "MonoTest1", 9)]
	public void DrawHelper( )
	{
		Rect rect = HierarchyHelperManager.GetControlRect( 15f );
		GUI.Label(rect, "S1");
	}
}
