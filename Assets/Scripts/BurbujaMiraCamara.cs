using UnityEngine;

public class BurbujaMiraCamara : MonoBehaviour
{
    [SerializeField] private Transform camara; // Main Camera
    public bool onlyY = true;     // Solo gira en Y (más natural)
    public bool flip = true;      // Invierte 180° si el texto queda espejo
    public bool keepSize = false;  // Mantener tamaño aparente
    public float designDistance = 2f; // Distancia de diseño (m)

    Vector3 baseScale;

    void Start()
    {
        if (!camara && Camera.main) camara = Camera.main.transform;
        baseScale = transform.localScale;
    }

    void LateUpdate()
    {
        if (!camara) return;

        Vector3 toCam = camara.position - transform.position;
        if (onlyY) toCam.y = 0f;

        transform.rotation = Quaternion.LookRotation(toCam.normalized, Vector3.up);
        if (flip) transform.Rotate(0f, 180f, 0f);

        if (keepSize)
        {
            float d = Mathf.Max(0.01f, Vector3.Distance(transform.position, camara.position));
            float s = d / Mathf.Max(0.01f, designDistance);
            transform.localScale = baseScale * s;
        }
    }
}
