using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThreeDigitCycler : MonoBehaviour
{
    public Button[] digitButtons;       
    public TextMeshProUGUI[] digitTexts; 
    public string correctCode = "735";

    public ParticleSystem confettiEffect;

    void Start()
    {
        for (int i = 0; i < digitButtons.Length; i++)
        {
            int index = i;  
            digitButtons[i].onClick.AddListener(() => CycleDigit(index));
            digitTexts[i].text = "0";  
        }
    }

    void CycleDigit(int buttonIndex)
    {
        int currentDigit = int.Parse(digitTexts[buttonIndex].text);
        currentDigit = (currentDigit + 1) % 10;
        digitTexts[buttonIndex].text = currentDigit.ToString();

        CheckCode();
    }

    void CheckCode()
    {
        string enteredCode = "";
        foreach (var txt in digitTexts)
        {
            enteredCode += txt.text;
        }

        if (enteredCode == correctCode)
        {
            Debug.Log("Code correct! Puzzle solved.");
            if (confettiEffect != null)
                confettiEffect.Play();

        }
    }
}
