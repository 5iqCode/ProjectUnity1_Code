using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateDoski : MonoBehaviour
{

    public void UpdateContentDoski()
    {
        VerticalLayoutGroup _verticalGroup = GameObject.Find("Content").GetComponent<VerticalLayoutGroup>();

        RectTransform _reactTransform = GameObject.Find("Content").GetComponent<RectTransform>();

        _reactTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,7124.1f);

        GameObject.Find("Content").transform.localPosition = new Vector3(0, 756.3875f, 0);

        _verticalGroup.spacing = 600;
        _verticalGroup.padding.top = 1200;
    }

    [SerializeField] private Sprite[] _LockedDoskiLots;
    [SerializeField] private Sprite[] _UnlockedDoskiLots;
    [SerializeField] private Sprite[] _SelectedDoskiLots;

    private int _selectedDoska;

    private float[,] _doskiInf = new float[3, 5];

    private GameObject _tempDoska;
    [SerializeField] private GameObject _doskaPrebaf;
    [SerializeField] private GameObject _parent;

    [SerializeField] private TMP_Text _textVallet;

    private int _selectedHero;
    private int _vallet;
    private string _MaxTime;
    private int _AllMoney;

    private Vector3 _ContentShort = new Vector3(1, 0.25f, 1);
    public void OnClickCreateDoskiLots()
    {
        AudioSource ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        ClickSound.pitch = UnityEngine.Random.Range(1f, 3f);
        ClickSound.Play();

        GameObject.Find("Content").transform.localScale = _ContentShort;

        GameObject[] _lotsHeroes =  GameObject.FindGameObjectsWithTag("HeroesLot"); //чистка списка
        
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

        BinaryFormatter _binary = new BinaryFormatter(); //сохранение изначальных данных
        FileStream file = File.Open(GlobalData.PathDoska, FileMode.Open);
        SaveDoska data = (SaveDoska)_binary.Deserialize(file);
        _selectedDoska = data._selectedDoska;
        
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                _doskiInf[i, j] = data.doski[i, j];
            }


            _tempDoska = Instantiate(_doskaPrebaf, _parent.transform); //создание элементов
            _tempDoska.name = i.ToString();
            _tempDoska.transform.localScale += new Vector3(0, 3, 0);

            UpdateDoskiSprite(i); // изменение спрайта объекта в магахине
        }

        file.Close();

        FileStream file2 = File.Open(GlobalData.PathDataGame, FileMode.Open);
        SaveData data2 = (SaveData)_binary.Deserialize(file2);

        _selectedHero = data2._SelectedHero; //«апоминаем текущие данные до сохранени€
        _vallet = data2._Vallet;
        _MaxTime = data2._MaxTime;
        _AllMoney = data2._AllMoney;



        _textVallet.text = _vallet.ToString();

        file2.Close();

    }

    private void UpdateDoskiSprite(int i)
    {
        if (i == _selectedDoska)
        {
            _tempDoska.GetComponent<Image>().sprite = _SelectedDoskiLots[i];
        }
        else if (_doskiInf[i, 0] == 1)
        {
            _tempDoska.GetComponent<Image>().sprite = _UnlockedDoskiLots[i];
        }
        else
            _tempDoska.GetComponent<Image>().sprite = _LockedDoskiLots[i];
    }

    public int _SelectedLotId;

    [SerializeField] private GameObject _messageYesNo;
    [SerializeField] private GameObject _noMoney;

    

    public void OnClickLot()
    {



        if (_doskiInf[_SelectedLotId, 0] == 0)
        {
            if (_vallet > _doskiInf[_SelectedLotId, 1])
            {
                Instantiate(_messageYesNo, transform);
            }
            else
            {
                Instantiate(_noMoney, transform);
            }
        }
        else if (_selectedDoska != _SelectedLotId)
        {
            GameObject.Find(_SelectedLotId.ToString()).GetComponent<Image>().sprite = _SelectedDoskiLots[_SelectedLotId];

            GameObject.Find(_selectedDoska.ToString()).GetComponent<Image>().sprite = _UnlockedDoskiLots[_selectedDoska];

            _selectedDoska = _SelectedLotId;

            SaveData2(0);

        }
    }

    public void OnClickMessageYes()
    {


        GameObject.Find(_SelectedLotId.ToString()).GetComponent<Image>().sprite = _SelectedDoskiLots[_SelectedLotId];

        GameObject.Find(_selectedDoska.ToString()).GetComponent<Image>().sprite = _UnlockedDoskiLots[_selectedDoska];

        _selectedDoska = _SelectedLotId;

        _vallet = _vallet - (int)_doskiInf[_SelectedLotId, 1];

        SaveData1();

        SaveData2(_SelectedLotId);

        _textVallet.text = _vallet.ToString();

        Destroy(GameObject.FindGameObjectWithTag("MessageBox"));
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
    private void SaveData2(int _selectedBuy) 
    {
        BinaryFormatter _binary = new BinaryFormatter();
        FileStream file = File.Open(GlobalData.PathDoska, FileMode.Open);

        SaveDoska _data = new SaveDoska();

        _data._selectedDoska = _selectedDoska;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if ((j == 0) &&(_selectedBuy==i))
                    {
                        _data.doski[i, j] = 1;
                        _doskiInf[i, j]=1;
                    }
                    else if ((j == 1) && (_selectedBuy == i))
                {
                        _data.doski[i, j] = 0;
                        _doskiInf[i, j] = 0;
                    }
                    else
                    {
                        _data.doski[i, j] = _doskiInf[i, j];
                    }
                }

            }
        _binary.Serialize(file, _data);
        file.Close();
    }

}
