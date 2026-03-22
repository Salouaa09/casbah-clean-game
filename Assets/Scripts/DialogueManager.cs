using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ============================================================
// DialogueManager.cs - Casbah Clean
// Gère la narration du Niveau 0 (Histoire / Introduction)
// ============================================================

public class DialogueManager : MonoBehaviour
{
    // ── Références UI ──────────────────────────────────────
    [Header("UI Elements")]
    public GameObject   dialoguePanel;      // Le panneau de dialogue
    public TextMeshProUGUI nameText;        // Nom du personnage
    public TextMeshProUGUI dialogueText;    // Texte du dialogue
    public Button       nextButton;         // Bouton "Suivant"
    public Button       skipButton;         // Bouton "Passer"

    // ── Paramètres d'affichage ─────────────────────────────
    [Header("Settings")]
    public float typingSpeed = 0.04f;       // Vitesse d'écriture lettre par lettre

    // ── Données internes ───────────────────────────────────
    private Queue<DialogueLine> lines = new Queue<DialogueLine>();
    private bool isTyping = false;
    private string currentFullText = "";

    // ── Événement de fin de dialogue ──────────────────────
    public delegate void DialogueFinished();
    public event DialogueFinished OnDialogueFinished;

    // ── Singleton ──────────────────────────────────────────
    public static DialogueManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        dialoguePanel.SetActive(false);

        nextButton.onClick.AddListener(OnNextClicked);
        skipButton.onClick.AddListener(SkipDialogue);
    }

    // ============================================================
    // LANCER UN DIALOGUE
    // Appelle cette méthode depuis n'importe quel script
    // ============================================================
    public void StartDialogue(List<DialogueLine> dialogue)
    {
        dialoguePanel.SetActive(true);
        lines.Clear();

        foreach (DialogueLine line in dialogue)
            lines.Enqueue(line);

        DisplayNextLine();
    }

    // ── Affiche la ligne suivante ──────────────────────────
    public void DisplayNextLine()
    {
        if (isTyping)
        {
            // Si on est en train d'écrire → affiche tout le texte d'un coup
            StopAllCoroutines();
            dialogueText.text = currentFullText;
            isTyping = false;
            return;
        }

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = lines.Dequeue();
        nameText.text = line.speaker;
        StartCoroutine(TypeLine(line.text));
    }

    // ── Effet machine à écrire ─────────────────────────────
    IEnumerator TypeLine(string text)
    {
        isTyping = true;
        currentFullText = text;
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // ── Bouton Suivant cliqué ──────────────────────────────
    void OnNextClicked()
    {
        DisplayNextLine();
    }

    // ── Passer tout le dialogue ────────────────────────────
    void SkipDialogue()
    {
        StopAllCoroutines();
        lines.Clear();
        EndDialogue();
    }

    // ── Fin du dialogue ────────────────────────────────────
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isTyping = false;
        OnDialogueFinished?.Invoke();
    }
}

// ============================================================
// STRUCTURE D'UNE LIGNE DE DIALOGUE
// ============================================================
[System.Serializable]
public class DialogueLine
{
    public string speaker;  // Nom affiché (ex: "Mokrane", "Narrateur")
    public string text;     // Texte du dialogue

    public DialogueLine(string speaker, string text)
    {
        this.speaker = speaker;
        this.text    = text;
    }
}