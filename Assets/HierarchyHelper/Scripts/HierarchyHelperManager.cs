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

		private static Dictionary<Type, List<MethodInfo>> _createdHelper = null;
		private static Dictionary<MethodInfo,string> _categoryMap = null;
		private static SortedList<int,List<MethodInfo>> _priorityMap = null;
		private static float _preservedWidth = 200f;

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

		static HierarchyHelperManager()
		{
			_createdHelper = new Dictionary<Type, List<MethodInfo>>(); 
			_priorityMap = new SortedList<int, List<MethodInfo>>();
			_categoryMap = new Dictionary<MethodInfo, string>();
			Categroies = new SortedList<string, int>();

			List<MethodInfo> methods = FindMethodsWithAttribute<HelperInfoAttribute>().ToList();
			foreach( MethodInfo m in methods )
			{
				object[] objs = m.GetCustomAttributes( typeof( HelperInfoAttribute ), false );
				foreach( object obj in objs )
				{
					if( !_createdHelper.ContainsKey( m.DeclaringType ) )
						_createdHelper[m.DeclaringType] = new List<MethodInfo>();
					_createdHelper[m.DeclaringType].Add( m );

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

			if( _createdHelper.Count > 0 )
			{
				EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
			}
		}

		private static void OnHierarchyGUI( int instanceID, Rect r )
		{
			if( !Showing || Event.current.type != EventType.Repaint )
				return;
			
			GameObject go = (GameObject)UnityEditor.EditorUtility.InstanceIDToObject( instanceID );

			if( go == null )
				return;

			_controlRect = r;
			if( _controlRect.x <= _preservedWidth )
			{
				float diff = _preservedWidth - _controlRect.x;
				_controlRect.x = _preservedWidth;
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

		#pragma warning disable CS0649
		private static Rect _controlRect;
		#pragma warning restore CS0649
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
