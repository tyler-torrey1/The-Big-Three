using UnityEngine.SceneManagement;
using UnityEngine;


public class HubDoor : Door
{
    public string destination;

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
