using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // Para interfaces de EventSystem

public class AccionCuboVR : MonoBehaviour, IPointerClickHandler
{
    public enum TipoAccion
    {
        Victoria,
        Reiniciar,
        Salir
    }

    [Header("Configuración del Cubo")]
    public TipoAccion accion;

    [Header("UI del Cubo (Siempre Visible)")]
    public GameObject canvasMensajeUI;

    private void Start()
    {
        // Forzamos a que el Canvas esté activo en todo momento
        if (canvasMensajeUI != null)
        {
            canvasMensajeUI.SetActive(true);
        }
    }

    // 1. Llamada que hace ReticleDwellBridge / Cardboard (SIN parámetros)
    public void OnPointerClick()
    {
        EjecutarAccion();
    }

    // 2. Llamada que hace EventSystem de Unity (CON parámetro)
    public void OnPointerClick(PointerEventData eventData)
    {
        EjecutarAccion();
    }

    public void EjecutarAccion()
    {
        switch (accion)
        {
            case TipoAccion.Reiniciar:
                ReiniciarJuego();
                break;

            case TipoAccion.Salir:
                SalirDelJuego();
                break;

            case TipoAccion.Victoria:
                Debug.Log("[VR] Miraste el cubo de Victoria.");
                break;
        }
    }

    private void ReiniciarJuego()
    {
        Debug.Log("[VR] Reiniciando la escena...");
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
    }

    private void SalirDelJuego()
    {
        Debug.Log("[VR] Saliendo del juego...");

        #if UNITY_EDITOR
            // Detiene la reproducción de la escena en el Editor de Unity
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Cierra la aplicación instalada en el dispositivo móvil
            Application.Quit();
        #endif
    }
}
