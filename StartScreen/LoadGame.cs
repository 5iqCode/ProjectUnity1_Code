using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using TMPro;
using UnityEngine.Audio;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private TMP_Text _vallet;

    [SerializeField] private AudioMixerGroup _mixer;

    [SerializeField] private GameObject _backgroundMusic;

    BinaryFormatter _binary = new BinaryFormatter();

    public bool _firstStart=false;

    private void Start()
    {
        FileStream file2 = File.Open(GlobalData.PathVolumeSound, FileMode.Open);
        VolumeSound data2 = (VolumeSound)_binary.Deserialize(file2);
        Debug.Log(data2.MusicVolume);
        _mixer.audioMixer.SetFloat("Interface", data2.InterfaceVolume);
        _mixer.audioMixer.SetFloat("Music", data2.MusicVolume);
        file2.Close();
        StartCoroutine(playMusicCor());
    }

    IEnumerator playMusicCor()
    {
        AudioSource _source = _backgroundMusic.GetComponent<AudioSource>();
        _source.volume = 0;
        _source.Play();
        yield return new WaitForSeconds(0.2f);
        _source.Play();
        _source.volume = 1;
    }

    private void Awake()
    {
        try { 

             //попытка считать данные из файла, если он существует
            FileStream file = File.Open(GlobalData.PathDataGame, FileMode.Open);
            SaveData data = (SaveData)_binary.Deserialize(file);
            _vallet.text = data._Vallet.ToString(); //текущие деньги

            if (data._MaxTime == "00:00:00")  _firstStart = true;


            file.Close();

        }
            catch
            {

                                                                //создание файла если его не существует
                                                            //BinaryFormatter для кодирования
                FileStream file = File.Create(GlobalData.PathDataGame);

                SaveData _data = new SaveData();
                _data._Vallet = 0;
            _data._MaxTime = "00:00:00";
            _data._SelectedHero = 0;
            _binary.Serialize(file, _data);
            file.Close();

            FileStream fileMusic = File.Create(GlobalData.PathVolumeSound);

            VolumeSound dataMusic = new VolumeSound();
            dataMusic.MusicVolume = 0;
            dataMusic.InterfaceVolume = -0;
            _binary.Serialize(fileMusic, dataMusic);
            fileMusic.Close();

            FileStream file2 = File.Create(GlobalData.PathHeroes);

                SaveData2 _data2 = new SaveData2();
                
                _data2.heroes[0,0]= 1;//куплен ли
                _data2.heroes[0,1]= 0;//цена
                _data2.heroes[0,2]= 1; //вес
                _data2.heroes[0,3]= 1; //коэффициент денег
                _data2.heroes[1, 0] = 0;
                _data2.heroes[1, 1] = 150;
                _data2.heroes[1, 2] = 4; 
                _data2.heroes[1, 3] = 2;
            _data2.heroes[2, 0] = 0;
            _data2.heroes[2, 1] = 100;
            _data2.heroes[2, 2] = 0.8f;
            _data2.heroes[2, 3] = 1;
            _data2.heroes[3, 0] = 0;
            _data2.heroes[3, 1] = 100;
            _data2.heroes[3, 2] = 1.2f;
            _data2.heroes[3, 3] = 1;
            _data2.heroes[4, 0] = 0;
            _data2.heroes[4, 1] = 100;
            _data2.heroes[4, 2] = 1.5f;
            _data2.heroes[4, 3] = 1;
            _data2.heroes[5, 0] = 0;
            _data2.heroes[5, 1] = 500;
            _data2.heroes[5, 2] = 1;
            _data2.heroes[5, 3] = 2;
            _data2.heroes[6, 0] = 0;
            _data2.heroes[6, 1] = 700;
            _data2.heroes[6, 2] = 0.5f;
            _data2.heroes[6, 3] = 2;
            _data2.heroes[7, 0] = 0;
            _data2.heroes[7, 1] = 500;
            _data2.heroes[7, 2] = 2;
            _data2.heroes[7, 3] = 3;
            _data2.heroes[8, 0] = 0;
            _data2.heroes[8, 1] = 1250;
            _data2.heroes[8, 2] = 0.7f;
            _data2.heroes[8, 3] = 3;
            _data2.heroes[9, 0] = 0;
            _data2.heroes[9, 1] = 1000;
            _data2.heroes[9, 2] = 1.2f;
            _data2.heroes[9, 3] = 3;
            _data2.heroes[10, 0] = 0;
            _data2.heroes[10, 1] = 1000;
            _data2.heroes[10, 2] = 0.9f;
            _data2.heroes[10, 3] = 3;
            _data2.heroes[11, 0] = 0;
            _data2.heroes[11, 1] = 1500;
            _data2.heroes[11, 2] = 1.5f;
            _data2.heroes[11, 3] = 4;
            _data2.heroes[12, 0] = 0;
            _data2.heroes[12, 1] = 2666;
            _data2.heroes[12, 2] = 2.6f;
            _data2.heroes[12, 3] = 6;
            _data2.heroes[13, 0] = 0;
            _data2.heroes[13, 1] = 3000;
            _data2.heroes[13, 2] = 2;
            _data2.heroes[13, 3] = 7;
            _data2.heroes[14, 0] = 0;
            _data2.heroes[14, 1] = 15000;
            _data2.heroes[14, 2] = 1.48f;
            _data2.heroes[14, 3] = 9;




            _binary.Serialize(file2, _data2);
                file2.Close();


            FileStream file3 = File.Create(GlobalData.PathDoska);

            SaveDoska _data3 = new SaveDoska();

            _data3._selectedDoska = 0;

            //куплен ли
            //цена
            // Положение спавна листьев(максимальный range)
            // вес палки
            // скорость спавна
            _data3.doski[0, 0] = 1;
            _data3.doski[0, 1] = 0;
            _data3.doski[0, 2] = 2.2f;
            _data3.doski[0, 3] = 10;
            _data3.doski[0, 4] = 2;

            _data3.doski[1, 0] = 0;
            _data3.doski[1, 1] = 3000;
            _data3.doski[1, 2] = 1.2f;
            _data3.doski[1, 3] = 6;
            _data3.doski[1, 4] = 1;

            _data3.doski[2, 0] = 0;
            _data3.doski[2, 1] = 4000;
            _data3.doski[2, 2] = 1.8f;
            _data3.doski[2, 3] = 17;
            _data3.doski[2, 4] = 1.7f;

            _binary.Serialize(file3, _data3);
            file3.Close();
        }

            
        
    }
}
