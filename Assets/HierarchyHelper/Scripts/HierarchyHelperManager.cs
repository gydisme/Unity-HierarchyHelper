using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace HierarchyHelper
{
	#if UNITY_EDITOR
	[InitializeOnLoad]
	#endif
	public static class HierarchyHelperManager
	{
		#if UNITY_EDITOR
		const string HELPER_IS_SHOWING = "HierarchyHelperSettingWindow.helper.showing";
		const string HELPER_PRESERVED_WIDTH = "HierarchyHelperSettingWindow.helper.preservedWidth";

		private static Dictionary<MethodInfo,string> _categoryMap = null;
		private static SortedList<int,List<MethodInfo>> _priorityMap = null;

		public static SortedList<string,int> Categroies { get; private set; }
		public static bool Showing
		{
			get
			{
				return EditorPrefs.GetBool( HELPER_IS_SHOWING, false );
			}
			set
			{
				EditorPrefs.SetBool( HELPER_IS_SHOWING, value );
			}
		}

		public static int PreservedWidth
		{
			get
			{
				return EditorPrefs.GetInt( HELPER_PRESERVED_WIDTH, 200 );
			}
			set
			{
				EditorPrefs.SetInt( HELPER_PRESERVED_WIDTH, value );
			}
		}

		static HierarchyHelperManager()
		{
			_priorityMap = new SortedList<int, List<MethodInfo>>();
			_categoryMap = new Dictionary<MethodInfo, string>();
			Categroies = new SortedList<string, int>();

			List<MethodInfo> methods = FindMethodsWithAttribute<HelperInfoAttribute>().ToList();
			foreach( MethodInfo m in methods )
			{
				object[] objs = m.GetCustomAttributes( typeof( HelperInfoAttribute ), false );
				foreach( object obj in objs )
				{
					HelperInfoAttribute attr = obj as HelperInfoAttribute;

					if( !_priorityMap.ContainsKey( attr.Priority ) )
						_priorityMap[attr.Priority] = new List<MethodInfo>();
					_priorityMap[attr.Priority].Add( m );

					_categoryMap[m] = attr.Category;

					if( !Categroies.ContainsKey( attr.Category ) )
						Categroies[attr.Category] = 0;
					Categroies[attr.Category]++;
				}
			}

			if( _categoryMap.Count > 0 )
			{
				EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
			}
		}

		private static void OnHierarchyGUI( int instanceID, Rect r )
		{
			if( !Showing )
				return;
			
			GameObject go = (GameObject)UnityEditor.EditorUtility.InstanceIDToObject( instanceID );

			if( go == null )
				return;

			_controlRect = r;
			if( _controlRect.x <= PreservedWidth )
			{
				float diff = PreservedWidth - _controlRect.x;
				_controlRect.x = PreservedWidth;
				_controlRect.width -= diff;
			}

			foreach( int p in _priorityMap.Keys )
			{
				foreach( MethodInfo m in _priorityMap[p] )
				{
					if( GetShowing( _categoryMap[m] ) )
					{
						if( ( m.Attributes & MethodAttributes.Static ) == 0 )
						{
							object obj = go.GetComponent( m.DeclaringType );
							if( obj != null )
								m.Invoke( obj, null );
						}
						else
						{
							m.Invoke( null, new object[]{ go } );
						}
					}
				}
			}
		}

		public static MethodInfo[] FindMethodsWithAttribute<T>() where T:Attribute
		{
			Assembly assembly = System.Reflection.Assembly.GetAssembly( typeof(T) );

			var methods = assembly.GetTypes()
				.SelectMany(t => t.GetMethods())
				.Where(m => m.GetCustomAttributes( typeof( T ), false).Length > 0)
				.ToArray();

			return methods;
		}

		public static bool GetShowing( string categroy )
		{
			return EditorPrefs.GetBool( HELPER_IS_SHOWING + categroy, true );
		}

		public static void SetShowing( string categroy, bool showing )
		{
			EditorPrefs.SetBool( HELPER_IS_SHOWING + categroy, showing );
		}
		#endif

		#pragma warning disable 0649
		private static Rect _controlRect;
		#pragma warning restore 0649
		public static Rect GetControlRect( float width )
		{
			if( _controlRect.width >= width )
			{
				_controlRect.width -= width;
			}
			else
			{
				width = _controlRect.width;
				_controlRect.width = 0;
			}
			return new Rect( _controlRect.x + _controlRect.width, _controlRect.y, width, _controlRect.height );
		}
	}
}
