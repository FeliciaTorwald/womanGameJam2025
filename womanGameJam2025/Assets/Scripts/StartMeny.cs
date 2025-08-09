using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMeny : MonoBehaviour
{
public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
public void Credits()
{
        SceneManager.LoadScene("Credits");
}

    public void StartMenyYay()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
