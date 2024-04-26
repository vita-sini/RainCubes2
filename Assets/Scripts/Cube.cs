using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private float _lifeTime = 5f;
    private Spawner _spawner;
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
        {
            _spawner.ReturnItem(this);
        }
    }

    public void Initialize(Spawner spawner)
    {
        _spawner = spawner;
    }

    public void SetColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        _renderer.material.color = randomColor;
    }

    public void SetStartColor()
    {
        _renderer.material.color = Color.white;
    }

    public void RenewTimeOut()
    {
        _lifeTime = 5f;
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }
}
