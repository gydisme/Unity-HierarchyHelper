#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using HierarchyHelper;

public class LayerControlling
{
	[HelperInfoAttribute( "Layer Name", 999 )]
	public static void DrawLayerName( GameObject obj )
	{
		GUIContent layerName = new GUIContent( LayerMask.LayerToName( obj.layer ) );
		Vector2 size = HierarchyHelperTools.GetContentSize( layerName );

		Rect rect = HierarchyHelperManager.GetControlRect( size.x );
		GUI.Label( rect, layerName );
	}

	[HelperInfoAttribute( "Layer Control", -998 )]
	public static void DrawLayerVisible( GameObject obj )
	{
		bool visible = ( Tools.visibleLayers & 1 << obj.layer ) > 0;
		Rect rect = HierarchyHelperManager.GetControlRect( 10 );
		bool newVisible = GUI.Toggle( rect, visible, new GUIContent( "", LayerMask.LayerToName( obj.layer ) ) , "VisibilityToggle" );
		if( newVisible != visible )
		{
			Tools.visibleLayers ^= 1 << obj.layer;
			SceneView.RepaintAll();
		}
	}

	[HelperInfoAttribute( "Layer Control", -999 )]
	public static void DrawLayerLock( GameObject obj )
	{
		bool locked = ( Tools.lockedLayers & 1 << obj.layer ) > 0;
		Rect rect = HierarchyHelperManager.GetControlRect( 16f );

		bool newLocked = locked;

		if( !locked )
		{
			HierarchyHelperTools.DrawWithColor( new Color(1f,1f,1f,0.25f), ()=>
				newLocked = GUI.Toggle( rect, locked, new GUIContent( "", LayerMask.LayerToName( obj.layer ) ), "IN LockButton" ) );
		}
		else
		{
			newLocked = GUI.Toggle( rect, locked, new GUIContent( "", LayerMask.LayerToName( obj.layer ) ), "IN LockButton" );
		}

		if( newLocked != locked )
		{
			Tools.lockedLayers ^= 1 << obj.layer;
		}
	}
}
#endif
