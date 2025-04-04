using UnityEngine;
using UnityEngine.UI;

public class PrintGalleryUI : MonoBehaviour
{
    public GameObject galleryPanel;
    public Transform galleryContent;
    public GameObject printButtonPrefab;
    public Button closeGalleryButton;

    private CategorySwitcher categorySwitcher;

    void Start()
    {
        galleryPanel.SetActive(false);
        closeGalleryButton.onClick.AddListener(HideGallery);
        categorySwitcher = FindObjectOfType<CategorySwitcher>();
    }

    public void ShowGallery()
    {
        galleryPanel.SetActive(true);
        PopulateGallery();
    }

    public void HideGallery()
    {
        galleryPanel.SetActive(false);
    }

    void PopulateGallery()
    {
        foreach (Transform child in galleryContent)
            Destroy(child.gameObject);

        for (int i = 0; i < categorySwitcher.prints.Length; i++)
        {
            int index = i;
            GameObject btn = Instantiate(printButtonPrefab, galleryContent);
            btn.GetComponent<Image>().sprite = categorySwitcher.prints[i];
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                categorySwitcher.SetPrintFromGallery(index);
                HideGallery();
            });
        }
    }
}