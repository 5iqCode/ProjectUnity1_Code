using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonateWindowScript : MonoBehaviour
{

    [SerializeField] private GameObject _donateMessage;
   public void OnClickDestroyMessage()
    {
        Destroy(gameObject);
    }

    public void OnClickBuyMessage()
    {
        Instantiate(_donateMessage, transform);
    }
}
