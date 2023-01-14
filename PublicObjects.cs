using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData
{

    private static string _version = 46.ToString();


    public static string PathLast5Strings = Application.persistentDataPath + "Last5Gamesv" + _version + ".dat";

    public static string PathDataGame = Application.persistentDataPath + "DataGamev" + _version + ".dat";

    public static string PathHeroes = Application.persistentDataPath + "heroesv" + _version + ".dat";

    public static string PathDoska = Application.persistentDataPath + "doskiv" + _version + ".dat";

    public static string PathVolumeSound = Application.persistentDataPath + "volumesettingsv"+_version+".dat";
}

[Serializable]

class VolumeSound
{
    public float MusicVolume;
    public float InterfaceVolume;
}

[Serializable]
class Last5Game
{
    public string[,] _lastsGames = new string[5, 3];// время, деньги, id героя
};

[Serializable]
class SaveData2
{
    public float[,] heroes = new float[15, 4];//всего героев
};

[Serializable]
class SaveData
{
    public string _MaxTime; //общий класс(максимальное время выбранный персонаж, всего заработано, деньги игрока
                            //, выбранный герой)
    public int _AllMoney;
    public int _Vallet;
    public int _SelectedHero;
};

[Serializable]

class SaveDoska
{

    public int _selectedDoska;
    public float[,] doski = new float[3,5]; //куплен ли
                                            //цена
                                            // Положение спавна листьев(максимальный range)
                                            // вес палки
                                            // скорость спавна
}