using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Синглтон для доступа к камере из других скриптов
    public static CameraFollow Instance { get; private set; }
    
    // Настройки камеры (видны в инспекторе)
    [Header("Settings")]
    public Transform target; // Цель (объект игрока)
    public float distance = 5f; // Дистанция от игрока
    public float height = 2f; // Высота камеры
    public float rotationSpeed = 2f; // Чувствительность вращения
    public float minVerticalAngle = -30f; // Минимальный угол наклона
    public float maxVerticalAngle = 60f; // Максимальный угол наклона
    public float smoothTime = 0.3f; // Время сглаживания движения
    
    // Приватные переменные состояния
    private float _currentRotationX; // Текущий горизонтальный угол
    private float _currentRotationY; // Текущий вертикальный угол
    private Vector3 _smoothVelocity; // Для плавного перемещения

    private void Awake()
    {
        // Инициализация синглтона
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // LateUpdate вызывается после Update - идеально для камеры
    private void LateUpdate()
    {
        if (target == null) return; // Если нет цели - выходим
        
        HandleCameraRotation(); // Обрабатываем вращение
        UpdateCameraPosition(); // Обновляем позицию
    }

    private void HandleCameraRotation()
    {
        // Вращение только при зажатой ПРАВОЙ кнопке мыши
        if (Input.GetMouseButton(1))
        {
            // Изменяем углы на основе движения мыши
            _currentRotationX += Input.GetAxis("Mouse X") * rotationSpeed;
            _currentRotationY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            // Ограничиваем вертикальный угол
            _currentRotationY = Mathf.Clamp(_currentRotationY, minVerticalAngle, maxVerticalAngle);
        }
    }

    // private void UpdateCameraPosition()
    // {
    //     // 1. ВЫЧИСЛЯЕМ ПОВОРОТ КАМЕРЫ
    //     // Создаем кватернион из текущих углов Эйлера
    //     Quaternion rotation = Quaternion.Euler(_currentRotationY, _currentRotationX, 0);
        
    //     // 2. ВЫЧИСЛЯЕМ ПОЗИЦИЮ КАМЕРЫ
    //     Vector3 targetPosition = target.position; // Позиция игрока
    //     // Рассчитываем желаемую позицию с учетом поворота и смещения
    //     Vector3 desiredPosition = targetPosition + rotation * new Vector3(0, height, -distance);
        
    //     // 3. ПЛАВНОЕ ПЕРЕМЕЩЕНИЕ
    //     // SmoothDamp обеспечивает плавное движение с замедлением
    //     transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, 
    //         ref _smoothVelocity, smoothTime);
        
    //     // 4. НАПРАВЛЯЕМ КАМЕРУ НА ИГРОКА
    //     // Смотрим на точку чуть выше основания игрока (для лучшего обзора)
    //     transform.LookAt(targetPosition + Vector3.up * height * 0.5f);
    // }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(_currentRotationY, _currentRotationX, 0);
        Vector3 targetPosition = target.position;
        
        // Рассчитываем смещение камеры относительно игрока
        Vector3 offset = rotation * new Vector3(0, height, -distance);
        
        // Объявляем desiredPosition здесь, чтобы она была доступна во всей функции
        Vector3 desiredPosition;
        
        // Проверяем коллизии камеры
        RaycastHit hit;
        if (Physics.Raycast(targetPosition, offset.normalized, out hit, offset.magnitude))
        {
            // Если есть препятствие, приближаем камеру
            desiredPosition = hit.point - offset.normalized * 0.2f; // небольшой отступ
        }
        else
        {
            desiredPosition = targetPosition + offset;
        }
        
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, 
            ref _smoothVelocity, smoothTime);
        
        // Сглаживаем поворот камеры
        Quaternion lookRotation = Quaternion.LookRotation(targetPosition + Vector3.up * height * 0.5f - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, smoothTime * Time.deltaTime * 10f);
    }
}
// public class CameraFollow : MonoBehaviour
// {
//     [SerializeField] private float _cameraOffsetX = 13.0f;
//     [SerializeField] private float _cameraOffsetY = 18.0f;
//     [SerializeField] private float _cameraOffsetZ = -14.0f;

//     [SerializeField] private GameObject _targetObject;

//     private void Start()
//     {
//         //_targetObject = Player.Instance.gameObject;
//     }

//     private void LateUpdate()
//     {
//         if (_targetObject != null)
//         {
//             transform.position = new Vector3(
//                 _targetObject.transform.position.x + _cameraOffsetX,
//                 _targetObject.transform.position.y + _cameraOffsetY,
//                 _targetObject.transform.position.z + _cameraOffsetZ
//                 );
//         }
//     }
// }
