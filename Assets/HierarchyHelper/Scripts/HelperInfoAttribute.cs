using UnityEngine;
using System;

namespace HierarchyHelper
{
	public class HelperInfoAttribute : Attribute
	{
		public HelperInfoAttribute( string category, int priority = 0 )
		{
			this.Category = category;
			this.Priority = priority;
		}

		public string Category { get; set; }
		public int Priority { get; set; }
	}
}