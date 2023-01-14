using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeValue : MonoBehaviour
{
    [SerializeField] public Image _UIVolumeImage;
    [SerializeField] public Image _UIVolumeImage2;
    [SerializeField] public Image _UIVolumeText;
    [SerializeField] public Slider _UISlider;



    [SerializeField] public Image _MusicVolumeImage;
    [SerializeField] public Image _MusicVolumeImage2;
    [SerializeField] public Image _MusicVolumeText;
    [SerializeField] public Slider _MusicSlider;

    [SerializeField] public Sprite[] _spritesVol;

    private float _tempUI;
    private float _tempMusic;


    BinaryFormatter _binary = new BinaryFormatter();

    public void Start()
    {
        
        FileStream file2 = File.Open(GlobalData.PathVolumeSound, FileMode.Open);
        VolumeSound data2 = (VolumeSound)_binary.Deserialize(file2);

        _tempUI = (data2.InterfaceVolume-20)/100 + 1;
        _tempMusic = data2.MusicVolume/80+1;
        Debug.Log(_tempUI);
        _UISlider.value = _tempUI;
        _MusicSlider.value = _tempMusic;
        file2.Close();

    }

    public void playSoundClick()
    {
        AudioSource ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        ClickSound.pitch = UnityEngine.Random.Range(1f, 3f);
        ClickSound.Play();
    }


    [SerializeField] private AudioMixerGroup _mixer;

    public void ChangeVolUI()
    {
        _mixer.audioMixer.SetFloat("Interface", Mathf.Lerp(-80, 20, _UISlider.value));
    }
    public void ChangeVolMusic()
    {

        _mixer.audioMixer.SetFloat("Music", Mathf.Lerp(-80,0, _MusicSlider.value));
    }

    public void SaveVolSet()
    {

        FileStream fileMusic = File.Create(GlobalData.PathVolumeSound);

        VolumeSound dataMusic = new VolumeSound();
        dataMusic.MusicVolume = Mathf.Lerp(-80, 0, _MusicSlider.value);
        dataMusic.InterfaceVolume = Mathf.Lerp(-80, 20, _UISlider.value);
        _binary.Serialize(fileMusic, dataMusic);
        fileMusic.Close();
    }


    public void UpdateUIImage()
    {
        if (_UISlider.value <= 0)
        {
            _UIVolumeImage.sprite = _spritesVol[0];
            
        }
        else if (_UISlider.value <= 0.33f)
        {
            _UIVolumeImage.sprite = _spritesVol[1];
        }
        else if (_UISlider.value <= 0.66f)
        {
            _UIVolumeImage.sprite = _spritesVol[2];
        }
        else
        {
            _UIVolumeImage.sprite = _spritesVol[3];
        }
        
        if (_UIVolumeImage.sprite == _spritesVol[0])
        {
            _UIVolumeText.sprite = _spritesVol[4];
            _UIVolumeImage2.sprite = _spritesVol[0];
        }
        else
        {
            _UIVolumeImage2.sprite = _spritesVol[3];
            _UIVolumeText.sprite = _spritesVol[5];
        }
        
    }

    public void UpdateMusicImage()
    {
        if (_MusicSlider.value <= 0)
        {
            _MusicVolumeImage.sprite = _spritesVol[0];
        }
        else if (_MusicSlider.value <= 0.33f)
        {
            _MusicVolumeImage.sprite = _spritesVol[1];
        }
        else if (_MusicSlider.value <= 0.66f)
        {
            _MusicVolumeImage.sprite = _spritesVol[2];
        }
        else
        {
            _MusicVolumeImage.sprite = _spritesVol[3];
        }

        if (_MusicVolumeImage.sprite == _spritesVol[0])
        {
            _MusicVolumeText.sprite = _spritesVol[4];
            _MusicVolumeImage2.sprite = _spritesVol[0];
        }
        else
        {
            _MusicVolumeImage2.sprite = _spritesVol[3];
            _MusicVolumeText.sprite = _spritesVol[5];
        }
    }


    public void OnClickMusicMute()
    {
        GameObject.Find("ClickSound").GetComponent<AudioSource>().Play();
        if (_MusicSlider.value>0)
        {
            _MusicSlider.value = 0;
            UpdateMusicImage();

        } else
        {
            _MusicSlider.value = 0.6f;
            UpdateMusicImage();
        }

    }

    public void OnClickUIMute()
    {
        GameObject.Find("ClickSound").GetComponent<AudioSource>().Play();
        if (_UISlider.value > 0)
        {
            _UISlider.value = 0;
            UpdateUIImage();

        }
        else
        {
            _UISlider.value = 0.6f;
            UpdateUIImage();
        }

    }
}
