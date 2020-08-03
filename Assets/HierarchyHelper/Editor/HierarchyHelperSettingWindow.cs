using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;

namespace HierarchyHelper
{
	public class HierarchyHelperSettingWindow : EditorWindow
	{
		const string MENU_PATH_SETTING_WINDOW = "Tools/HierarchyHelper/Open Setting Window";
		const string MENU_PATH_ENABLED = "Tools/HierarchyHelper/Enabled";

		private static Dictionary<HelperInfoSetting,MethodInfo> _methodMap = null;
		private static Dictionary<string,List<HelperInfoSetting>> _priorityMap = null;

		[MenuItem(MENU_PATH_SETTING_WINDOW, false, 1)]
		public static void Create()
		{
			HierarchyHelperSettingWindow t = GetWindow<HierarchyHelperSettingWindow>( "Hierarchy Helper" );
			t.Show();
		}

		void OnEnable()
		{
			_priorityMap = new Dictionary<string, List<HelperInfoSetting>>();
			_methodMap = new Dictionary<HelperInfoSetting, MethodInfo>();

			var methods = HierarchyHelperManager.FindMethodsWithAttribute<HelperInfoSetting>();
			foreach( MethodInfo m in methods )
			{
				object[] objs = m.GetCustomAttributes( typeof( HelperInfoSetting ), true );
				foreach( object obj in objs )
				{
					HelperInfoSetting attr = obj as HelperInfoSetting;
					if( !_priorityMap.ContainsKey( attr.Category ) )
						_priorityMap[attr.Category] = new List<HelperInfoSetting>();
					_priorityMap[attr.Category].Add( attr );

					_methodMap[attr] = m;
				}
			}

			foreach( List<HelperInfoSetting> list in _priorityMap.Values )
			{
				list.Sort( delegate(HelperInfoSetting x, HelperInfoSetting y) {
					return x.Priority.CompareTo( y.Priority );
				});
			}
		}

		Vector2 _scrollPosition = Vector2.zero;
		void OnGUI()
		{
			GUI.changed = false;

			EditorGUILayout.Space();
			var showing = EditorGUILayout.ToggleLeft( "Enable Helper System", HierarchyHelperManager.Showing );
			if (showing != HierarchyHelperManager.Showing)
				HierarchyHelperManager.Showing = showing;

			EditorGUI.BeginDisabledGroup( !HierarchyHelperManager.Showing );
			{
				HierarchyHelperManager.PreservedWidth = EditorGUILayout.IntSlider( "Preserved Width", HierarchyHelperManager.PreservedWidth, 100, 500 );
				HierarchyHelperManager.Spacing = EditorGUILayout.IntSlider( "Spacing", HierarchyHelperManager.Spacing, 0, 10 );
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

						if( _priorityMap.ContainsKey( c ) )
						{
							foreach( HelperInfoSetting setting in _priorityMap[c] )
							{
								_methodMap[setting].Invoke( null, null );
							}
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
