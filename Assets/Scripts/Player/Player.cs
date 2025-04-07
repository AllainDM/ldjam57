using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Реализация синглтона - делает объект игрока доступным из любого скрипта
    public static Player Instance { get; private set; }

    // Настройки движения (видны в инспекторе Unity)
    [SerializeField] private float _speed = 5f; // Скорость перемещения
    [SerializeField] private float _rotationSpeed = 10f; // Скорость поворота
    
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _model;
    // [SerializeField] private Vector3 initialPosition;

    // Компоненты и приватные переменные
    private CharacterController _characterController; // Компонент для физического перемещения
    private Vector3 _moveDirection; // Текущее направление движения
    private bool _isAlive = true; // Флаг жизненного состояния
    private bool _isWin = false;


    private void Awake()
    {
        // Инициализация синглтона
        if (Instance == null) Instance = this;
        else Destroy(gameObject); // Уничтожаем дубликаты
        
        // Получаем компонент CharacterController автоматически
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // transform.position.y = initialPosition.y;
        // transform.position.y = initialPosition.y;
        if (!_isAlive) return; // Если игрок мертв - не обрабатываем движение
        
        HandleMovement(); // Обрабатываем движение каждый кадр


        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!GameManager.Instance.IsPausePanelActive())
            {
                GameManager.Instance.ShowPausePanel();
            }
            else
            {
                GameManager.Instance.HidePausePanel();
            }
        }

    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // Используем GetAxisRaw для мгновенного нуля
        float vertical = Input.GetAxisRaw("Vertical");
        
        // Получаем направление относительно камеры
        Vector3 forward = CameraFollow.Instance.transform.forward;
        Vector3 right = CameraFollow.Instance.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        
        _moveDirection = (forward * vertical + right * horizontal).normalized;

        // Добавляем проверку на минимальный ввод
        bool hasInput = Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f;
        
        if (hasInput)
        {

            // ПОВОРОТ ПЕРСОНАЖА:
            // Создаем кватернион поворота в направлении движения
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            // Плавно интерполируем текущий поворот к целевому
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
                _rotationSpeed * Time.deltaTime);

            // Мгновенный поворот без интерполяции
            transform.rotation = Quaternion.LookRotation(_moveDirection);
            
            _characterController.Move(_moveDirection * (_speed * Time.deltaTime));
            _animator.SetBool("IsWalk", true);

        }
        else 
        {
            // Полный сброс состояния
            _animator.SetBool("IsWalk", false);
            _moveDirection = Vector3.zero;
            
            // Важно: принудительно останавливаем возможное вращение
            transform.rotation = transform.rotation; // Фиксируем текущее вращение
            
        }
        // _model.transform.localPosition = new Vector3(_model.transform.localPosition.x, -1, _model.transform.localPosition.z);

        _model.transform.localPosition = new Vector3(0, -1, 0);
        _model.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    public void Teleport(Vector3 newPosition)
{
    // Вариант 1: Move() (лучший способ)
    Vector3 offset = newPosition - transform.position;
    _characterController.Move(offset); // Перемещает с учетом коллайдеров
    
    // Вариант 2: SimpleMove() (если не нужно учитывать Y-ось)
    _characterController.SimpleMove(Vector3.zero); // Сбросить скорость
    transform.position = newPosition; // Затем установить позицию
    
    // Сбросить направление движения, чтобы игрок не двигался по инерции
    _moveDirection = Vector3.zero;
}

    public void Die()
    {
        _isAlive = false;
        _animator.SetBool("IsAlive", false);
        GameManager.Instance.ShowDiePanel();
    }

    public void Resurrect()
    {
        _isAlive = true;
    }

    public void Win()
    {
        _isWin = true;
        _animator.SetBool("IsWin", _isWin);
        StartCoroutine(EndDance());
    }

    IEnumerator EndDance()
    {
        yield return new WaitForSeconds(10.0f);
        _isWin = false;
        _animator.SetBool("IsWin", _isWin);
        GameManager.Instance.ShowWinPanel();
        //GameManager.Instance.Pause();
    }
}
