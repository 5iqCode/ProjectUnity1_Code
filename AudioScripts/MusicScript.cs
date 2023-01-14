using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] MusicObjs = GameObject.FindGameObjectsWithTag("Music");

        if (MusicObjs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
    }
    
    }
