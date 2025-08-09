using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public InteractionManager interactionManager;
    private bool hasInteracted = false;

    void OnMouseDown()
    {
        if (!hasInteracted)
        {
            hasInteracted = true;
            interactionManager.RegisterInteraction();

            // Your interaction logic here (e.g., pick up item, show clue...)
        }
    }
}
