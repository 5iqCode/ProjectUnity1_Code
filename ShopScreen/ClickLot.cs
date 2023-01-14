using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickLot : MonoBehaviour
{
    public CreateLots _codeCreateLots;

    public CreateDoski _codeCreateDoski;

    private GameObject _canvas;

    private void Awake()
    {
        _canvas = GameObject.Find("Canvas");
    }

    public void ClickButtonLot() // отправка информации о том на какой объект нажали
    {
        AudioSource ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        ClickSound.pitch = UnityEngine.Random.Range(1f, 3f);
        ClickSound.Play();

        _codeCreateLots = _canvas.GetComponent<CreateLots>();
        Debug.Log(gameObject.name);
        int NameStr = Int32.Parse(gameObject.name);

        _codeCreateLots._SelectedLotId = NameStr;
        _codeCreateLots.OnClickLot();
    }
    
    
    
    public void ClickButtonNo()
    {
        AudioSource ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        ClickSound.pitch = UnityEngine.Random.Range(1f, 3f);
        ClickSound.Play();

        try
        {
            Destroy(gameObject);
        }
        catch
        {
            Debug.Log("Error");
        }
    }

    [SerializeField] private GameObject _donateWindow;

    public void ClickDonate()
    {
        AudioSource ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        ClickSound.pitch = UnityEngine.Random.Range(1f, 3f);
        ClickSound.Play(); 

        Instantiate(_donateWindow, _canvas.transform);
        Destroy(gameObject);
    }

    public void ClickButtonYes()
    {
        AudioSource ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        ClickSound.pitch = UnityEngine.Random.Range(1f, 3f);
        ClickSound.Play();

        _codeCreateLots = _canvas.GetComponent<CreateLots>();

        _codeCreateLots.OnClickMessageYes();
    }

    public void ClickButtonDoski() // отправка информации о том на какой объект нажали
    {
        AudioSource ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        ClickSound.pitch = UnityEngine.Random.Range(1f, 3f);
        ClickSound.Play();


        _codeCreateDoski = _canvas.GetComponent<CreateDoski>();
        int NameStr = Int32.Parse(gameObject.name);

        _codeCreateDoski._SelectedLotId = NameStr;
        _codeCreateDoski.OnClickLot();
    }

    public void ClickButtonYesDoski()
    {
        AudioSource ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        ClickSound.pitch = UnityEngine.Random.Range(1f, 3f);
        ClickSound.Play();

        _codeCreateDoski = _canvas.GetComponent<CreateDoski>();

        _codeCreateDoski.OnClickMessageYes();
    }

    public void CloseDonate()
    {
        AudioSource ClickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        ClickSound.pitch = UnityEngine.Random.Range(1f, 3f);
        ClickSound.Play();

        Destroy(gameObject);
    }

}
