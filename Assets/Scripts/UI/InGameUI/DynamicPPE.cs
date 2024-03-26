using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicPPE : MonoBehaviour
{
    public Image image;
    public RectTransform container;
    public TextMeshProUGUI header;
    public string inputHeader;
    public Sprite[] ppeSprites;

    [Button]
    public void CreatePpeImage()
    {
        header.text = inputHeader;
        for (int i = 0; i < ppeSprites.Length; i++)
        {
            Image newobj = Instantiate(image,container);
            newobj.sprite = ppeSprites[i];
        }
        image.gameObject.SetActive(false);
    }

    public void ppeSelected()
    {
        this.gameObject.SetActive(false);
    }
}
