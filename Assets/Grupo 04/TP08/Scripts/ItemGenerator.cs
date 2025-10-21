using UnityEngine;
using UnityEditor;

public class ItemGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate 40 Items")]
    public static void GenerateItems()
    {
        string folderPath = "Assets/Resources/Items";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Items");
        }

        for (int i = 1; i <= 40; i++)
        {
            Item newItem = ScriptableObject.CreateInstance<Item>();

            // Asignar valores
            newItem.objName = "Item_" + i;
            newItem.price = (int)Random.Range(10f, 500f);

            // Guardar como asset
            string assetPath = $"{folderPath}/Item_{i}.asset";
            AssetDatabase.CreateAsset(newItem, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("✅ 40 Items generados en " + folderPath);
    }
}
