using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Hero : MonoBehaviour
{
    public int _CountCoun;
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private Rigidbody2D _heroBody;

    [SerializeField] private Sprite[] _sprites;

    public Sprite _PublicSelectedSprite;
    private int k; //коэффициент монет

    private AudioSource _takeCoinSound;

    private void Start()
    {
        _takeCoinSound = GameObject.Find("TakeCoin").GetComponent<AudioSource>();

        int _selectedHero = (int)_mainCamera.GetComponent<SaveGame>()._selectedHero;
        SpriteRenderer _SelectedSprite = gameObject.GetComponent<SpriteRenderer>();

        BinaryFormatter _binary = new BinaryFormatter();
        FileStream file = File.Open(GlobalData.PathHeroes, FileMode.Open);
        SaveData2 data = (SaveData2)_binary.Deserialize(file);
        
        k = (int)data.heroes[_selectedHero, 3];
        _heroBody.mass = (float) data.heroes[_selectedHero, 2];


        Debug.Log(data.heroes[_selectedHero, 2]);

        file.Close();

        _SelectedSprite.sprite = _sprites[_selectedHero];

        _PublicSelectedSprite = _SelectedSprite.sprite;
    }

    [SerializeField] TMP_Text _textCoins;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out Coin _coin)) // поиск объекта с компонентом Player
                                                             // и помещение его в player (out Player player)
        {
            _CountCoun += 1*k;
            _textCoins.text =_CountCoun.ToString();
            Destroy(collision.gameObject);
            _takeCoinSound.pitch = UnityEngine.Random.Range(1f, 1.3f);
            _takeCoinSound.Play();
        }
    }
}
