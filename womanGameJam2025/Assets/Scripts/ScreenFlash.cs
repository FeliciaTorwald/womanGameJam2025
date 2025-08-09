using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlash : MonoBehaviour
{
    public Image flashImage;  

    public IEnumerator Flash(float duration)
    {
        flashImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(duration);
        flashImage.color = new Color(1, 1, 1, 0);
    }
}
