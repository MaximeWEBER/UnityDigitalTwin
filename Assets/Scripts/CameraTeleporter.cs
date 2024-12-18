using UnityEngine;

public class CameraTeleporter : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;  // Assignez la caméra principale dans l'Inspecteur
    [SerializeField] private GameObject canvasCurrent;  // Canvas à masquer
    [SerializeField] private GameObject canvasMain;     // Canvas à afficher

    // Méthode à associer au bouton OnClick()
    public void OnTeleportButtonClick()
    {
        if (mainCamera == null)
        {
            Debug.LogError("La caméra principale n'est pas définie !");
            return;
        }

        // Téléportation
        Vector3 targetPosition = new Vector3(-698f, 87f, 29f);
        Quaternion targetRotation = Quaternion.Euler(0f, -79.139f, 0f);
        
        mainCamera.position = targetPosition;
        mainCamera.rotation = targetRotation;

        Debug.Log("Caméra téléportée avec succès !");

        // Gestion des Canvas
        if (canvasCurrent != null)
        {
            canvasCurrent.SetActive(false);  // Masquer le canvas actuel
        }

        if (canvasMain != null)
        {
            canvasMain.SetActive(true);     // Afficher le canvas principal
        }
    }
}
