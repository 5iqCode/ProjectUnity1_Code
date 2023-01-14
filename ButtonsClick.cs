using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsClick : MonoBehaviour
{


    private AudioSource _clickSound;

    private void Start()
    {
        
        _clickSound = GameObject.FindGameObjectWithTag("ClickButtonUI").GetComponent<AudioSource>();
    }

    private void ClickSound()
    {
        try
        {
            _clickSound.pitch = Random.Range(1f, 2f);
            _clickSound.Play();
        }
        catch { };
        }

    [SerializeField] private GameObject _PrebafFirstStart;

    public void OnClickButPlayGame()
    {
        ClickSound();
        if (GameObject.Find("Main Camera").GetComponent<LoadGame>()._firstStart == false)
        {
            SceneManager.LoadScene(1);
        } else
        {
            Instantiate(_PrebafFirstStart, gameObject.transform);
            gameObject.GetComponent<LoadGame>()._firstStart = false;
        }
        
    }

    public void OnClickButRestart()
    {
        ClickSound();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void OnClickButShop()
    {

        SceneManager.LoadScene(2);
    }

    public void OnClickGoStartScreen()
    {
        ClickSound();
        SceneManager.LoadScene(0);
    }
    public void OnClickGoStartScreenFromGame()
    {
        ClickSound();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void OnClickLiders()
    {
        ClickSound();
        SceneManager.LoadScene(3);
    }

    public void OnClickSettings()
    {
        ClickSound();
        SceneManager.LoadScene(4);
    }

    public void OnClickVK()
    {
        ClickSound();
        Application.OpenURL("https://vk.com/depget");
    }
    public void OnClickTg()
    {
        ClickSound();
        Application.OpenURL("https://t.me/+4pS944FvvVVlN2Yy");
    }
    

}
