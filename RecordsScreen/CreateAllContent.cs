using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class CreateAllContent : MonoBehaviour
{

    [SerializeField] private GameObject _PrebafLastGame;
    [SerializeField] private GameObject _Parent;

    public string[,] _MassLastGames = new string[5, 3];


    void Start()
    {
        BinaryFormatter _binary = new BinaryFormatter();
        {  
            FileStream file = File.Open(GlobalData.PathLast5Strings, FileMode.Open);

            Last5Game data = (Last5Game)_binary.Deserialize(file);




            for (int i = 0; i < 5; i++)
            {
                _MassLastGames[i, 0] = data._lastsGames[i, 0];
                _MassLastGames[i, 1] = data._lastsGames[i, 1];
                _MassLastGames[i, 2] = data._lastsGames[i, 2];


            }
            GameObject _tempGameObject;
            for (int i = 0; i < 5; i++)
            {

                _tempGameObject = Instantiate(_PrebafLastGame, _Parent.transform);

                _tempGameObject.name = i.ToString();

                _tempGameObject.GetComponent<CreateThisBlock>().UpdateThisBlock();
            }
            
        }//5 position Block
        {
            FileStream file = File.Open(GlobalData.PathDataGame, FileMode.Open);

            SaveData data = (SaveData)_binary.Deserialize(file);

            GameObject.Find("MaxTime").GetComponent<TMP_Text>().text = data._MaxTime;
            GameObject.Find("AllMoney").GetComponent<TMP_Text>().text = data._AllMoney.ToString();
            file.Close();

            FileStream file2 = File.Open(GlobalData.PathHeroes, FileMode.Open);

            SaveData2 data2 = (SaveData2)_binary.Deserialize(file2);

            int CountHero = 1;

            for (int i = 1; i < 15; i++)
            {
                if (data2.heroes[i, 0] == 1)
                {
                    CountHero++;
                }
            }
            if (CountHero < 10)
            {
                GameObject.Find("CostHeroes").GetComponent<TMP_Text>().text ="0"+ CountHero.ToString() + "/15";
            }
            else
            {
                GameObject.Find("CostHeroes").GetComponent<TMP_Text>().text = CountHero.ToString() + "/15";
            }

            file2.Close();

        }// общие результаты блок
    }
}
