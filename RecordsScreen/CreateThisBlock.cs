using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class CreateThisBlock : MonoBehaviour
{

    [SerializeField] private Sprite[] spritesHero;
    // Start is called before the first frame update
    public void UpdateThisBlock()
    {
        string[,] _MassLastGame = GameObject.Find("Main Camera").GetComponent<CreateAllContent>()._MassLastGames;

       TMP_Text[] _textsBlock = gameObject.GetComponentsInChildren<TMP_Text>();


        try
        {
            int _idBlock = int.Parse(gameObject.name);
            int _idHero = int.Parse(_MassLastGame[_idBlock, 2]);

            _textsBlock[0].text = _MassLastGame[_idBlock, 1];
            _textsBlock[1].text = _MassLastGame[_idBlock, 0];

            gameObject.GetComponentsInChildren<Image>()[3].sprite = spritesHero[_idHero];
        }
        catch
        {
            _textsBlock[0].text = "—";
            _textsBlock[1].text = "—";

            gameObject.GetComponentsInChildren<Image>()[3].sprite = spritesHero[0];
        }
   

    }
}
