using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using TMPro;
using UnityEngine.Audio;

public class SaveGame : MonoBehaviour
{
    [SerializeField] public TMP_Text _timerThis;
    [SerializeField] public TMP_Text _scoreThis;
    [SerializeField] private Transform _heroPos;

    public int _valletLast;
    public string _timeLast;
    public int _secLast;//сколько максимум времени был
    public int _secThis;//сколько сейчас
    public int _allMoneyLast;
    public int _selectedHero;

    [SerializeField] private TMP_Text _recordText; //РЕКОРД

    private void Awake() //загрузка всех данных в начале игры
    {
        BinaryFormatter _binary = new BinaryFormatter();
        FileStream file = File.Open(GlobalData.PathDataGame, FileMode.Open);
        SaveData data = (SaveData)_binary.Deserialize(file);
        _valletLast = data._Vallet; // даньги до начала игры
        _allMoneyLast = data._AllMoney;
        _timeLast = data._MaxTime;
        _selectedHero = data._SelectedHero;

        _recordText.text = data._MaxTime;
file.Close();

    }
    private void Start()
    {
        Application.targetFrameRate = 60;

        int minLast = int.Parse(_timeLast[0].ToString())*10 + int.Parse(_timeLast[1].ToString());
        int secLast = int.Parse(_timeLast[3].ToString()) * 10 + int.Parse(_timeLast[4].ToString());
        _secLast = minLast * 60 + secLast;
    }



    [SerializeField] private AudioMixerGroup _mixer;

    [SerializeField] private GameObject _endGameMenu;
    private void FixedUpdate()//проверка находится ли объект в игровой области
    {
        if (_heroPos.position.y < -10)
        {
            GameObject.Find("GameOverSound").GetComponent<AudioSource>().Play();
            int minThis = int.Parse(_timerThis.text[0].ToString()) * 10 + int.Parse(_timerThis.text[1].ToString());
            int secThis = int.Parse(_timerThis.text[3].ToString()) * 10 + int.Parse(_timerThis.text[4].ToString());
            _secThis = minThis * 60 + secThis;
            SaveData();
            SaveThisGameScore();
            Time.timeScale = 0;
            Instantiate(_endGameMenu);
        }
    }
    private void SaveData() //сохранение данных
    {
        BinaryFormatter _binary = new BinaryFormatter();
        FileStream file = File.Open(GlobalData.PathDataGame, FileMode.Open);

        SaveData _data = new SaveData();
        _data._Vallet = _valletLast + int.Parse(_scoreThis.text);
        _data._AllMoney = _allMoneyLast + int.Parse(_scoreThis.text);
        _data._SelectedHero = _selectedHero;
        if (_secLast < _secThis)
        {
            _data._MaxTime = _timerThis.text;
        }else
            _data._MaxTime = _timeLast;
        _binary.Serialize(file, _data);

        file.Close();

    }

    private bool _SaveStatus = false;

    private void SaveThisGameScore()
    {
        if (_SaveStatus == false)
        {
            _SaveStatus = true;

            try
            {
                BinaryFormatter _binary = new BinaryFormatter();
                FileStream file = File.Open(GlobalData.PathLast5Strings, FileMode.Open);

                Last5Game dataFirstData = (Last5Game)_binary.Deserialize(file);



                int _countStr = 0;

                string[,] _TempSrtings = new string[5, 3];

                for (int i = 0; i < 5; i++)
                {
                    _TempSrtings[i, 0] = dataFirstData._lastsGames[i, 0];
                    _TempSrtings[i, 1] = dataFirstData._lastsGames[i, 1];
                    _TempSrtings[i, 2] = dataFirstData._lastsGames[i, 2];

                    if (dataFirstData._lastsGames[i, 0] != null)
                    {
                        _countStr++;
                    }

                    Debug.Log(_TempSrtings[i, 0]);
                }

                Debug.Log(_countStr);

                file.Close();

                FileStream fileSave = File.Open(GlobalData.PathLast5Strings, FileMode.Open);

                Last5Game data = new Last5Game();

                if (_countStr < 5)
                {
                    for (int i = 0; i < _countStr; i++)
                    {
                        data._lastsGames[i, 0] = _TempSrtings[i, 0];
                        data._lastsGames[i, 1] = _TempSrtings[i, 1];
                        data._lastsGames[i, 2] = _TempSrtings[i, 2];

                    }

                    data._lastsGames[_countStr, 0] = _timerThis.text;
                    data._lastsGames[_countStr, 1] = _scoreThis.text;
                    data._lastsGames[_countStr, 2] = _selectedHero.ToString();
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        data._lastsGames[i, 0] = _TempSrtings[i + 1, 0];
                        data._lastsGames[i, 1] = _TempSrtings[i + 1, 1];
                        data._lastsGames[i, 2] = _TempSrtings[i + 1, 2];
                    }

                    data._lastsGames[4, 0] = _timerThis.text;
                    data._lastsGames[4, 1] = _scoreThis.text;
                    data._lastsGames[4, 2] = _selectedHero.ToString();
                }

                _binary.Serialize(fileSave, data);


                fileSave.Close();
            }
            catch
            {
                BinaryFormatter _binary = new BinaryFormatter();
                FileStream file2 = File.Create(GlobalData.PathLast5Strings);

                Last5Game _data = new Last5Game();
                _data._lastsGames[0, 0] = _timerThis.text;
                _data._lastsGames[0, 1] = _scoreThis.text;
                _data._lastsGames[0, 2] = 0.ToString();

                _binary.Serialize(file2, _data);

                file2.Close();

                Debug.Log(_data._lastsGames[0, 0]);
            }

        }
    }
}



