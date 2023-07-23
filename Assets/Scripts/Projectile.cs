using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    [Range(1, 1000)]
    private float _speed;
    private Vector2 _moveDirection;
    private RaycastHit2D _hit;
    private RaycastHit2D _oldHit;
    private void Start()
    {   
        _moveDirection = new Vector2(transform.up[0], transform.up[1]);
        _hit = Physics2D.Raycast(transform.position, _moveDirection, 100, 8);

    }

    private void FixedUpdate()
    {
        Debug.Log(_moveDirection);
        transform.position += _speed * Time.fixedDeltaTime * new Vector3(_moveDirection[0], _moveDirection[1]);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = _hit.point + _hit.normal * 0.16f;
        _moveDirection = Vector3.Reflect(_moveDirection, collision.contacts[0].normal);
        _hit = Physics2D.Raycast(transform.position, _moveDirection, 100, 8);
    }


}
