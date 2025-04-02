using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategorySwitcher : MonoBehaviour
{
    public TMP_Text partText; // Название текущей категории
    public Button button1, button2; // Кнопки переключения категорий
    public Button prevButton, nextButton; // Кнопки переключения деталей

    // Image для каждой категории
    public Image hairImage;  // Волосся
    public Image accessoryImage; // Аксесуар
    public Image printImage; // Принти
    public Image legsImage;  // Ноги
    public Image torsoImage; // Торс
    public Image headImage;  // Обличчя

    private string[] categories = { "Волосся", "Аксесуар", "Принти", "Ноги", "Торс", "Обличчя" };
    private int currentCategoryIndex = 0;

    // Массивы спрайтов для каждой категории
    public Sprite[] hairs;
    public Sprite[] accessories;
    public Sprite[] prints;
    public Sprite[] legs;
    public Sprite[] torsos;
    public Sprite[] heads;

    // Координаты и размеры для волос, аксессуаров, принтов и ног
    public Vector2[] hairPositions;
    public Vector2[] hairSizes;

    public Vector2[] accessoryPositions;
    public Vector2[] accessorySizes;

    public Vector2[] printPositions;
    public Vector2[] printSizes;

    public Vector2[] legsPositions;
    public Vector2[] legsSizes;

    // Индексы текущих элементов для каждой категории
    private int hairIndex = 0;
    private int accessoryIndex = 0;
    private int printIndex = 0;
    private int legsIndex = 0;
    private int torsoIndex = 0;
    private int headIndex = 0;

    void Start()
    {
        UpdateCategoryText();
        button1.onClick.AddListener(OnCategoryNext);
        button2.onClick.AddListener(OnCategoryPrev);
        prevButton.onClick.AddListener(OnDetailPrev);
        nextButton.onClick.AddListener(OnDetailNext);
        UpdateCharacterParts();
    }

    private void UpdateCategoryText()
    {
        partText.text = categories[currentCategoryIndex]; 
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

        if (torsos.Length > 0) torsoImage.sprite = torsos[torsoIndex];
        if (heads.Length > 0) headImage.sprite = heads[headIndex];
    }
}