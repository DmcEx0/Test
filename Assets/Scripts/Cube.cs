using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;

    private float _speed;
    private float _distance;

    private MeshRenderer _meshRenderer;
    private Color _color;

    public void Init(float speed, float distance)
    {
        _speed = speed;
        _distance = distance;
    }

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _color = _meshRenderer.material.color;
    }

    private void Update()
    {
        int value = 5;
        int minimalColorAlpha = 0;
        Color color = _meshRenderer.material.color;

        if (Vector3.Distance(transform.position, GetTargetPosition()) <= 0)
        {
            ChangeColorAlpha(color ,value);

            if (color.a <= minimalColorAlpha)
            {
                _meshRenderer.material.color = _color;
                gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetTargetPosition(), _speed * Time.fixedDeltaTime);
    }

    private Vector3 GetTargetPosition()
    {
        return _direction.normalized * _distance;
    }

    private void ChangeColorAlpha(Color color, int value)
    {
        color.a -= value * Time.deltaTime;
        _meshRenderer.material.color = color;
    }
}
