namespace Framework.Singleton
{
    using UnityEngine;

#if ODIN_INSPECTOR
    public class MonobehaviourSingleton<T> : Sirenix.OdinInspector.SerializedMonoBehaviour where T : Sirenix.OdinInspector.SerializedMonoBehaviour
#else
    public class MonobehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
#endif
    {
        private static T p_Instance;

        public static T Instance => Get();

        /// <summary>
        /// Get this singleton.
        /// </summary>
        public static T Get()
        {
            return Get(false);
        }

        private static T Get( bool dontLog )
        {
            if ( p_Instance == null )
            {
                p_Instance = FindObjectOfType<T>();

                if ( p_Instance == null )
                {
                    Debug.LogError("Need to have an instance within the scene in order to make a singleton.\nPlease add a [" + typeof(T).Name + "] to the scene.");
                }
            }

            return p_Instance;
        }

        public static bool Exist()
        {
            return Get(true) != null;
        }

        /// <summary>
        /// Kill this singleton.
        /// </summary>
        public static void Kill()
        {
            Destroy(p_Instance.gameObject);
            p_Instance = null;
        }

        protected virtual void Awake()
        {
            if ( p_Instance == null )
            {
                p_Instance = this as T;
            }

            if ( p_Instance != this )
            {
                // If the real instance is on the same gameObject => just destroy self
                if ( p_Instance.gameObject == this.gameObject )
                {
                    Destroy(this);
                }
                // Else destroy my entire gameObject
                else
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        protected bool IsValid => p_Instance == this;
    }

}
