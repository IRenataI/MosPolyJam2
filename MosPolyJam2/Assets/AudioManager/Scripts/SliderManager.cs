using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private AudioSlider[] audioSliders;

    private void Start()
    {
        foreach(AudioSlider audioSlider in audioSliders)
        {
            audioSlider.slider.onValueChanged.AddListener(delegate { AudioManager.Instance.ChangeVolume(audioSlider.audioMixerGroup, audioSlider.slider.value); });
        }
    }
}

[System.Serializable]
public class AudioSlider
{
    public Slider slider;
    public AudioMixerGroup audioMixerGroup;
}
