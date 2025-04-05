using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CategorySwitcher : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text partText;
    public Button button1, button2;
    public Button prevButton, nextButton;
    public GameObject galleryButton;

    public GameObject configPopup;
    public TMP_Text configText;
    public Button closePopupButton;
    public Button doneButton;
    public Button copyButton;
    public TMP_Text copiedMessage;

    [Header("Character Images")]
    public Image hairImage, accessoryImage, printImage, legsImage, torsoImage, headImage, armsImage;

    [Header("Categories")]
    private readonly string[] categories = { "Волосся", "Аксесуар", "Принти", "Ноги", "Руки", "Торс", "Обличчя" };
    private int currentCategoryIndex = 0;

    [Header("Sprites and Names")]
    public Sprite[] hairs, accessories, prints, legs, torsos, heads, arms;
    public string[] hairNames, accessoryNames, printNames, legNames, torsoNames, headNames, armNames;

    [Header("Positions and Sizes")]
    public Vector2[] hairPositions, hairSizes;
    public Vector2[] accessoryPositions, accessorySizes;
    public Vector2[] printPositions, printSizes;
    public Vector2[] legsPositions, legsSizes;
    public Vector2[] armsPositions, armsSizes;

    private int hairIndex, accessoryIndex, printIndex, legsIndex, torsoIndex, headIndex, armsIndex;
    private PrintGalleryUI printGalleryUI;

    private void Start()
    {
        printGalleryUI = FindObjectOfType<PrintGalleryUI>();

        button1.onClick.AddListener(OnCategoryNext);
        button2.onClick.AddListener(OnCategoryPrev);
        prevButton.onClick.AddListener(OnDetailPrev);
        nextButton.onClick.AddListener(OnDetailNext);

        if (doneButton != null) doneButton.onClick.AddListener(ShowConfigurationPopup);
        if (closePopupButton != null) closePopupButton.onClick.AddListener(() => configPopup.SetActive(false));
        if (copyButton != null) copyButton.onClick.AddListener(CopyConfigurationToClipboard);

        if (copiedMessage != null)
            copiedMessage.gameObject.SetActive(false); // по умолчанию скрыт

        UpdateCategoryText();
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
                hairIndex = GetNewIndex(hairIndex, direction, hairs.Length);
                break;
            case "Аксесуар":
                accessoryIndex = GetNewIndex(accessoryIndex, direction, accessories.Length);
                break;
            case "Принти":
                printIndex = GetNewIndex(printIndex, direction, prints.Length);
                break;
            case "Ноги":
                legsIndex = GetNewIndex(legsIndex, direction, legs.Length);
                break;
            case "Руки":
                armsIndex = GetNewIndex(armsIndex, direction, arms.Length);
                break;
            case "Торс":
                torsoIndex = GetNewIndex(torsoIndex, direction, torsos.Length);
                break;
            case "Обличчя":
                headIndex = GetNewIndex(headIndex, direction, heads.Length);
                break;
        }
    }

    private int GetNewIndex(int current, int dir, int max)
    {
        return Mathf.Clamp(current + dir, 0, Mathf.Max(0, max - 1));
    }

    private void UpdateCharacterParts()
    {
        UpdatePart(hairImage, hairs, hairIndex, hairPositions, hairSizes);
        UpdatePart(accessoryImage, accessories, accessoryIndex, accessoryPositions, accessorySizes);
        UpdatePart(printImage, prints, printIndex, printPositions, printSizes);
        UpdatePart(legsImage, legs, legsIndex, legsPositions, legsSizes);
        UpdatePart(armsImage, arms, armsIndex, armsPositions, armsSizes);

        if (torsos.Length > 0) torsoImage.sprite = torsos[torsoIndex];
        if (heads.Length > 0) headImage.sprite = heads[headIndex];
    }

    private void UpdatePart(Image img, Sprite[] sprites, int index, Vector2[] positions, Vector2[] sizes)
    {
        if (sprites.Length == 0 || index >= sprites.Length) return;

        img.sprite = sprites[index];
        if (positions.Length > index)
            img.rectTransform.anchoredPosition = positions[index];
        if (sizes.Length > index)
            img.rectTransform.sizeDelta = sizes[index];
    }

    public void SetPrintFromGallery(int index)
    {
        printIndex = Mathf.Clamp(index, 0, prints.Length - 1);
        UpdateCharacterParts();
    }

    private string GetSafeName(string[] names, int index)
    {
        return (names != null && index >= 0 && index < names.Length) ? names[index] : "-";
    }

    private void ShowConfigurationPopup()
    {
        string result =
            $"Волосся: {GetSafeName(hairNames, hairIndex)}\n" +
            $"Обличчя: {GetSafeName(headNames, headIndex)}\n" +
            $"Торс: {GetSafeName(torsoNames, torsoIndex)}\n" +
            $"Руки: {GetSafeName(armNames, armsIndex)}\n" +
            $"Ноги: {GetSafeName(legNames, legsIndex)}\n" +
            $"Принт: {GetSafeName(printNames, printIndex)}\n" +
            $"Аксесуар: {GetSafeName(accessoryNames, accessoryIndex)}";

        configText.text = result;
        configPopup.SetActive(true);
    }

    private void CopyConfigurationToClipboard()
    {
        GUIUtility.systemCopyBuffer = configText.text;

        if (copiedMessage != null)
        {
            copiedMessage.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(HideCopiedMessage());
        }

        Debug.Log("Скопійовано в буфер обміну: " + configText.text);
    }

    private IEnumerator HideCopiedMessage()
    {
        yield return new WaitForSeconds(2f);
        if (copiedMessage != null)
            copiedMessage.gameObject.SetActive(false);
    }
}