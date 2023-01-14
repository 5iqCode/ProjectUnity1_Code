using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SpawnCoin : MonoBehaviour
{
    private float _spawnRange;
    private int _selectedDoska;
    private float _massDoska;


    [SerializeField] private GameObject[] _DoskiPrebaf = new GameObject[3];


    [SerializeField] private GameObject _coin;
    [SerializeField] private GameObject _parent;
    [SerializeField] float _duration;

    [SerializeField] GameObject _effectSpawnCoin;
    private GameObject _tempeffect;

    [SerializeField] private Sprite[] _sprites;

 
    public Coroutine _Job;
    private GameObject _tempCoin;


    private void Awake() // Создание доски
    {
        BinaryFormatter _binary = new BinaryFormatter();

        FileStream file2 = File.Open(GlobalData.PathDoska, FileMode.Open);
        SaveDoska data2 = (SaveDoska)_binary.Deserialize(file2);
        _selectedDoska = data2._selectedDoska;

        _spawnRange = data2.doski[_selectedDoska, 2];

        _massDoska = data2.doski[_selectedDoska, 3];

        _duration = data2.doski[_selectedDoska, 4];

        Debug.Log(_spawnRange);


        file2.Close();

        _parent.GetComponent<Rigidbody2D>().mass = _massDoska;

        Instantiate(_DoskiPrebaf[_selectedDoska], _parent.transform);
    }


    void Start()
    {
        _Job = StartCoroutine(SpawnCoins());
    }

    private float _allDuration;
    private int _StepSpeed=0;

    private IEnumerator SpawnCoins()
    {
        while (true)
        {
           int _CountCoins =  GameObject.FindGameObjectsWithTag("Coin").Length;

            if (_CountCoins < 10)
            {
                float x = Random.Range(-_spawnRange, _spawnRange);

                _tempCoin = Instantiate(_coin, _parent.transform);
                _tempCoin.GetComponent<BoxCollider2D>().enabled = false;
                _tempCoin.transform.localPosition = (new Vector3(x, 0.15f, 1));


                _tempeffect = Instantiate(_effectSpawnCoin, _parent.transform);
                _tempeffect.transform.localPosition = new Vector3(0, -10, 0);

                int RandCoin = Random.Range(0, 6);

                SpriteRenderer _tempCoinSprite = _tempCoin.GetComponent<SpriteRenderer>();
                _tempCoinSprite.sprite = _sprites[RandCoin];



                Tween tween1 = _tempCoin.transform.DOScale(new Vector2(0.125f, 0.21f), _duration);
                Tween tween2 = _tempCoin.transform.DOLocalMoveY(0.231f, _duration);

                tween1.SetEase(Ease.Linear);
                tween2.SetEase(Ease.Linear);

                yield return new WaitForSeconds(_duration);
                _tempCoin.GetComponent<BoxCollider2D>().enabled = true;



                if (_tempCoin.GetComponent<BoxCollider2D>().enabled == true)
                {
                    _tempeffect.transform.localPosition = _tempCoin.GetComponent<BoxCollider2D>().transform.localPosition + new Vector3(0, 0.12f, 0);

                }
                _tempeffect.GetComponent<ParticleSystem>().Play();
                _tempeffect.transform.parent = _tempCoin.transform;


            } else
                yield return new WaitForSeconds(_duration);
            _allDuration += _duration;
            Debug.Log(_duration);
            if ((_allDuration > 15)&&(_StepSpeed==0)) {
                _duration -= _duration*0.10f;
                _StepSpeed = 1;
            }
            if ((_allDuration > 30) && (_StepSpeed == 1))
            {
                _duration -= _duration * 0.15f;
                _StepSpeed = 2;
            }
            if ((_allDuration > 60) && (_StepSpeed == 2))
            {
                _duration -= _duration * 0.15f;
                _StepSpeed = 3;
            }
            if ((_allDuration > 120) && (_StepSpeed == 3))
            {
                _duration -= _duration * 0.15f;
                _StepSpeed = 4;
            }
        }

    }

}
