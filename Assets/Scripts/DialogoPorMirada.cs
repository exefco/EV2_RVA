using TMPro;
using UnityEngine;

public class DialogoPorMirada : MonoBehaviour
{
    [SerializeField] private GameObject burbuja;
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField, TextArea(2, 4)] private string mensaje = "Aquí llegan los pedidos de la granja";
    
    [Header("Configuración de Mirada")]
    [SerializeField] private float tiempoParaMostrar = 1.0f; // Segundos que debe mirar para activar

    private bool mirando = false;
    private float contadorTiempo = 0f;

    private void Awake()
    {
        if (burbuja == null || texto == null)
        {
            Debug.LogError("Asigna Burbuja y Texto en el Inspector.", this);
            enabled = false;
            return;
        }
        texto.text = mensaje;
        burbuja.SetActive(false);
    }

    private void Update()
    {
        // Si el usuario está mirando el objeto y la burbuja aún no se ha activado
        if (mirando && !burbuja.activeSelf)
        {
            contadorTiempo += Time.deltaTime;
            
            // Cuando pasa el tiempo especificado, activa la burbuja
            if (contadorTiempo >= tiempoParaMostrar)
            {
                burbuja.SetActive(true);
            }
        }
    }

    public void OnPointerEnter()
    {
        mirando = true;
        contadorTiempo = 0f; // Reinicia el contador al empezar a mirar
    }

    public void OnPointerExit()
    {
        mirando = false;
        contadorTiempo = 0f;
        if (burbuja != null)
        {
            burbuja.SetActive(false);
        }
    }

    // Mantenemos OnPointerClick opcional por si también usas un botón o tap en pantalla
    public void OnPointerClick()
    {
        if (burbuja != null)
        {
            burbuja.SetActive(true);
        }
    }
}

