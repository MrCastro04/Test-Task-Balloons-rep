using System.Collections;
using Modules.Core.Systems.Action_System.Scripts;
using Modules.Core.UI.Screens.Base_Screen;
using UnityEngine;

public class GameplayScreen : BaseScreen
{
    public override IEnumerator Open()
    {
        ActionSystem.Instance.AddReaction(new StartLevelGA());
        
        yield return null;
    }
}