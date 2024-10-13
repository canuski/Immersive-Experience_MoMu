using UnityEngine;

public class ToggleGameObjects : MonoBehaviour
{
    // Drag and drop the GameObjects you want to toggle here in the Inspector
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;

    // Method to toggle the GameObjects
    public void ToggleObjects()
    {
        // Activate specified GameObjects
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        // Deactivate specified GameObjects
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
