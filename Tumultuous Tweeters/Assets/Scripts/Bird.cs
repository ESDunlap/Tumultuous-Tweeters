
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance = 5;
    Crate[] _crates;

    Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;


    public bool IsDragging { get; private set; }
    public bool IsShot { get; private set; }

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _crates = FindObjectsOfType<Crate>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    void OnMouseDown()
    {
        if (!IsShot)
        {
            _spriteRenderer.color = Color.red;
            IsDragging = true;
        }
    }

    void OnMouseUp()
    {
        if (!IsShot)
        {
            Vector2 currentPosition = _rigidbody2D.position;
            Vector2 direction = _startPosition - currentPosition;
            direction.Normalize();


            _rigidbody2D.isKinematic = false;
            _rigidbody2D.AddForce(direction * _launchForce);

            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();

            _spriteRenderer.color = Color.white;
            IsDragging = false;
            IsShot = true;
        }
    }

    void OnMouseDrag()
    {
        if (!IsShot)
        { 
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 desiredPosition = mousePosition;

            float distance = Vector2.Distance(desiredPosition, _startPosition);
            if (distance > _maxDragDistance)
            {
                Vector2 direction = desiredPosition - _startPosition;
                direction.Normalize();
                desiredPosition = _startPosition + (direction * _maxDragDistance);
            }

            if (desiredPosition.x > _startPosition.x)
                desiredPosition.x = _startPosition.x;

            _rigidbody2D.position = desiredPosition;
      }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        if (IsShot)
        {
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        IsShot = false;
        }
        foreach (var crate in _crates)
        {
            if (crate.destroyNextRestart)
            {
                crate.gameObject.SetActive(false);
            }
        }
    }
}
