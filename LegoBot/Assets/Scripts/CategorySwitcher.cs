using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CategorySwitcher : MonoBehaviour
{
    public TMP_Text partText;
    public Button button1, button2;
    public Button prevButton, nextButton;
    public GameObject galleryButton;

    public Image hairImage, accessoryImage, printImage, legsImage, torsoImage, headImage, armsImage;

    private string[] categories = { "Волосся", "Аксесуар", "Принти", "Ноги", "Руки", "Торс", "Обличчя" };
    private int currentCategoryIndex = 0;

    public Sprite[] hairs, accessories, prints, legs, torsos, heads, arms;
    public string[] hairNames, accessoryNames, printNames, legNames, torsoNames, headNames, armNames;

    public Vector2[] hairPositions, hairSizes;
    public Vector2[] accessoryPositions, accessorySizes;
    public float[] accessoryRotationsZ; // ✅ Новый массив поворотов аксессуаров
    public Vector2[] printPositions, printSizes;
    public Vector2[] legsPositions, legsSizes;
    public Vector2[] armsPositions, armsSizes;

    private int hairIndex = 0, accessoryIndex = 0, printIndex = 0, legsIndex = 0, torsoIndex = 0, headIndex = 0, armsIndex = 0;

    private PrintGalleryUI printGalleryUI;

    public GameObject configPopup;
    public TMP_Text configText;
    public Button closePopupButton;
    public Button doneButton;

    void Start()
    {
        printGalleryUI = Object.FindFirstObjectByType<PrintGalleryUI>();
        UpdateCategoryText();

        button1.onClick.AddListener(OnCategoryNext);
        button2.onClick.AddListener(OnCategoryPrev);
        prevButton.onClick.AddListener(OnDetailPrev);
        nextButton.onClick.AddListener(OnDetailNext);

        if (doneButton != null) doneButton.onClick.AddListener(ShowConfigurationPopup);
        if (closePopupButton != null) closePopupButton.onClick.AddListener(() => configPopup.SetActive(false));

        UpdateCharacterParts();
    }

    private void UpdateCategoryText()
    {
        partText.text = categories[currentCategoryIndex];
        galleryButton.SetActive(categories[currentCategoryIndex] == "Принти");
        UpdateCharacterParts();
    }

    private void OnCategoryNext()
    {
        currentCategoryIndex = (currentCategoryIndex + 1) % categories.Length;
        UpdateCategoryText();
    }

    private void OnCategoryPrev()
    {
        currentCategoryIndex = (currentCategoryIndex - 1 + categories.Length) % categories.Length;
        UpdateCategoryText();
    }

    private void OnDetailNext()
    {
        ChangeDetailIndex(1);
        UpdateCharacterParts();
    }

    private void OnDetailPrev()
    {
        ChangeDetailIndex(-1);
        UpdateCharacterParts();
    }

    private void ChangeDetailIndex(int direction)
    {
        switch (categories[currentCategoryIndex])
        {
            case "Волосся":
                hairIndex = Mathf.Clamp(hairIndex + direction, 0, hairs.Length - 1);
                break;
            case "Аксесуар":
                accessoryIndex = Mathf.Clamp(accessoryIndex + direction, 0, accessories.Length - 1);
                break;
            case "Принти":
                printIndex = Mathf.Clamp(printIndex + direction, 0, prints.Length - 1);
                break;
            case "Ноги":
                legsIndex = Mathf.Clamp(legsIndex + direction, 0, legs.Length - 1);
                break;
            case "Торс":
                torsoIndex = Mathf.Clamp(torsoIndex + direction, 0, torsos.Length - 1);
                break;
            case "Обличчя":
                headIndex = Mathf.Clamp(headIndex + direction, 0, heads.Length - 1);
                break;
            case "Руки":
                armsIndex = Mathf.Clamp(armsIndex + direction, 0, arms.Length - 1);
                break;
        }
    }

    private void UpdateCharacterParts()
    {
        if (hairs.Length > 0)
        {
            hairImage.sprite = hairs[hairIndex];
            if (hairPositions.Length > hairIndex)
                hairImage.rectTransform.anchoredPosition = hairPositions[hairIndex];
            if (hairSizes.Length > hairIndex)
                hairImage.rectTransform.sizeDelta = hairSizes[hairIndex];
        }

        if (accessories.Length > 0)
        {
            accessoryImage.sprite = accessories[accessoryIndex];
            if (accessoryPositions.Length > accessoryIndex)
                accessoryImage.rectTransform.anchoredPosition = accessoryPositions[accessoryIndex];
            if (accessorySizes.Length > accessoryIndex)
                accessoryImage.rectTransform.sizeDelta = accessorySizes[accessoryIndex];
            if (accessoryRotationsZ.Length > accessoryIndex)
                accessoryImage.rectTransform.rotation = Quaternion.Euler(0, 0, accessoryRotationsZ[accessoryIndex]);
        }

        if (prints.Length > 0)
        {
            printImage.sprite = prints[printIndex];
            if (printPositions.Length > printIndex)
                printImage.rectTransform.anchoredPosition = printPositions[printIndex];
            if (printSizes.Length > printIndex)
                printImage.rectTransform.sizeDelta = printSizes[printIndex];
        }

        if (legs.Length > 0)
        {
            legsImage.sprite = legs[legsIndex];
            if (legsPositions.Length > legsIndex)
                legsImage.rectTransform.anchoredPosition = legsPositions[legsIndex];
            if (legsSizes.Length > legsIndex)
                legsImage.rectTransform.sizeDelta = legsSizes[legsIndex];
        }

        if (arms.Length > 0)
        {
            armsImage.sprite = arms[armsIndex];
            if (armsPositions.Length > armsIndex)
                armsImage.rectTransform.anchoredPosition = armsPositions[armsIndex];
            if (armsSizes.Length > armsIndex)
                armsImage.rectTransform.sizeDelta = armsSizes[armsIndex];
        }

        if (torsos.Length > 0)
            torsoImage.sprite = torsos[torsoIndex];
        if (heads.Length > 0)
            headImage.sprite = heads[headIndex];
    }

    public void SetPrintFromGallery(int index)
    {
        printIndex = Mathf.Clamp(index, 0, prints.Length - 1);
        UpdateCharacterParts();
    }

    private string GetSafeName(string[] names, int index)
    {
        if (names != null && index >= 0 && index < names.Length)
            return names[index];
        return "-";
    }

    private void ShowConfigurationPopup()
    {
        string result =
            "Волосся: " + GetSafeName(hairNames, hairIndex) + "\n" +
            "Обличчя: " + GetSafeName(headNames, headIndex) + "\n" +
            "Торс: " + GetSafeName(torsoNames, torsoIndex) + "\n" +
            "Руки: " + GetSafeName(armNames, armsIndex) + "\n" +
            "Ноги: " + GetSafeName(legNames, legsIndex) + "\n" +
            "Принт: " + GetSafeName(printNames, printIndex) + "\n" +
            "Аксесуар: " + GetSafeName(accessoryNames, accessoryIndex);

        configText.text = result;
        configPopup.SetActive(true);
    }
}