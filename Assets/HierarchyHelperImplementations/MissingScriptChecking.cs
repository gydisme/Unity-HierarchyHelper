#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public class MissingScriptChecking
{
	private static GUIContent _yellowTooltip;
	private static GUIContent _redTooltip;
	private static GUIContent _greenTooltip;

	[HelperInfoAttribute( "Check Missing", -1000 )]
	public static void DrawHelper( GameObject obj )
	{
		if( _yellowTooltip == null )
		{
			_yellowTooltip = new GUIContent( string.Empty, "Contains Missing Script(s) in Children" );
			_redTooltip = new GUIContent( string.Empty, "Contains Missing Script(s)" );
			_greenTooltip = new GUIContent( string.Empty );
		}
		
		Rect rect = HierarchyHelperManager.GetControlRect( 4 );
		rect.y += 1;
		rect.height -= 2;

		Component[] allComponents = obj.GetComponentsInChildren<Component>( true );
		bool found = false;
		bool isMissingOnThis = false;
		foreach( Component allC in allComponents )
		{
			if( allC == null )
			{
				Component[] myComponents = obj.GetComponents<Component>( );
				foreach( Component myC in myComponents )
				{
					if( myC == null )
					{
						isMissingOnThis = true;
						break;
					}
				}
				found = true;
				break;
			}
		}

		HierarchyHelperTools.DrawWithColor( found ? isMissingOnThis ? Color.red : Color.yellow : Color.green, ()=>{
			GUI.DrawTexture( rect, HierarchyHelperTools.WhiteTexture, ScaleMode.StretchToFill, true );
			GUI.Label( rect, found ? isMissingOnThis ? _redTooltip : _yellowTooltip : _greenTooltip );
		});
	}
}
#endif