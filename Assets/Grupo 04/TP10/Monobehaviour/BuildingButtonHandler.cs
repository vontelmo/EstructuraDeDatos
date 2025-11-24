using UnityEngine;
using UnityEngine.UI;


public class BuildingButtonHandler : MonoBehaviour
{
    [SerializeField] BuildingAsset tileAsset;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        Debug.Log("button clicked: " + tileAsset.name);
    }
}
