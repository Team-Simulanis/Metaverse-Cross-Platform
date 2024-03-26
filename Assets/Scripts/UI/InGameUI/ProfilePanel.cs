using Doozy.Runtime.UIManager.Components;
using UnityEngine;

public class ProfilePanel : MonoBehaviour
{
    public UISlider environmentVolumeSlider;
    public AudioSource environmentAudioSource;
    public void ChangeEnvironmentVolume()
    {
        environmentAudioSource.volume = environmentVolumeSlider.value;
    }
        
}
