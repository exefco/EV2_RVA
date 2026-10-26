using UnityEngine;

public class PumpkinKeyTrigger : MonoBehaviour
{
    [Header("Referencias de la UI")]
    [SerializeField] private GameObject rewardCanvas;

    public void OnPointerClick()
    {
        OnGazeSelect();
    }

    public void OnGazeSelect()
    {
        // 1. Notificar al inventario que se recogió la llave
        if (PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.CollectKey();
        }

        // 2. Activar el Canvas de recompensa si existe
        if (rewardCanvas != null)
        {
            rewardCanvas.SetActive(true);
        }

        // 3. Desactivar la calabaza
        gameObject.SetActive(false);
    }
}