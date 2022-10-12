using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    [SerializeField] private float _delay;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private Cube _cubeTemplate;

    private WaitForSeconds _waitForSeconds;
    private float _defoultValue = 2;

    public void SetDelay(string userInput)
    {
        _delay = TryParce(userInput);
        _waitForSeconds = new WaitForSeconds(_delay);
    }

    public void SetSpeed(string userInput)
    {
        _speed = TryParce(userInput);
    }

    public void SetDistance(string userInput)
    {
        _distance = TryParce(userInput);
    }

    private void Start()
    {
        Initialize(_cubeTemplate.gameObject);
        var spawnCubeJob = StartCoroutine(SpawnCube());
    }

    private IEnumerator SpawnCube()
    {
        int maxDelay = 1000;
        _waitForSeconds = new WaitForSeconds(_delay);

        while (_delay < maxDelay)
        {
            if (TryGetObject(out GameObject cube))
            {
                SetCube(cube);
            }

            yield return _waitForSeconds;
        }
    }

    private void SetCube(GameObject cubeTemplate)
    {
        cubeTemplate.SetActive(true);
        cubeTemplate.transform.position = transform.position;

        Cube cube = cubeTemplate.GetComponent<Cube>();
        cube.Init(_speed, _distance);
    }

    private float TryParce(string userInput)
    {
        bool isParce = float.TryParse(userInput, out float parce);

        if (isParce && parce > 0)
            return parce;

        return _defoultValue;
    }
}
