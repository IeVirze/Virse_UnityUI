using UnityEngine;

[CreateAssetMenu(fileName = "HeroLegends", menuName = "Character/Hero Legends")]
public class HeroLegends : ScriptableObject
{
    [TextArea(5, 10)]
    public string maleLegend;

    [TextArea(5, 10)]
    public string femaleLegend;
}
