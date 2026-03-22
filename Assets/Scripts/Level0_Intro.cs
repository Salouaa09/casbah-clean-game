using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ============================================================
// Level0_Intro.cs - Casbah Clean
// Gère toute la cinématique d'introduction (Niveau 0)
// Histoire de Mokrane à l'USTHB
// ============================================================

public class Level0_Intro : MonoBehaviour
{
    [Header("Scène suivante")]
    public string nextSceneName = "Level1_Tutorial";

    void Start()
    {
        // Lance automatiquement le dialogue au démarrage de la scène
        StartIntroDialogue();

        // S'abonne à l'événement de fin de dialogue
        DialogueManager.Instance.OnDialogueFinished += OnIntroFinished;
    }

    // ============================================================
    // TOUS LES DIALOGUES DU NIVEAU 0
    // ============================================================
    void StartIntroDialogue()
    {
        List<DialogueLine> intro = new List<DialogueLine>
        {
            // ── Scène 1 : USTHB ─────────────────────────────
            new DialogueLine("Narrateur",
                "Alger, USTHB. Mokrane, étudiant en informatique, " +
                "rentre chez lui après une longue journée de cours."),

            new DialogueLine("Mokrane",
                "Encore une journée chargée... " +
                "Mais quelque chose me pèse depuis ce matin."),

            new DialogueLine("Narrateur",
                "En traversant les ruelles de la Casbah, " +
                "il observe autour de lui avec tristesse."),

            // ── Scène 2 : La Casbah polluée ─────────────────
            new DialogueLine("Mokrane",
                "Regarde cet endroit... Des déchets partout. " +
                "Des bouteilles, des sacs en plastique, des débris..."),

            new DialogueLine("Mokrane",
                "La Casbah est classée patrimoine mondial de l'UNESCO. " +
                "Comment peut-on laisser un tel trésor dans cet état ?"),

            new DialogueLine("Narrateur",
                "Les ruelles historiques aux murs blancs sont recouvertes " +
                "d'ordures. Les escaliers en pierre sont jonchés de déchets."),

            // ── Scène 3 : Prise de conscience ───────────────
            new DialogueLine("Mokrane",
                "Ces rues, ces maisons... " +
                "C'est là où on vit, où on étudie, où on travaille."),

            new DialogueLine("Mokrane",
                "La pollution détruit ces lieux et nuit à notre santé. " +
                "Notre patrimoine disparaît sous les ordures."),

            new DialogueLine("Mokrane",
                "Je ne peux plus rester passif. " +
                "Je dois agir. Maintenant."),

            // ── Scène 4 : La décision ────────────────────────
            new DialogueLine("Narrateur",
                "Mokrane décide ce jour-là de s'engager concrètement " +
                "pour nettoyer et préserver la Casbah d'Alger."),

            new DialogueLine("Mokrane",
                "Je vais commencer par les maisons autour de moi. " +
                "Chaque geste compte. Chaque déchet ramassé fait la différence."),

            // ── Scène 5 : La rencontre (fin du niveau 0) ────
            new DialogueLine("Narrateur",
                "C'est alors qu'une vieille dame l'interpelle " +
                "depuis le seuil d'une maison délabrée."),

            new DialogueLine("La femme de la Casbah",
                "Toi, le jeune ! Je t'ai vu regarder nos rues avec tant de peine. " +
                "Tu veux vraiment changer les choses ?"),

            new DialogueLine("Mokrane",
                "Oui, Khalti. Je veux agir. " +
                "Mais je ne sais pas par où commencer."),

            new DialogueLine("La femme de la Casbah",
                "Commence par ma maison. Elle est dans un état terrible. " +
                "Si tu la nettoies et tries correctement les déchets..."),

            new DialogueLine("La femme de la Casbah",
                "...je t'aiderai à financer la reconstruction " +
                "de ta propre maison dans la Casbah. C'est un accord ?"),

            new DialogueLine("Mokrane",
                "C'est un accord, Khalti ! " +
                "Ensemble, on va rendre la Casbah plus propre et plus belle."),

            // ── Message final ────────────────────────────────
            new DialogueLine("Narrateur",
                "Le voyage de Mokrane commence ici. " +
                "Chaque déchet ramassé est un pas vers un avenir meilleur."),
        };

        DialogueManager.Instance.StartDialogue(intro);
    }

    // ============================================================
    // QUAND LE DIALOGUE EST TERMINÉ → Passer au niveau 1
    // ============================================================
    void OnIntroFinished()
    {
        Debug.Log("Niveau 0 terminé ! Chargement du Level 1...");
        SceneManager.LoadScene(nextSceneName);
    }

    void OnDestroy()
    {
        // Nettoyage de l'événement
        if (DialogueManager.Instance != null)
            DialogueManager.Instance.OnDialogueFinished -= OnIntroFinished;
    }
}