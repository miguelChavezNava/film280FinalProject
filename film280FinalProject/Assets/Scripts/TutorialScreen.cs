using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TutorialScreen : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMP_Text promptText;

    [Header("Text Settings")]
    [SerializeField] private string promptMessage = "Press E to continue...";

    private bool tutorialDismissed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
        }
        if(promptText != null)
        {
            promptText.text = promptMessage;
        }
    }

    public void OnContinue(InputValue value)
    {
        if(!value.isPressed) return;

        if(!tutorialDismissed)
        {
            tutorialDismissed = true;
            tutorialPanel.SetActive(false);
            if(DialogueManager.Instance != null)
            {
                DialogueManager.Instance.TriggerIntroDialogue();
            }
            else
            {
                Debug.LogWarning("TutorialScreen: No DialogueManager found in scene");
            }
        }
        else
        {
            
            if(DialogueManager.Instance != null)
            {
                DialogueManager.Instance.HandleContinueInput();
            }
        }
    }
}
