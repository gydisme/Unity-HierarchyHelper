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
		const string HELPER_SPACING = "HierarchyHelperSettingWindow.helper.spacing";

		private static Dictionary<MethodInfo,HelperInfoAttribute> _helperInfoMap = null;
		private static SortedList<int,List<MethodInfo>> _priorityMap = null;

		public static Func<GameObject,float> CalculateOffset = null;

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
				if( value )
					EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
				else
					EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyGUI;
			}
		}

		public static int Spacing
		{
			get
			{
				return EditorPrefs.GetInt( HELPER_SPACING, 5 );
			}
			set
			{
				EditorPrefs.SetInt( HELPER_SPACING, value );
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
			_helperInfoMap = new Dictionary<MethodInfo, HelperInfoAttribute>();
			Categroies = new SortedList<string, int>();
			HashSet<string> _cache = new HashSet<string>();

			List<MethodInfo> methods = FindMethodsWithAttribute<HelperInfoAttribute>().ToList();
			foreach( MethodInfo m in methods )
			{
				object[] objs = m.GetCustomAttributes( typeof( HelperInfoAttribute ), true );
				string key = m.DeclaringType.ToString() + "." + m.Name;
				if( !_cache.Contains( key ) )
				{
					_cache.Add( key );

					foreach( object obj in objs )
					{
						HelperInfoAttribute attr = obj as HelperInfoAttribute;
						if( !_priorityMap.ContainsKey( attr.Priority ) )
							_priorityMap[attr.Priority] = new List<MethodInfo>();
						_priorityMap[attr.Priority].Add( m );

						_helperInfoMap[m] = attr;

						if( !Categroies.ContainsKey( attr.Category ) )
							Categroies[attr.Category] = 0;
						Categroies[attr.Category]++;
					}
				}
			}

			if( _helperInfoMap.Count > 0 && Showing )
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

			if( CalculateOffset != null )
				_controlRect.width -= CalculateOffset( go );

			_controlRect.width -= Spacing;

			foreach( int p in _priorityMap.Keys )
			{
				foreach( MethodInfo m in _priorityMap[p] )
				{
					if( GetShowing( _helperInfoMap[m].Category ) )
					{
						if( ( m.Attributes & MethodAttributes.Static ) == 0  )
						{
							object comp = go.GetComponent( m.DeclaringType );
							if( !comp.Equals( null ) )
								m.Invoke( comp, null );
						}
						else if( _helperInfoMap[m].HelperType != null )
						{
							object comp = go.GetComponent( _helperInfoMap[m].HelperType );
							if( !comp.Equals( null ) )
							{
								m.Invoke( null, new object[]{ comp } );
							}
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
			Rect rect = new Rect( _controlRect.x + _controlRect.width, _controlRect.y, width, _controlRect.height );
			_controlRect.width -= Spacing;

			return rect;
		}
		
		#endif
	}
}
