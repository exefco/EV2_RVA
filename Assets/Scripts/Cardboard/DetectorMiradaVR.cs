using UnityEngine;
using UnityEngine.InputSystem;
using Google.XR.Cardboard;
 
public class DetectorMiradaVR : MonoBehaviour
{
    [SerializeField] private Camera camaraVR;
    [SerializeField] private LayerMask capaInteractiva;
    [SerializeField] private float distanciaMaxima = 8f;
    private ObjetoInteractivoMirada objetoActual;
 
    private void Update()
    {
        DetectarMirada();
        if (objetoActual != null && ActivacionSolicitada())
            objetoActual.Activar();
    }
 
    private void DetectarMirada()
    {
        Ray rayo = camaraVR.ViewportPointToRay(new Vector3(.5f,.5f,0f));
        ObjetoInteractivoMirada encontrado = null;
 
        if (Physics.Raycast(rayo, out RaycastHit impacto,
            distanciaMaxima, capaInteractiva))
        {
            encontrado = impacto.collider
                .GetComponentInParent<ObjetoInteractivoMirada>();
        }
 
        if (encontrado == objetoActual) return;
        if (objetoActual != null) objetoActual.SalirMirada();
        objetoActual = encontrado;
        if (objetoActual != null) objetoActual.EntrarMirada();
    }
 
    private bool ActivacionSolicitada()
    {
#if UNITY_EDITOR
        return Mouse.current != null &&
               Mouse.current.leftButton.wasPressedThisFrame;
#else
        return Api.IsTriggerPressed;
#endif
    }
 
    private void OnDisable()
    {
        if (objetoActual != null) objetoActual.SalirMirada();
    }
}

