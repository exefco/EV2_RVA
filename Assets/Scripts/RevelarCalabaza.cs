using UnityEngine;
using UnityEngine.EventSystems;

public class RevelarCalabaza : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject calabaza;

    // 1. Método sin parámetros que llama la herramienta por reflexión (ReticleDwellBridge)
    public void OnPointerClick()
    {
        RevelarContenido();
    }

    // 2. Método de la interfaz IPointerClickHandler para EventSystems estándar
    public void OnPointerClick(PointerEventData eventData)
    {
        RevelarContenido();
    }

    // 3. Método público ejecutable desde el inspector o script
    public void RevelarContenido()
    {
        if (calabaza != null)
        {
            calabaza.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
