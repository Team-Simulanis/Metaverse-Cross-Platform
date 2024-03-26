using UnityEngine;

public class ControlsPanel : MonoBehaviour
{

    public GameObject[] mobilePanel;
    public GameObject[] desktopPanel;

    private void Start()
    {
        SwitchControlsPanels();
    }

    private void SwitchControlsPanels()
    {
        if (GameManager.Instance.accessData.platform == AccessData.Platform.Android)
        {
            for (var i = 0; i < mobilePanel.Length; i++)
            {
                mobilePanel[i].SetActive(true);
            }

            for(var i = 0;i < desktopPanel.Length; i++)
            {
                desktopPanel[i].SetActive(false);
            }
        }

        if (GameManager.Instance.accessData.platform != AccessData.Platform.Windows &&
            GameManager.Instance.accessData.platform != AccessData.Platform.Mac) return;
        {
            for (var i = 0; i < desktopPanel.Length; i++)
            {
                desktopPanel[i].SetActive(true);
            }

            for (var i = 0;i<mobilePanel.Length;i++)
            {
                mobilePanel[i].SetActive(false);
            }
        }
    }
}
