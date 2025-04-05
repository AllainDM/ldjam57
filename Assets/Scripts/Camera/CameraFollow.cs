using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _cameraOffsetX = 0.0f;
    [SerializeField] private float _cameraOffsetY = 10.0f;
    [SerializeField] private float _cameraOffsetZ = -10.0f;

    [SerializeField] private GameObject _targetObject;

    private void Start()
    {
        //_targetObject = Player.Instance.gameObject;
    }

    private void LateUpdate()
    {
        if (_targetObject != null)
        {
            transform.position = new Vector3(
                _targetObject.transform.position.x + _cameraOffsetX,
                _targetObject.transform.position.y + _cameraOffsetY,
                _targetObject.transform.position.z + _cameraOffsetZ
                );
        }
    }
}
