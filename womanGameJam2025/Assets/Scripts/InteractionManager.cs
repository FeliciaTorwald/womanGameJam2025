using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public int totalObjects = 3;      // How many objects you need to interact with
    public int interactedCount = 0;  // How many you've interacted with so far

    public WallpaperPeeler wallpaperPeeler; // Reference to your wallpaper script

    // Call this method whenever an object is interacted with
    public void RegisterInteraction()
    {
        interactedCount++;
        Debug.Log($"Interacted with {interactedCount}/{totalObjects}");

        if (interactedCount >= totalObjects)
        {
            Debug.Log("All objects interacted! You can peel the wallpaper now.");
            if (wallpaperPeeler != null)
                wallpaperPeeler.EnablePeeling();
        }
    }
}
