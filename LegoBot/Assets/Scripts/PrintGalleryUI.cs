using UnityEngine;
using UnityEngine.UI;

public class PrintGalleryUI : MonoBehaviour
{
    public GameObject galleryPanel;
    public Transform galleryContent;
    public GameObject printButtonPrefab;
    public Button closeGalleryButton;

    private CategorySwitcher categorySwitcher;

    private void Start()
    {
        if (galleryPanel != null) galleryPanel.SetActive(false);
        if (closeGalleryButton != null) closeGalleryButton.onClick.AddListener(HideGallery);
        categorySwitcher = FindObjectOfType<CategorySwitcher>();
    }

    public void ShowGallery()
    {
        if (galleryPanel != null)
        {
            galleryPanel.SetActive(true);
            PopulateGallery();
        }
    }

    public void HideGallery()
    {
        if (galleryPanel != null)
            galleryPanel.SetActive(false);
    }

    private void PopulateGallery()
    {
        foreach (Transform child in galleryContent)
            Destroy(child.gameObject);

        if (categorySwitcher == null || categorySwitcher.prints == null) return;

        for (int i = 0; i < categorySwitcher.prints.Length; i++)
        {
            int index = i;
            GameObject btn = Instantiate(printButtonPrefab, galleryContent);
            Image img = btn.GetComponent<Image>();
            if (img != null) img.sprite = categorySwitcher.prints[i];

            Button button = btn.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    categorySwitcher.SetPrintFromGallery(index);
                    HideGallery();
                });
            }
        }
    }
}