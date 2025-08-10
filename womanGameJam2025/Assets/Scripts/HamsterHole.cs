using UnityEngine;
using UnityEngine.SceneManagement;

public class HamsterHole : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Hamster Hole");
    }
}
