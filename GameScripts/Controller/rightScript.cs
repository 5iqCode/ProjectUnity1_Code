using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rightScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _Hero;
    [SerializeField] private float _speed;
    [SerializeField] private float _posForce;
    private bool _isActive;
    public void OnPressBut()
    {
        _isActive = true;
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            _isActive=false;
            _Hero.AddForceAtPosition(new Vector2(1 * _speed * Time.deltaTime*_Hero.mass, _speed * Time.deltaTime * _Hero.mass), _Hero.position + new Vector2(0, _posForce), ForceMode2D.Impulse);
        }
        
    }
}
