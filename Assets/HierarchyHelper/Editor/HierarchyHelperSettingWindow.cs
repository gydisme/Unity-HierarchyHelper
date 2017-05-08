using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HierarchyHelper
{
	public class HierarchyHelperSettingWindow : EditorWindow
	{
		const string MENU_PATH_SETTING_WINDOW = "Tools/HierarchyHelper/Open Setting Window";
		const string MENU_PATH_ENABLED = "Tools/HierarchyHelper/Enabled";

		[MenuItem(MENU_PATH_SETTING_WINDOW, false, 1)]
		public static void Create()
		{
			HierarchyHelperSettingWindow t = GetWindow<HierarchyHelperSettingWindow>( "Hierarchy Helper" );
			t.Show();
		}
			
		Vector2 _scrollPosition = Vector2.zero;
		void OnGUI()
		{
			GUI.changed = false;

			EditorGUILayout.Space();
			HierarchyHelperManager.Showing = EditorGUILayout.ToggleLeft( "Enable Helper System", HierarchyHelperManager.Showing );

			EditorGUI.BeginDisabledGroup( !HierarchyHelperManager.Showing );
			{
				HierarchyHelperManager.PreservedWidth = EditorGUILayout.IntSlider( "Preserved Width", HierarchyHelperManager.PreservedWidth, 100, 500 );
				EditorGUILayout.Space();

				EditorGUILayout.BeginHorizontal();
				{
					GUILayout.Label( "No.", GUILayout.Width( 40f ) );
					GUILayout.Label( "Count", GUILayout.Width( 40f ) );

					EditorGUILayout.LabelField( "Categroy", "Showing" );
				}
				EditorGUILayout.EndHorizontal();

				_scrollPosition = EditorGUILayout.BeginScrollView( _scrollPosition );

				int i=1;
				foreach( string c in HierarchyHelperManager.Categroies.Keys )
				{
					EditorGUILayout.BeginHorizontal();
					{
						GUILayout.Label( i++.ToString(), GUILayout.Width( 40f ) );
						GUILayout.Label( HierarchyHelperManager.Categroies[c].ToString(), GUILayout.Width( 40f ) );

						bool isOn = HierarchyHelperManager.GetShowing( c );
						bool tempOn = EditorGUILayout.Toggle( c, isOn );
						if( isOn != tempOn )
						{
							HierarchyHelperManager.SetShowing( c, tempOn );
						}
					}
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUI.EndDisabledGroup();
			EditorGUILayout.EndScrollView();

			if( GUI.changed )
				EditorApplication.RepaintHierarchyWindow();
		}
	}
}
