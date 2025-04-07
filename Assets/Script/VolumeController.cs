using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ami.BroAudio;
public class VolumeController : MonoBehaviour
{
    public Image[] volumeBars;
    public float volume = 0.5f;
    private float step = 1f / 8f;
    public enum AudioType
    {
        Master,
        Music,
        Sfx,
        Ambient
    }
    [SerializeField] private AudioType audioType = AudioType.Master;
    private void Start()
    {
        UpdateVolumeBars();
        AdjustVolume();
    }
    public void Increase()
    {
        volume = Mathf.Clamp(volume + step, 0, 1f);
        UpdateVolumeBars();
        AdjustVolume();
    }
    public void Decrease()
    {
        volume = Mathf.Clamp(volume - step, 0f, 1f);
        UpdateVolumeBars();
        AdjustVolume();
    }
    private void UpdateVolumeBars()
    {
        int activeBars = Mathf.RoundToInt(volume * 7);
        for (int i = 0; i < volumeBars.Length; i++)
        {
            if (i < activeBars)
            {
                volumeBars[i].color = new Color(247 / 255f, 230 / 255f, 178 / 255f);
            }
            else
            {
                volumeBars[i].color = Color.clear;
            }
        }
    }
    private void AdjustVolume()
    {
        switch (audioType)
        {
            case AudioType.Master:
                AudioListener.volume = volume;
                break;
            case AudioType.Music:
                BroAudio.SetVolume(BroAudioType.Music,volume);
                break;
            case AudioType.Sfx:
                BroAudio.SetVolume(BroAudioType.SFX, volume);
                break;
            case AudioType.Ambient:
                BroAudio.SetVolume(BroAudioType.Ambience, volume);
                break;

        }
    }

}
