using UnityEngine;

public class WheelClick : MonoBehaviour
{
    public Padlock3D padlock;
    public int wheelIndex;

    void OnMouseDown()
    {
        padlock.RotateWheel(wheelIndex);
    }
}
