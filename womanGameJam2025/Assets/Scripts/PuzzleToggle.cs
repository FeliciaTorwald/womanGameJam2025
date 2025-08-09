using UnityEngine;

public class PuzzleToggle : MonoBehaviour
{
    public GameObject puzzleObject;  

    private bool isPuzzleActive = false;

    void OnMouseDown()
    {
        TogglePuzzle();
    }

    void TogglePuzzle()
    {
        isPuzzleActive = !isPuzzleActive;
        puzzleObject.SetActive(isPuzzleActive);


        // Cursor.lockState = isPuzzleActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
