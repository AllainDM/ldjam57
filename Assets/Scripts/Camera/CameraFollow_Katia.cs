using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_Katia : MonoBehaviour
{
    public Transform target; // Игрок
    public Vector3 offset = new Vector3(-10f, 10f, 0f); // Смещение камеры относительно игрока

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
