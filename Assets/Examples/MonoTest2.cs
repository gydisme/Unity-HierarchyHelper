using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public class MonoTest2 : MonoBehaviour
{
	[HelperInfoAttribute( "MonoTest2", 0)]
	public void DrawHelper( )
	{
		Rect rect = HierarchyHelperManager.GetControlRect( 25f );
		GUI.Label(rect, "M2");
	}
}
