using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TimeSliderController : MonoBehaviour
{
    [SerializeField] private Slider timeSlider;  // Référence au Slider
    [SerializeField] private TextMeshProUGUI timeText;  // Référence au TextMeshPro pour afficher le temps
    [SerializeField] private TextMeshProUGUI powerText;  // Référence pour afficher la puissance
    [SerializeField] private TextMeshProUGUI totalPowerText;  // Nouveau TextMeshPro pour afficher la somme des puissances
    [SerializeField] private TurbineDataContainer turbineDataContainer;  // Référence au TurbineDataContainer
    [SerializeField] private TMP_Dropdown turbineDropdown;  // Dropdown pour choisir l'éolienne

    private int currentTimeIndex = 0;  // L'indice du temps (basé sur le Slider)
    private int currentTurbineIndex = 0;  // Index de l'éolienne active (sélectionnée via le Dropdown)

    void Start()
    {
        if (timeSlider != null)
        {
            // Initialiser le slider pour correspondre aux indices des intervalles de temps
            timeSlider.minValue = 0;
            timeSlider.maxValue = turbineDataContainer.turbines[0].timeIntervals.Length - 1;  // Supposons que tous les turbines ont la même longueur
            timeSlider.onValueChanged.AddListener(OnSliderValueChanged);  // Attacher la méthode pour changer la valeur
        }

        if (turbineDropdown != null)
        {
            // Initialiser le dropdown avec les turbines disponibles
            turbineDropdown.ClearOptions();
            var turbineOptions = new List<string>();
            foreach (var turbine in turbineDataContainer.turbines)
            {
                turbineOptions.Add(turbine.turbineID);  // Ajouter l'ID de chaque turbine dans le Dropdown
            }
            turbineDropdown.AddOptions(turbineOptions);
            turbineDropdown.onValueChanged.AddListener(OnTurbineSelectionChanged);  // Attacher la méthode pour changer la turbine
        }

        if (timeText != null)
        {
            timeText.text = "Time: " + turbineDataContainer.turbines[currentTurbineIndex].timeIntervals[currentTimeIndex];  // Afficher le temps initial
        }

        UpdateDataDisplay();
    }

    // Cette méthode sera appelée chaque fois que l'utilisateur déplace le slider
    private void OnSliderValueChanged(float value)
    {
        currentTimeIndex = Mathf.RoundToInt(value);  // Convertir la valeur du slider en un index entier
        if (timeText != null)
        {
            // Afficher l'intervalle de temps
            timeText.text = "Time: " + turbineDataContainer.turbines[currentTurbineIndex].timeIntervals[currentTimeIndex];
        }

        UpdateDataDisplay();
    }

    // Cette méthode est appelée chaque fois que l'utilisateur sélectionne une nouvelle turbine
    private void OnTurbineSelectionChanged(int index)
    {
        currentTurbineIndex = index;  // Mettre à jour l'indice de la turbine active
        currentTimeIndex = 0;  // Réinitialiser le temps à 0 lorsque l'éolienne change
        timeSlider.value = currentTimeIndex;  // Réinitialiser la valeur du Slider
        if (timeText != null)
        {
            timeText.text = "Time: " + turbineDataContainer.turbines[currentTurbineIndex].timeIntervals[currentTimeIndex];
        }

        UpdateDataDisplay();
    }

    // Mettre à jour les valeurs de puissance et autres données en fonction du temps et de la turbine
    private void UpdateDataDisplay()
    {
        if (powerText != null)
        {
            // Afficher la puissance à cet intervalle de temps pour la turbine active
            float powerAtCurrentTime = turbineDataContainer.turbines[currentTurbineIndex].powers[currentTimeIndex];
            powerText.text = "Power: " + powerAtCurrentTime.ToString("F2") + " kW";
        }

        // Calculer la somme des puissances pour toutes les turbines jusqu'à l'index actuel
        float totalPower = 0f;
        foreach (var turbine in turbineDataContainer.turbines)
        {
            // Ajouter les puissances de chaque turbine jusqu'à l'index actuel
            for (int i = 0; i <= currentTimeIndex; i++)
            {
                totalPower += turbine.powers[i];
            }
        }

        // Afficher la somme des puissances
        if (totalPowerText != null)
        {
            totalPowerText.text = "Total Power: " + totalPower.ToString("F2") + " kWh";
        }
    }
}
