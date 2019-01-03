using UnityEngine;

namespace LUT
{

	/// <summary>
	/// Singleton pattern implemented using unique game objects. 
	/// Modifications made by Lawendt and Fabio Damian 
	/// Available @ https://github.com/Lawendt/UnityLawUtilities
	/// Original can be found @ http://wiki.unity3d.com/index.php/Singleton
	/// </summary>
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		public bool _dontDestroyOnLoad = false;
		public bool _destroyExistentObject = false;
		private static T _instance;

		private static object _lock = new object();


		public static bool haveInstance()
		{
			if (applicationIsQuitting)
			{
				return false;
			}
			if (_instance == null)
			{
				_instance = (T)FindObjectOfType(typeof(T));
			}

			if (_instance == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public static T Instance
		{

			get
			{
				if (applicationIsQuitting)
				{
					Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
						"' already destroyed on application quit." +
						" Won't create again - returning null.");
					return null;
				}
				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = (T)FindObjectOfType(typeof(T));

						if (FindObjectsOfType(typeof(T)).Length > 1)
						{
							Debug.LogError("[Singleton] Something went really wrong " +
								" - there should never be more than 1 singleton!" +
								" Reopening the scene might fix it.");
							return _instance;
						}

						if (_instance == null)
						{

							GameObject singleton = new GameObject("(singleton)<" + typeof(T) + ">", typeof(T));

							DontDestroyOnLoad(singleton);


							/*Debug.Log("[Singleton] An instance of " + typeof(T) +
								" is needed in the scene, so '" + singleton +
								"' was created with DontDestroyOnLoad.");
							*/
							_instance = singleton.GetComponent<T>();

						}
						else
						{
							//Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);
						}
					}
					return _instance;
				}
			}
		}

		private static bool applicationIsQuitting = false;

		virtual public void Awake()
		{
			VerifyExistence();

			applicationIsQuitting = false;

			if (_instance == null)
			{
				_instance = this.GetComponent<T>();
			}

			if (_dontDestroyOnLoad)
			{
				DontDestroyOnLoad(this.gameObject);
			}
		}

		public void OnEnable()
		{
			//print("Enable " + this.name);
			applicationIsQuitting = false;

			VerifyExistence();
		}

		void Start()
		{
			VerifyExistence();
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

		void VerifyExistence()
		{
			if (_instance != null && _instance != this)
			{
				if (_destroyExistentObject)
				{
					Destroy(this.gameObject);
				}
				else
				{
					Destroy(this);
				}
			}
		}
	}
}