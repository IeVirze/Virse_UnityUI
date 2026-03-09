using UnityEngine;

[CreateAssetMenu(fileName = "NewClothingItem", menuName = "Character/Clothing Item")]
public class ClothingItem : ScriptableObject
{
    public string itemName;
    public string category; // "Accessory" "Top" "Pants" "Hair"
    public Mesh mesh;
    public Material[] materials;
    public Sprite previewIcon;
}
