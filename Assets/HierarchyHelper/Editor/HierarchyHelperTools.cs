using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HierarchyHelper
{
	public static class HierarchyHelperTools
	{
		private static Texture2D _whiteTexture;
		public static Texture2D WhiteTexture
		{
			get
			{
				if( _whiteTexture == null )
					_whiteTexture = new Texture2D( 2, 2 );

				return _whiteTexture;
			}
		}

		public static void DrawWithColor( Color color, Action draw )
		{
			if( draw != null )
			{
				Color c = GUI.color;
				GUI.color = color;
				draw();
				GUI.color = c;
			}
		}

		public static Vector2 GetContentSize( GUIContent c )
		{
			return GUI.skin.label.CalcSize( c );
		}
	}
}
