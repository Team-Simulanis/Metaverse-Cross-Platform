using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollController : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] float scrollSpeed;
    private void OnEnable()
    {
       scrollRect = GetComponent<ScrollRect>();
    }
    public void scrollLeft()
    {
       if (scrollRect != null) 
        {
            if (scrollRect.horizontalNormalizedPosition >= 0f)
            {
                scrollRect.horizontalNormalizedPosition -= scrollSpeed;
            }
        }
    }
    public void scrollRight()
    {
        if (scrollRect != null)
        {
            if (scrollRect.horizontalNormalizedPosition <= 1f)
            {
                scrollRect.horizontalNormalizedPosition += scrollSpeed;
            }
        }
    }
}
