using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance {get; private set;}

    [Header("UI References")]
    [SerializeField] private TMP_Text dialogueText;

    [Header("Intro Dialogue")]
    [TextArea(2,5)]
    [SerializeField] private string[] introLines;

    [Header("Type Settings")]
    [SerializeField] private float typingSpeed = 0.04f;

    [SerializeField] private GameObject magicCircle;

    private string[] currentLines;
    private int currentLineIndex;
    private bool isTyping;
    private bool skipTyping;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TriggerIntroDialogue()
    {
        if(introLines != null && introLines.Length > 0)
        {
            StartDialogue(introLines);
        }
    }

    public void StartDialogue(string[] lines)
    {
        if(lines == null || lines.Length == 0)
        {
            return;
        }
        currentLines = lines;
        currentLineIndex = 0;
        DisplayCurrentLine();
    }

    public void HandleContinueInput()
    {
        if(isTyping)
        {
            skipTyping = true;
        }
        else
        {
            AdvanceLine();
        }
    }

    private void DisplayCurrentLine()
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeLine(currentLines[currentLineIndex]));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        skipTyping = false;
        dialogueText.text = string.Empty;

        foreach(char c in line)
        {
            if(skipTyping)
            {
                dialogueText.text = line;
                break;
            }
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void AdvanceLine()
    {
        currentLineIndex++;
        if(currentLineIndex < currentLines.Length)
        {
            DisplayCurrentLine();
            if(currentLineIndex == 3)
            {
                magicCircle.SetActive(true);
            }
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
 
        currentLines = null;
        currentLineIndex = 0;
    }

}
