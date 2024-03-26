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
            foreach (var t in mobilePanel)
            {
                t.SetActive(true);
            }

            foreach (var t in desktopPanel)
            {
                t.SetActive(false);
            }
        }

        if (GameManager.Instance.accessData.platform != AccessData.Platform.Windows &&
            GameManager.Instance.accessData.platform != AccessData.Platform.Mac) return;
        {
            foreach (var t in desktopPanel)
            {
                t.SetActive(true);
            }

            foreach (var t in mobilePanel)
            {
                t.SetActive(false);
            }
        }
    }
}
