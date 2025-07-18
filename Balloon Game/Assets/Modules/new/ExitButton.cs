using UnityEngine;

public class ExitButton : BaseButton
{
    protected override void OnClickAction()
    {
        Application.Quit();
    }
}