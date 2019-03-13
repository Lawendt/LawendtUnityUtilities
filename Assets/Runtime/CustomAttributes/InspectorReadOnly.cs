using UnityEngine;

namespace LUT
{
	/// <summary>
	/// Public fields that you want to easily inspect in the editor without allowing modifications. InspectorReadOnlyDrawer needs to be inside an Editor folder.
	/// Developed by @jringrose
	/// Avaiable @ https://gist.github.com/jringrose
	/// <example> 
	/// </code>
	/// [InspectorReadOnly] 
	/// public float someValue = 5f;
	/// </code>
	/// </example>
	/// </summary>
	public class InspectorReadOnly : PropertyAttribute
	{

	}
}