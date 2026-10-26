using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine;
using UnityEngine.InputSystem;

public class EditorMouseLook : MonoBehaviour
{
    [SerializeField] private Transform camara;
    [SerializeField] private float sensibilidad = 0.12f;
    [SerializeField] private float limiteVertical = 75f;

    private float vertical;

    private void Start()
    {
        if (camara != null)
        {
            float xRot = camara.localEulerAngles.x;
            if (xRot > 180f) xRot -= 360f;
            vertical = xRot;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Mouse.current == null || camara == null) return;
        if (!Mouse.current.rightButton.isPressed) return;

        Vector2 delta = Mouse.current.delta.ReadValue();

        float deltaX = delta.x * sensibilidad;
        float deltaY = delta.y * sensibilidad;

        // 1. Giro horizontal (Y): Rota el objeto alrededor de la posición exacta de la cámara
        transform.RotateAround(camara.position, Vector3.up, deltaX);

        // 2. Giro vertical (X): Se aplica de forma aislada a la cámara con Clamp
        float previoVertical = vertical;
        vertical = Mathf.Clamp(vertical - deltaY, -limiteVertical, limiteVertical);
        float cambioVertical = vertical - previoVertical;

        camara.Rotate(Vector3.right, cambioVertical, Space.Self);
#endif
    }
}

