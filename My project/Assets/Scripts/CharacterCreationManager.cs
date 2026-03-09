using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterCreationManager : MonoBehaviour
{
    [Header("Characters")]
    public GameObject maleCharacter;
    public GameObject femaleCharacter;

    [Header("Clothing Slots on Character (SkinnedMeshRenderers)")]
    public SkinnedMeshRenderer accessorySlot;
    public SkinnedMeshRenderer topSlot;
    public SkinnedMeshRenderer pantsSlot;
    public SkinnedMeshRenderer hairSlot;

    [Header("Gender Dropdown")]
    public TMP_Dropdown genderDropdown;

    [Header("Clothing Panels")]
    public GameObject maleClothingPanel;
    public GameObject femaleClothingPanel;

    [Header("Male Clothing Items (5)")]
    public ClothingItem[] maleItems = new ClothingItem[5];

    [Header("Female Clothing Items (5)")]
    public ClothingItem[] femaleItems = new ClothingItem[5];

    [Header("Render Textures - Male (5)")]
    public RenderTexture[] maleRTs = new RenderTexture[5];

    [Header("Render Textures - Female (5)")]
    public RenderTexture[] femaleRTs = new RenderTexture[5];

    [Header("Clothing Cards - Male RawImages (5)")]
    public RawImage[] maleCards = new RawImage[5];

    [Header("Clothing Cards - Female RawImages (5)")]
    public RawImage[] femaleCards = new RawImage[5];

    [Header("Card Labels - Male (5)")]
    public TMP_Text[] maleLables = new TMP_Text[5];

    [Header("Card Labels - Female (5)")]
    public TMP_Text[] femaleLabels = new TMP_Text[5];

    [Header("Preview Stage")]
    public ClothingPreviewRenderer previewRenderer;

    [Header("Buttons")]
    public Button resetButton;
    public Button finishButton;
    public Button quitButton;

    private bool isMale = true;

    void Start()
    {
        genderDropdown.onValueChanged.AddListener(OnGenderChanged);
        resetButton.onClick.AddListener(ResetCharacter);
        finishButton.onClick.AddListener(OnFinish);
        quitButton.onClick.AddListener(OnQuit);

        SetGender(true);
        RenderAllPreviews();
    }

    void RenderAllPreviews()
    {
        if (previewRenderer == null) return;

        for (int i = 0; i < 5; i++)
        {
            if (i < maleItems.Length && maleItems[i] != null && i < maleRTs.Length)
            {
                previewRenderer.RenderItemToTexture(maleItems[i], maleRTs[i]);
                if (maleLables != null && i < maleLables.Length && maleLables[i] != null)
                    maleLables[i].text = maleItems[i].itemName;
            }

            if (i < femaleItems.Length && femaleItems[i] != null && i < femaleRTs.Length)
            {
                previewRenderer.RenderItemToTexture(femaleItems[i], femaleRTs[i]);
                if (femaleLabels != null && i < femaleLabels.Length && femaleLabels[i] != null)
                    femaleLabels[i].text = femaleItems[i].itemName;
            }
        }
    }

    void OnGenderChanged(int value)
    {
        SetGender(value == 1);
    }

    void SetGender(bool male)
    {
        isMale = male;
        maleCharacter.SetActive(male);
        femaleCharacter.SetActive(!male);
        maleClothingPanel.SetActive(male);
        femaleClothingPanel.SetActive(!male);
        UpdateSlotReferences();
        ResetCharacter();
    }

    void UpdateSlotReferences()
    {
        GameObject active = isMale ? maleCharacter : femaleCharacter;
        accessorySlot = FindSMR(active, "Accessory");
        topSlot       = FindSMR(active, "Top");
        pantsSlot     = FindSMR(active, "Pants");
        hairSlot      = FindSMR(active, "Hair");
    }

    SkinnedMeshRenderer FindSMR(GameObject root, string slotName)
    {
        foreach (var smr in root.GetComponentsInChildren<SkinnedMeshRenderer>())
            if (smr.gameObject.name.Contains(slotName))
                return smr;
        Debug.LogWarning($"SMR slot '{slotName}' not found on {root.name}");
        return null;
    }

    public void ApplyItemByCategory(ClothingItem item, string category)
    {
        switch (category)
        {
            case "Accessory": ApplyToSlot(item, accessorySlot); break;
            case "Top":       ApplyToSlot(item, topSlot);       break;
            case "Pants":     ApplyToSlot(item, pantsSlot);     break;
            case "Hair":      ApplyToSlot(item, hairSlot);      break;
            default:
                Debug.LogWarning($"Unknown category: {category}");
                break;
        }
    }

    void ApplyToSlot(ClothingItem item, SkinnedMeshRenderer slot)
    {
        if (slot == null)
        {
            Debug.LogWarning("Slot is null — check SkinnedMeshRenderer names on prefab");
            return;
        }
        slot.sharedMesh = item != null ? item.mesh : null;
        slot.sharedMaterials = item != null ? item.materials : new Material[0];
    }

    public void ResetCharacter()
    {
        ApplyToSlot(null, accessorySlot);
        ApplyToSlot(null, topSlot);
        ApplyToSlot(null, pantsSlot);
        ApplyToSlot(null, hairSlot);
    }

    void OnFinish()
    {
        PlayerPrefs.SetString("Gender", isMale ? "Male" : "Female");
        PlayerPrefs.Save();
        SceneManager.LoadScene("ReviewScreen");
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