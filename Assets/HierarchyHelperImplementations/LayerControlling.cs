#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using HierarchyHelper;

public class LayerControlling
{
	[HelperInfoAttribute( "Layer Name", -997 )]
	public static void DrawLayerName( GameObject obj )
	{
		GUIContent layerName = new GUIContent( LayerMask.LayerToName( obj.layer ) );
		Vector2 size = GUI.skin.label.CalcSize( layerName );

		Rect rect = HierarchyHelperManager.GetControlRect( size.x );
		GUI.Label( rect, layerName );
	}

	[HelperInfoAttribute( "Layer Control", -998 )]
	public static void DrawLayerVisible( GameObject obj )
	{
		bool visible = ( Tools.visibleLayers & 1 << obj.layer ) > 0;
		Rect rect = HierarchyHelperManager.GetControlRect( 15f );
		bool newVisible = GUI.Toggle( rect, visible, new GUIContent( "", LayerMask.LayerToName( obj.layer ) ) , "VisibilityToggle" );
		if( newVisible != visible )
		{
			Tools.visibleLayers ^= 1 << obj.layer;
			SceneView.RepaintAll();
		}
	}

	static Texture2D _lockedTexture = null;
	static Texture2D _unlockedTexture = null;
	[HelperInfoAttribute( "Layer Control", -999 )]
	public static void DrawLayerLock( GameObject obj )
	{
		bool locked = ( Tools.lockedLayers & 1 << obj.layer ) > 0;
		Rect rect = HierarchyHelperManager.GetControlRect( 15f );
		if( !locked )
		{
			if( _lockedTexture == null )
			{
				_lockedTexture = Resources.Load( "buttonLock" ) as Texture2D;
				_unlockedTexture = Resources.Load( "buttonUnlock" ) as Texture2D;
			}
		}
		bool newLocked = GUI.Toggle( rect, locked, locked ? _lockedTexture :_unlockedTexture, new GUIStyle() );

		if( newLocked != locked )
		{
			Tools.lockedLayers ^= 1 << obj.layer;
		}
	}
}
#endif
