using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateLots : MonoBehaviour
{
    public void UpdateContent()
    {
        VerticalLayoutGroup _verticalGroup = GameObject.Find("Content").GetComponent<VerticalLayoutGroup>();

        RectTransform _reactTransform = GameObject.Find("Content").GetComponent<RectTransform>();

        _reactTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 6537.1f);

        GameObject.Find("Content").transform.localPosition = new Vector3(0, -3268.55f, 0);


        _verticalGroup.spacing = 0;   
        _verticalGroup.padding.top = 85;
    }

    [SerializeField] public Sprite[] _lockedLots;
    [SerializeField] public Sprite[] _unlockedLots;
    [SerializeField] public Sprite[] _selectedLots;

    [SerializeField] private GameObject _shopLot;
    [SerializeField] private GameObject _content;

    [SerializeField] private TMP_Text _textVallet;

    private GameObject _tempCoin;
    private Image _imageTempCoin;
    
    public int _selectedHero; 
    public int _vallet;
    public string _MaxTime; 
    public int _AllMoney;


    private float[,] _heroStatPrice = new float[15,4];// 1- куплен ли
                                                    //2 - цена
    void Start()
    {
        OnClickCreateLots();
        
    }
    public void OnClickCreateLots()
    {
        AudioSource ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        ClickSound.pitch = UnityEngine.Random.Range(1f, 3f);
        ClickSound.Play();

        GameObject.Find("Content").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Content").transform.localPosition = new Vector3(0, -3268.55f, 0);

        GameObject[] _lotsHeroes = GameObject.FindGameObjectsWithTag("HeroesLot"); //чистка списка

        GameObject[] _lotsDoski = GameObject.FindGameObjectsWithTag("DoskiLot");


        if (_lotsHeroes.Length != 0)
        {
            for (int i = 0; i < _lotsHeroes.Length; i++)
            {
                Destroy(_lotsHeroes[i]);
            }
        }
        if (_lotsDoski.Length != 0)
        {
            for (int i = 0; i < _lotsDoski.Length; i++)
            {
                Destroy(_lotsDoski[i]);
            }
        }

        BinaryFormatter _binary = new BinaryFormatter();
            FileStream file = File.Open(GlobalData.PathDataGame, FileMode.Open);
            SaveData data = (SaveData)_binary.Deserialize(file);

            _selectedHero = data._SelectedHero; //Запоминаем текущие данные до сохранения
            _vallet = data._Vallet;
            _MaxTime = data._MaxTime;
            _AllMoney = data._AllMoney;



            _textVallet.text = _vallet.ToString();

            file.Close();

            FileStream file2 = File.Open(GlobalData.PathHeroes, FileMode.Open);
            SaveData2 data2 = (SaveData2)_binary.Deserialize(file2);
            for (int i = 0; i <= 14; i++)
            {
                _tempCoin = Instantiate(_shopLot, _content.transform);
                _tempCoin.GetComponent<ClickLot>()._codeCreateLots = gameObject.GetComponent<CreateLots>();
                _imageTempCoin = _tempCoin.GetComponent<Image>();
                _tempCoin.name = i.ToString();

                Debug.Log(i);

                _heroStatPrice[i, 0] = (int)data2.heroes[i, 0];
                _heroStatPrice[i, 1] = (int)data2.heroes[i, 1];
                _heroStatPrice[i, 2] = (float)data2.heroes[i, 2];
                _heroStatPrice[i, 3] = (int)data2.heroes[i, 3];

                UpdateSprite(i);
            }
            file2.Close();
        
    }
    private void UpdateSprite(int i)
    {
        if (i == _selectedHero)
        {
            _imageTempCoin.sprite = _selectedLots[i];
        }
        else if (_heroStatPrice[i, 0] == 1)
        {
            _imageTempCoin.sprite = _unlockedLots[i];
        }
        else
        {
            _imageTempCoin.sprite = _lockedLots[i];
        }
    }



    [SerializeField] public GameObject _messageYesNo;

    [SerializeField] public GameObject _noMoney;

    public int _SelectedLotId;

    private Sprite _ClickedLotSprite;


    

    public void OnClickLot()
    {


        Debug.Log(_heroStatPrice[_SelectedLotId, 1] + "wtyf");
        if (_heroStatPrice[_SelectedLotId, 0] == 0)
        {
            if (_vallet > _heroStatPrice[_SelectedLotId, 1])
            {
                Instantiate(_messageYesNo, transform);
            }
            else
            {
                Instantiate(_noMoney, transform);
            }
        }
        else if (_selectedHero != _SelectedLotId)
        {
            GameObject.Find(_SelectedLotId.ToString()).GetComponent<Image>().sprite = _selectedLots[_SelectedLotId];

            GameObject.Find(_selectedHero.ToString()).GetComponent<Image>().sprite = _unlockedLots[_selectedHero];

            _selectedHero = _SelectedLotId;

            SaveData1();

        }
    }

    private void SaveData1() //сохранение данных
    {
        BinaryFormatter _binary = new BinaryFormatter();
        FileStream file = File.Open(GlobalData.PathDataGame, FileMode.Open);

        SaveData _data = new SaveData();
        _data._Vallet = _vallet;
        _data._AllMoney = _AllMoney;
        _data._SelectedHero = _selectedHero;
        _data._MaxTime = _MaxTime;
        _binary.Serialize(file, _data);

        file.Close();

    }

    private void SaveData2() //сохранение данных о героях
    {

        BinaryFormatter _binary = new BinaryFormatter();
        FileStream file2 = File.Open(GlobalData.PathHeroes,FileMode.Open);

        SaveData2 _data2 = new SaveData2();

        for (int i = 0; i <= 14; i++)
        {
            if (i == _SelectedLotId) 
            {
                _data2.heroes[i, 0] = 1;
                _data2.heroes[i, 1] = -1;
                _heroStatPrice[i, 0] = 1;
                _heroStatPrice[i, 1] = -1;
            }
            else
            {
                _data2.heroes[i, 0] = _heroStatPrice[i, 0];
                _data2.heroes[i, 1] = _heroStatPrice[i, 1];
            }
            _data2.heroes[i, 2] = (float)_heroStatPrice[i, 2];
            _data2.heroes[i, 3] = _heroStatPrice[i, 3];
            Debug.Log(_data2.heroes[i, 0] + "   " + _data2.heroes[i, 1] + "   " + _data2.heroes[i, 2] + "   " + _data2.heroes[i, 3] + "   ");
        }
        



        _binary.Serialize(file2, _data2);
        file2.Close();

    }

    public void OnClickMessageYes()
    {


        GameObject.Find(_SelectedLotId.ToString()).GetComponent<Image>().sprite = _selectedLots[_SelectedLotId];

        GameObject.Find(_selectedHero.ToString()).GetComponent<Image>().sprite = _unlockedLots[_selectedHero];

        _selectedHero = _SelectedLotId;

        _vallet = _vallet - (int)_heroStatPrice[_SelectedLotId, 1];

        SaveData1();

        SaveData2();

        _textVallet.text = _vallet.ToString();

        Destroy(GameObject.FindGameObjectWithTag("MessageBox"));
    }


}


