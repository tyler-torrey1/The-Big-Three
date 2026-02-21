using UnityEngine.SceneManagement;
using UnityEngine;


public class HubDoor : Door
{
    public Scene destination;

    protected override void HandleExitTriggered()
    {
        if (destination == null)
        {
            Debug.LogError(name + ": null destination");
            return;
        }
        GlobalManager.ChangeSceneTo(destination, direction);
    }
}
