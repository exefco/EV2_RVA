using UnityEngine;

public class BookTrigger : MonoBehaviour
{
    [Header("Referencias de UI (Opcional)")]
    [SerializeField] private GameObject rewardCanvas;

    public void OnPointerClick()
    {
        Debug.Log("[BookTrigger] OnPointerClick disaparado en el Libro.");
        OnGazeSelect();
    }

    public void OnGazeSelect()
    {
        Debug.Log("[BookTrigger] OnGazeSelect disparado en el Libro.");

        // 1. Notificar al PlayerInventory
        if (PlayerInventory.Instance != null)
        {
            Debug.Log("[BookTrigger] Llamando a PlayerInventory.Instance.CollectBook()...");
            PlayerInventory.Instance.CollectBook();
        }
        else
        {
            Debug.LogError("[BookTrigger] PlayerInventory.Instance es NULL. Verifica que el GameManager esté en la escena.");
        }

        // 2. Activar Canvas de recompensa opcional
        if (rewardCanvas != null)
        {
            rewardCanvas.SetActive(true);
        }

        // 3. Desactivar el libro
        gameObject.SetActive(false);
    }
}
