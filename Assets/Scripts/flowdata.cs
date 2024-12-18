using UnityEngine;
using ChartAndGraph;
using System.Linq;

public class TurbineDataAnalyzer : MonoBehaviour
{
    [SerializeField] private TurbineDataContainer turbineDataContainer;
    [SerializeField] private BarChart turbinePowerChart;
    [SerializeField] private Material barMaterial;

    private TurbineData GetTurbineData(string turbineID)
    {
        return turbineDataContainer.turbines
            .FirstOrDefault(t => t.turbineID == turbineID);
    }

    public void DisplayPowerChart(string turbineID)
    {
        TurbineData turbineData = GetTurbineData(turbineID);

        if (turbineData == null)
        {
            Debug.LogError($"Turbine {turbineID} not found!");
            return;
        }

        // Nettoyage du graphique
        turbinePowerChart.DataSource.ClearCategories(); 
        turbinePowerChart.DataSource.AddCategory("Puissance", barMaterial); 

        // Pré-définir les groupes (intervalles de temps)
        foreach (string time in turbineData.timeIntervals)
        {
            turbinePowerChart.DataSource.AddGroup(time);
            Debug.Log($"Pré-définition du groupe : {time}");
        }

        // Ajouter les données
        for (int i = 0; i < turbineData.timeIntervals.Length; i++)
        {
            string time = turbineData.timeIntervals[i];
            float power = turbineData.powers[i];
            
            // Ajouter les valeurs au graphique
            turbinePowerChart.DataSource.SetValue("Puissance", time, power);
            Debug.Log($"Donnée ajoutée pour {turbineID} à {time} : {power}");
        }
    }

    void Start()
    {
        string turbineId = "T98";
        DisplayPowerChart(turbineId);
    }
}
