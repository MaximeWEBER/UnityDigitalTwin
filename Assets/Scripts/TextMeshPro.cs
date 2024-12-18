using UnityEngine;
using TMPro;  // Assurez-vous d'utiliser le namespace TMP

public class DisplayTurbineData : MonoBehaviour
{
    [SerializeField] private TurbineDataContainer turbineDataContainer; // Référence au ScriptableObject contenant les données
    [SerializeField] private TextMeshProUGUI turbineText;  // Référence au TextMeshPro pour afficher les données

    void Start()
    {
        // Vérifier si le TextMeshPro est bien assigné
        if (turbineText == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned!");
            return;
        }

        // Assurez-vous que turbineDataContainer n'est pas null et contient des données
        if (turbineDataContainer != null && turbineDataContainer.turbines != null)
        {
            // Construire une chaîne de texte pour afficher les données
            string displayText = "Turbine Data:\n";
            foreach (var turbine in turbineDataContainer.turbines)
            {
                displayText += "Turbine ID: " + turbine.turbineID + "\n"; // Afficher l'ID de la turbine
                displayText += "Power Data: \n";
                
                // Afficher les données de puissance pour chaque turbine
                for (int i = 0; i < turbine.powers.Length; i++)
                {
                    displayText += $"Power {i}: {turbine.powers[i]}\n";
                }
                displayText += "\n";  // Ajouter une ligne vide pour séparer les turbines
            }

            // Mettre à jour le texte de TextMeshPro
            turbineText.text = displayText;
        }
        else
        {
            Debug.LogError("Turbine data is missing!");
        }
    }
}
