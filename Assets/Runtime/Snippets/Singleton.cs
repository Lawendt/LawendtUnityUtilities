using UnityEngine;

namespace LUT
{

	/// <summary>
	/// Singleton pattern implemented with lazy instation.
	/// 
	/// It assures that only one object exists and if not it creates one gameobject with thes desired script
	/// 
	/// 
	/// Modifications made by Lawendt and Fabio Damian 
	/// Available @ https://github.com/Lawendt/UnityLawUtilities
	/// Original can be found @ http://wiki.unity3d.com/index.php/Singleton
	/// </summary>
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		public bool _persistent = true;
		public bool _destroyExistentObject = true;

		private static T _instance;
		private static object _lock = new object();

		public static bool HasInstance()
		{
			if (applicationIsQuitting)
			{
				return false;
			}
			return _instance;
		}

		public static T Instance
		{
			get
			{
				lock (_lock)
				{
					if (HasInstance())
					{
						_instance = (T)FindObjectOfType(typeof(T));

						if (_instance == null)
						{
							CreateInstance();
						}

					}
					return _instance;
				}
			}
		}

		private static bool applicationIsQuitting = false;

		public virtual void Awake()
		{
			CheckIfDuplicated();

			applicationIsQuitting = false;

			if (_instance == null)
			{
				_instance = GetComponent<T>();
			}

			if (_persistent)
			{
				DontDestroyOnLoad(gameObject);
			}
		}

		public virtual void Start()
		{
			CheckIfDuplicated();
		}


		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		public virtual void OnApplicationQuit()
		{
			applicationIsQuitting = true;
		}

		private void CheckIfDuplicated()
		{
			if (_instance && _instance != this)
			{
				if (_destroyExistentObject)
				{
					Destroy(gameObject);
				}
				else
				{
					Destroy(this);
				}
			}
		}

		private static void CreateInstance()
		{
			GameObject singleton = new GameObject(string.Format("(singleton)<{0}>", typeof(T)));

			_instance = singleton.AddComponent<T>();
		}
	}
}