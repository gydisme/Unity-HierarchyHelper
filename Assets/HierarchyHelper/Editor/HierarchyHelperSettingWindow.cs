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
			

		void OnGUI()
		{
			GUI.changed = false;

			EditorGUILayout.Space();
			HierarchyHelperManager.Showing = EditorGUILayout.ToggleLeft( "Enable Helper System", HierarchyHelperManager.Showing );
			HierarchyHelperManager.PreservedWidth = EditorGUILayout.IntSlider( "Preserved Width", HierarchyHelperManager.PreservedWidth, 100, 500 );
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			{
				GUILayout.Label( "Used", GUILayout.Width( 40f ) );

				EditorGUILayout.LabelField( "Categroy", "Showing" );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUI.BeginDisabledGroup( !HierarchyHelperManager.Showing );
			{
				foreach( string c in HierarchyHelperManager.Categroies.Keys )
				{
					EditorGUILayout.BeginHorizontal();
					{
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

			if( GUI.changed )
				EditorApplication.RepaintHierarchyWindow();
		}
	}
}
