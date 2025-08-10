using UnityEngine;

public class WallpaperPeeler : MonoBehaviour
{
    private bool canPeel = false;

    public GameObject wallpaper; 

    public void EnablePeeling()
    {
        canPeel = true;
        PeelWallpaper();
        Debug.Log("Wallpaper peeling enabled!");
    }

    void OnMouseDown()
    {
        if (canPeel)
        {
            PeelWallpaper();
        }
    }

    void PeelWallpaper()
    {
        Debug.Log("Peeling wallpaper now!");
        if (wallpaper != null)
            wallpaper.SetActive(false);
    }
}
