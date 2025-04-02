using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CategorySwitcher : MonoBehaviour
{
    public TMP_Text partText; // Название текущей категории
    public Button button1, button2; // Кнопки переключения категорий
    public Button prevButton, nextButton; // Кнопки переключения деталей

    // Image для каждой категории
    public Image headImage;
    public Image torsoImage;
    public Image legsImage;
    public Image hairImage;
    public Image printImage;

    private string[] categories = { "Голова", "Торс", "Ноги", "Волосы", "Принты" };
    private int currentCategoryIndex = 0;

    // Массивы спрайтов для каждой категории
    public Sprite[] heads;
    public Sprite[] torsos;
    public Sprite[] legs;
    public Sprite[] hairs;
    public Sprite[] prints;

    // Координаты, размеры и масштаб для волос
    public Vector2[] hairPositions; // Позиции (X, Y)
    public Vector2[] hairSizes;     // Размеры (Width, Height)

    // Индексы текущих элементов для каждой категории
    private int headIndex = 0;
    private int torsoIndex = 0;
    private int legsIndex = 0;
    private int hairIndex = 0;
    private int printIndex = 0;

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
        partText.text = categories[currentCategoryIndex]; // Обновляем название категории
        UpdateCharacterParts(); // Загружаем картинку текущей категории
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
            case "Голова":
                headIndex = Mathf.Clamp(headIndex + direction, 0, heads.Length - 1);
                break;
            case "Торс":
                torsoIndex = Mathf.Clamp(torsoIndex + direction, 0, torsos.Length - 1);
                break;
            case "Ноги":
                legsIndex = Mathf.Clamp(legsIndex + direction, 0, legs.Length - 1);
                break;
            case "Волосы":
                hairIndex = Mathf.Clamp(hairIndex + direction, 0, hairs.Length - 1);
                break;
            case "Принты":
                printIndex = Mathf.Clamp(printIndex + direction, 0, prints.Length - 1);
                break;
        }
    }

    private void UpdateCharacterParts()
    {
        if (heads.Length > 0) headImage.sprite = heads[headIndex];
        if (torsos.Length > 0) torsoImage.sprite = torsos[torsoIndex];
        if (legs.Length > 0) legsImage.sprite = legs[legsIndex];

        if (hairs.Length > 0) 
        {
            hairImage.sprite = hairs[hairIndex];

            // Устанавливаем позицию
            if (hairPositions.Length > hairIndex)
                hairImage.rectTransform.anchoredPosition = hairPositions[hairIndex];

            // Устанавливаем точный размер (ширина и высота)
            if (hairSizes.Length > hairIndex)
                hairImage.rectTransform.sizeDelta = hairSizes[hairIndex];
        }

        if (prints.Length > 0) printImage.sprite = prints[printIndex];
    }
}