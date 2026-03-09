using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ReviewScreenManager : MonoBehaviour
{
    [Header("Characters")]
    public GameObject maleCharacter;
    public GameObject femaleCharacter;

    [Header("Clothing Slots - Male")]
    public SkinnedMeshRenderer maleAccessorySlot;
    public SkinnedMeshRenderer maleTopSlot;
    public SkinnedMeshRenderer malePantsSlot;
    public SkinnedMeshRenderer maleHairSlot;

    [Header("Clothing Slots - Female")]
    public SkinnedMeshRenderer femaleAccessorySlot;
    public SkinnedMeshRenderer femaleTopSlot;
    public SkinnedMeshRenderer femalePantsSlot;
    public SkinnedMeshRenderer femaleHairSlot;

    [Header("Clothing Data")]
    public ClothingItem[] maleItems = new ClothingItem[5];
    public ClothingItem[] femaleItems = new ClothingItem[5];

    [Header("UI")]
    public TMP_Text nameText;
    public TMP_Text birthYearText;
    public TMP_Text legendText;

    [Header("Legend Data")]
    public HeroLegends heroLegends;

    [Header("Buttons")]
    public Button restartButton;
    public Button quitButton;

    void Start()
    {
        restartButton.onClick.AddListener(OnRestart);
        quitButton.onClick.AddListener(OnQuit);

        LoadCharacter();
    }

    void LoadCharacter()
    {
        // read saved data
        string gender    = PlayerPrefs.GetString("Gender", "Male");
        string charName  = PlayerPrefs.GetString("CharacterName", "Unknown");
        int age          = PlayerPrefs.GetInt("CharacterAge", 25);
        int accIndex     = PlayerPrefs.GetInt("AccIndex", 0);
        int topIndex     = PlayerPrefs.GetInt("TopIndex", 0);
        int pantIndex    = PlayerPrefs.GetInt("PantIndex", 0);
        int hairIndex    = PlayerPrefs.GetInt("HairIndex", 0);

        bool isMale = gender == "Male";

        // show correct character
        maleCharacter.SetActive(isMale);
        femaleCharacter.SetActive(!isMale);

        // apply clothing to the correct character
        ClothingItem[] items = isMale ? maleItems : femaleItems;

        SkinnedMeshRenderer accSlot  = isMale ? maleAccessorySlot  : femaleAccessorySlot;
        SkinnedMeshRenderer topSlot  = isMale ? maleTopSlot        : femaleTopSlot;
        SkinnedMeshRenderer pantSlot = isMale ? malePantsSlot      : femalePantsSlot;
        SkinnedMeshRenderer hairSlot = isMale ? maleHairSlot       : femaleHairSlot;

        ApplyItem(accSlot,  accIndex,  items);
        ApplyItem(topSlot,  topIndex,  items);
        ApplyItem(pantSlot, pantIndex, items);
        ApplyItem(hairSlot, hairIndex, items);

        // display name
        nameText.text = charName;

        // calculate birth year
        int currentYear = System.DateTime.Now.Year;
        int birthYear = currentYear - age;
        birthYearText.text = $"Born: {birthYear}";

       
        string legend = isMale ? heroLegends.maleLegend : heroLegends.femaleLegend;
        legendText.text = legend.Replace("[NAME]", charName);
    }

    void ApplyItem(SkinnedMeshRenderer slot, int index, ClothingItem[] items)
    {
        if (slot == null) return;

  
        if (index == 0 || index > items.Length || items[index - 1] == null)
        {
            slot.sharedMesh = null;
            slot.sharedMaterials = new Material[0];
            return;
        }

        ClothingItem item = items[index - 1];
        slot.sharedMesh = item.mesh;
        slot.sharedMaterials = item.materials;
    }

    void OnRestart()
    {
    
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("StartScreen");
    }

    void OnQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}