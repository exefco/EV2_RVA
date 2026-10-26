using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    [Header("UI del Jugador (HUD)")]
    [SerializeField] private GameObject keyUICanvas;  // Canvas de la Llave
    [SerializeField] private GameObject bookUICanvas; // Canvas del Libro

    private bool hasKey = false;
    private bool hasBook = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Validación de referencias en el Inspector
        if (keyUICanvas == bookUICanvas && keyUICanvas != null)
        {
            Debug.LogError("[PlayerInventory] ALERTA: 'keyUICanvas' y 'bookUICanvas' tienen asignado EL MISMO GameObject en el Inspector.");
        }
    }

    // --- Lógica de la Llave ---
    public void CollectKey()
    {
        hasKey = true;
        Debug.Log("[PlayerInventory] Executing CollectKey(). HasKey set to TRUE.");

        // Desactivar el Canvas del libro si existiera
        if (bookUICanvas != null)
        {
            bookUICanvas.SetActive(false);
            Debug.Log($"[PlayerInventory] bookUICanvas ({bookUICanvas.name}) set to Active: false");
        }

        // Activar el Canvas de la llave
        if (keyUICanvas != null)
        {
            keyUICanvas.SetActive(true);
            Debug.Log($"[PlayerInventory] keyUICanvas ({keyUICanvas.name}) set to Active: true");
        }
        else
        {
            Debug.LogWarning("[PlayerInventory] keyUICanvas es NULL en el Inspector.");
        }
    }

    public bool HasKey()
    {
        return hasKey;
    }

    // --- Lógica del Libro ---
    public void CollectBook()
    {
        hasBook = true;
        Debug.Log("[PlayerInventory] Executing CollectBook(). HasBook set to TRUE.");

        // Desactivar el Canvas de la llave
        if (keyUICanvas != null)
        {
            keyUICanvas.SetActive(false);
            Debug.Log($"[PlayerInventory] keyUICanvas ({keyUICanvas.name}) set to Active: false");
        }
        else
        {
            Debug.LogWarning("[PlayerInventory] keyUICanvas es NULL al intentar desactivarlo.");
        }

        // Activar el Canvas del libro
        if (bookUICanvas != null)
        {
            bookUICanvas.SetActive(true);
            Debug.Log($"[PlayerInventory] bookUICanvas ({bookUICanvas.name}) set to Active: true");
        }
        else
        {
            Debug.LogWarning("[PlayerInventory] bookUICanvas es NULL en el Inspector.");
        }
    }

    public bool HasBook()
    {
        return hasBook;
    }
}