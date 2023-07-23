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
    }

    private void FixedUpdate()
    {
        CalculateNextPos();
    }

    private void CalculateNextPos()
    {
        float dist = _speed * Time.fixedDeltaTime;
        _hit = Physics2D.Raycast(transform.position, _moveDirection, dist, 8);
        if(_hit.normal == new Vector2(0, 0))
        {
            transform.position += _speed * Time.fixedDeltaTime * new Vector3(_moveDirection[0], _moveDirection[1]);
        }
        else
        {
            _oldHit.point = transform.position;
            while (_hit.normal != new Vector2(0, 0))
            {
                _moveDirection = Vector3.Reflect(_moveDirection, _hit.normal);
                dist -= Vector3.Distance(_hit.point, _oldHit.point);
                _oldHit = _hit;
                Collider2D colide = _hit.collider;
                colide.enabled = false;
                _hit = Physics2D.Raycast(_hit.point, _moveDirection, dist, 8);
                colide.enabled = true;
            }
            transform.position = _oldHit.point + _moveDirection.normalized * dist;
        }

    }


}
