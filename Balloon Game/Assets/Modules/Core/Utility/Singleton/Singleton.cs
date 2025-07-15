using UnityEngine;

namespace Modules.Core.Utility.Singleton
{
    public abstract class Singleton <T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void  Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
        }
    }
}