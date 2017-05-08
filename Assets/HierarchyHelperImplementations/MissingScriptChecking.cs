#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HierarchyHelper;

public class MissingScriptChecking
{
	private static Texture2D _whiteTexture;
	private static GUIContent _yellowContent;
	private static GUIContent _redContent;
	private static GUIContent _greenContent;

	[HelperInfoAttribute( "Check Missing", -1000 )]
	public static void DrawHelper( GameObject obj )
	{
		if( _whiteTexture == null )
		{
			_whiteTexture = new Texture2D( 5, 16 );
			_yellowContent = new GUIContent( _whiteTexture, "Contains Missing Script(s) in Children" );
			_redContent = new GUIContent( _whiteTexture, "Contains Missing Script(s)" );
			_greenContent = new GUIContent( _whiteTexture );
		}
		
		Rect rect = HierarchyHelperManager.GetControlRect( 10 );
		Color color = GUI.color;
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
		GUI.color = found ? isMissingOnThis ? Color.red : Color.yellow : Color.green;
		GUI.Label( rect, found ? isMissingOnThis ? _redContent : _yellowContent : _greenContent );
		GUI.color = color;
	}
}
#endif