using UnityEngine;
using ChartAndGraph;
using System.Collections;
using System;

public class GraphChartFeed : MonoBehaviour
{
    [SerializeField] private TurbineDataContainer turbineDataContainer; // Référence au ScriptableObject contenant les données

    void Start()
    {
        GraphChartBase graph = GetComponent<GraphChartBase>();
        if (graph != null)
        {
            graph.Scrollable = false;
            graph.HorizontalValueToStringMap[0.0] = "Zero"; // Exemple de personnalisation des étiquettes d'axe

            // Commencer à configurer les données
            graph.DataSource.StartBatch();
            graph.DataSource.ClearCategory("Turbines");
            graph.DataSource.ClearAndMakeBezierCurve("T98"); // Optionnel : Efface une courbe spécifique si nécessaire

            // Parcourez les turbines dans le TurbineDataContainer
            if (turbineDataContainer != null && turbineDataContainer.turbines != null)
            {
                // Parcourez les turbines pour ajouter leurs données dans le graphique
                for (int i = 0; i < turbineDataContainer.turbines.Length; i++)
                {
                    var turbine = turbineDataContainer.turbines[i];

                    // Utilisez directement l'ID de la turbine pour l'étiqueter (sans ajouter "Turbine")
                    string categoryName = turbine.turbineID; // Utilisation directe de l'ID

                    // Ajouter les données de puissance à partir de chaque turbine
                    for (int j = 0; j < turbine.powers.Length; j++)
                    {
                        // Ajouter les points de données pour chaque turbine en utilisant son ID comme catégorie
                        graph.DataSource.AddPointToCategory(categoryName, j * 10, turbine.powers[j]);
                    }
                }
            }

            graph.DataSource.EndBatch();
        }
    }

    // Optionnel : Ajoutez une coroutine pour vider les données après un certain temps
    IEnumerator ClearAll()
    {
        yield return new WaitForSeconds(5f);
        GraphChartBase graph = GetComponent<GraphChartBase>();

        if (graph != null)
        {
            graph.DataSource.Clear();
        }
    }
}
