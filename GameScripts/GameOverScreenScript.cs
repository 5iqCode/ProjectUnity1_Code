using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class GameOverScreenScript : MonoBehaviour
{
    [SerializeField] private Image _ImageHero;

    private SaveGame _saveGameScript;

    private Hero _heroScript;


    private TMP_Text[] tMP_Texts;

    void Start()
    {
        _saveGameScript = GameObject.Find("Main Camera").GetComponent<SaveGame>();

        _heroScript = GameObject.Find("Hero").GetComponent<Hero>();

       tMP_Texts = gameObject.GetComponentsInChildren<TMP_Text>();

        tMP_Texts[1].text = _saveGameScript._scoreThis.text;
        tMP_Texts[0].text = _saveGameScript._timerThis.text;

        _ImageHero.sprite = _heroScript._PublicSelectedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
