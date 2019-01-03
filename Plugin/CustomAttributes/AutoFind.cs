using UnityEngine;


namespace LUT
{
	/// <summary>
	/// AutoFind attribute: automatically find the best component, will search in children if asked nicely, cycle through all found comp
	/// Made by @sandwich_cool
	/// avaiable @ https://twitter.com/sandwich_cool/status/815917262490234881
	/// </summary>
	public class AutoFind : PropertyAttribute
	{
		public System.Type objectType;
		public bool searchInChildren;

		/// <summary>
		/// Add a Find button in the inspector.
		/// </summary>
		/// <param name="ObjectType">Type of the component.</param>
		/// <param name="SearchInChildren">If set to <c>true</c>, will search in children.</param>
		public AutoFind(System.Type ObjectType, bool SearchInChildren = false)
		{
			this.objectType = ObjectType;
			this.searchInChildren = SearchInChildren;
		}
	}
}
