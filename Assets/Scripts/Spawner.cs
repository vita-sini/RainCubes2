using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private Transform[] _spawnPoints;

    public ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: Create,
        actionOnGet: ReceiveObject,
        actionOnRelease: (cube) => cube.gameObject.SetActive(false));
    }

    private void Start()
    {
        StartCoroutine(ExtractingElement());
    }

    public void ReturnItem(Cube cube)
    {
        _pool.Release(cube);
    }

    private Cube Create()
    {
        Cube cube = Instantiate(_cube);
        cube.Initialize(this);
        return cube;
    }

    private void ReceiveObject(Cube cube)
    {
        int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
        _spawnPoints[spawnPointNumber].transform.position = new Vector3(Random.Range(5.0f, 15.0f), Random.Range(5.0f, 15.0f), Random.Range(5.0f, 15.0f));
        cube.transform.position = _spawnPoints[spawnPointNumber].transform.position;
        cube.SetVelocity(Vector3.zero);
        cube.gameObject.SetActive(true);
        cube.RenewTimeOut();
        cube.SetStartColor();
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private IEnumerator ExtractingElement()
    {
        var waitForSeconds = new WaitForSeconds(_timeBetweenSpawn);

        while (enabled)
        {
            GetCube();

            yield return waitForSeconds;
        }
    }
}
