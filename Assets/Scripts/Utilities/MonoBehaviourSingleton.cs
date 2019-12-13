using UnityEngine;

namespace CityBuilder.Utils
{
    ///<summary>
    /// Use this generic to convert your class into a Singleton.
    /// If you want to add extra initialization steps to your singleton, just override the InitializeSingleton method.
    /// You can create a persistent singleton by calling SetAsPersistentSingleton method.
    ///</summary>
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }
                else
                {
                    var instances = FindObjectsOfType<T>();
                    if (instances.Length > 0)
                    {
                        _instance = instances[0];
                    }
                    if (instances.Length > 1)
                    {
                        Debug.LogError("There are more than one instances of " + typeof(T).Name);
                    }
                    if (!_instance)
                    {
                        _instance = CreateDefaultInstance();
                    }
                    return Instance;
                }
            }
        }

        /// <summary>
        /// Initializes singleton or destroy it if there is one already
        /// </summary>
        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                InitializeSingleton();
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        /// <summary>
        /// Creates a default singleton instance
        /// </summary>
        /// <returns></returns>
        public static T CreateDefaultInstance()
        {
            string typeName = typeof(T).Name;
            GameObject singletonInstance = null;
            
            singletonInstance = new GameObject(typeName);
            singletonInstance.AddComponent<T>();
            T singleton = singletonInstance.GetComponent<T>();
            MonoBehaviourSingleton<T> genericSingleton = singleton as MonoBehaviourSingleton<T>;
            genericSingleton.InitializeSingleton();
            return singleton;
        }

        /// <summary>
        /// Set the singleton as a persistent game object
        /// </summary>
        public void SetAsPersistentSingleton()
        {
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Abstract method that initializes the singleton
        /// </summary>
        public abstract void InitializeSingleton();
    }
}

