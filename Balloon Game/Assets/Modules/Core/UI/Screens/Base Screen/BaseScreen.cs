using System.Collections;
using UnityEngine;

namespace Modules.Core.UI.Screens.Base_Screen
{
    public abstract class BaseScreen : MonoBehaviour
    {
        public virtual IEnumerator Open()
        {
            yield return Exit();
        }

        protected virtual IEnumerator Exit()
        {
            yield return null;
        }
    }
}