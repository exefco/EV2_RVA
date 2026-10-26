using System.Collections;
using UnityEngine;
using TMPro;

public class HouseDoorTeleport : MonoBehaviour
{
    [Header("Referencias de Teletransporte")]
    [SerializeField] private Transform playerTransform;       // VR Rig / Cámara Jugador
    [SerializeField] private Transform destinationTarget;     // Punto de destino dentro de la casa[cite: 3]
    [SerializeField] private ScreenFadeVR fadeCanvas;         // Transición ScreenFadeVR[cite: 2, 3]

    [Header("UI de la Casa (World Space)")]
    [SerializeField] private GameObject doorMessageCanvas;   // Canvas de la Casa con el mensaje de bloqueo
    [SerializeField] private TextMeshProUGUI messageText;     // Texto dentro del Canvas de la casa[cite: 3]
    [SerializeField] private float messageDuration = 2.5f;   // Duración de visibilidad[cite: 3]

    private Coroutine messageCoroutine;
    private bool isTeleporting = false;

    public void OnPointerClick()
    {
        OnInteractWithDoor();
    }

    public void OnGazeSelect()
    {
        OnInteractWithDoor();
    }

    public void OnInteractWithDoor()
    {
        if (isTeleporting) return;

        // Validar si el Canvas de la llave en el jugador está activo
        bool playerHasKey = PlayerInventory.Instance != null && PlayerInventory.Instance.HasKey();

        if (playerHasKey)
        {
            // Ocultar el Canvas de la casa si estaba activo por un intento anterior
            if (doorMessageCanvas != null)
            {
                doorMessageCanvas.SetActive(false);
            }

            StartCoroutine(TeleportSequence());
        }
        else
        {
            // Activar el Canvas de la casa para mostrar el mensaje
            ShowAccessDeniedMessage("Aún no puedes entrar a la cabaña");
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
        // 1. Asignar el texto si existe la referencia
        if (messageText != null)
        {
            messageText.text = message;
        }

        // 2. Activar el Canvas de la Casa
        if (doorMessageCanvas != null)
        {
            doorMessageCanvas.SetActive(true);
        }

        // 3. Esperar el tiempo configurado
        yield return new WaitForSeconds(messageDuration);

        // 4. Desactivar el Canvas de la Casa
        if (doorMessageCanvas != null)
        {
            doorMessageCanvas.SetActive(false);
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

        // 2. Reubicar al jugador en el destino
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
