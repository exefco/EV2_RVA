using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeleportVR : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ScreenFadeVR screenFade;
    [SerializeField] private Transform targetDestination;
    [SerializeField] private Transform playerTransform;

    public void OnPointerClick()
    {
        StartTeleport();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartTeleport();
    }

    public void StartTeleport()
    {
        StartCoroutine(TeleportSequence());
    }

    private IEnumerator TeleportSequence()
    {
        if (screenFade != null)
        {
            // Asegurar que el GameObject del ScreenFadeVR esté activo
            screenFade.gameObject.SetActive(true);

            // Transición a negro y esperar
            yield return screenFade.CerrarYEsperar();
        }

        // Mover jugador
        if (playerTransform != null && targetDestination != null)
        {
            playerTransform.position = targetDestination.position;
        }

        if (screenFade != null)
        {
            // Transición a transparente y esperar
            yield return screenFade.AbrirYEsperar();
        }
    }
}


