using UnityEngine;
using UnityEngine.UI;


public class BuildingButtonHandler : MonoBehaviour
{
    [SerializeField] BuildingAsset tileAsset;
    Button button;

    BuildingCreator buildingCreator;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
        buildingCreator = BuildingCreator.GetInstance();
    }

    private void ButtonClicked()
    {
        Debug.Log("button clicked: " + tileAsset.name);
        buildingCreator.ObjectSelected(tileAsset);
    }
}
