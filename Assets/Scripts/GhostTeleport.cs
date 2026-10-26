using System.Collections;
using UnityEngine;
using TMPro;

public class GhostTeleport : MonoBehaviour
{
    [Header("Referencias de Teletransporte")]
    [SerializeField] private Transform playerTransform;       // VR Rig / Cámara Jugador
    [SerializeField] private Transform destinationTarget;     // Destino al ser teletransportado por el fantasma[cite: 3]
    [SerializeField] private ScreenFadeVR fadeCanvas;         // Referencia a ScreenFadeVR

    [Header("UI del Fantasma (World Space)")]
    [SerializeField] private GameObject ghostMessageCanvas;   // Canvas propio del Fantasma
    [SerializeField] private TextMeshProUGUI messageText;     // Texto en el Canvas del Fantasma[cite: 3]
    [SerializeField] private float messageDuration = 2.5f;   // Duración del mensaje[cite: 3]

    private Coroutine messageCoroutine;
    private bool isTeleporting = false;

    public void OnPointerClick()
    {
        OnInteractWithGhost();
    }

    public void OnGazeSelect()
    {
        OnInteractWithGhost();
    }

    public void OnInteractWithGhost()
    {
        if (isTeleporting) return;

        // Validar si el jugador posee el libro consultando al PlayerInventory
        bool playerHasBook = PlayerInventory.Instance != null && PlayerInventory.Instance.HasBook();

        if (playerHasBook)
        {
            // Ocultar mensaje del fantasma si estaba activo
            if (ghostMessageCanvas != null)
            {
                ghostMessageCanvas.SetActive(false);
            }

            StartCoroutine(TeleportSequence());
        }
        else
        {
            // Activar el Canvas del fantasma indicando que falta el libro
            ShowAccessDeniedMessage("Bienvenido al refugio, necesito que encuentres y me devuelvas mi libro perdido, o no te dejaré escapar...");
        }
    }

    private void ShowAccessDeniedMessage(string message)
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        messageCoroutine = StartCoroutine(ShowMessageRoutine(message));
    }

    private IEnumerator ShowMessageRoutine(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }

        if (ghostMessageCanvas != null)
        {
            ghostMessageCanvas.SetActive(true);
        }

        yield return new WaitForSeconds(messageDuration);

        if (ghostMessageCanvas != null)
        {
            ghostMessageCanvas.SetActive(false);
        }
    }

    private IEnumerator TeleportSequence()
    {
        isTeleporting = true;

        // 1. Pantalla a negro
        if (fadeCanvas != null)
        {
            yield return fadeCanvas.CerrarYEsperar();
        }

        // 2. Reubicar al jugador
        if (playerTransform != null && destinationTarget != null)
        {
            playerTransform.position = destinationTarget.position;
            playerTransform.rotation = destinationTarget.rotation;
        }

        yield return new WaitForSeconds(0.1f);

        // 3. Volver a abrir pantalla
        if (fadeCanvas != null)
        {
            yield return fadeCanvas.AbrirYEsperar();
        }

        isTeleporting = false;
    }
}
