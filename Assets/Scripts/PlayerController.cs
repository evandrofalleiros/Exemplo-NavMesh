using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    [SerializeField] private float _moveSpeed = 2f;
    private Rigidbody2D _rb2d;

    void Start(){
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update(){
        float directionX = _rb2d.velocity.x + Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;

        _rb2d.velocity = new Vector2(directionX, _rb2d.velocity.y);        
    }

    private void FixedUpdate() {
        _rb2d.AddForce(transform.up * Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime , ForceMode2D.Impulse);    
    }
}
