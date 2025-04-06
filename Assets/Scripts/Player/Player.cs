using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _model;

    [SerializeField] float _speed = 4.0f;
    // [SerializeField] float _sensitivityHor = 100.0f;
    [SerializeField] float _rotationSpeed = 10.0f; // Скорость поворота к точке

    [SerializeField] float _movementRotationSpeed = 5.0f;

    private bool _isAlive = true;
    private bool _isWin = false;
    private Vector3 _targetDirection; // Направление, куда нужно повернуться

    private Vector3 _lastMovementDirection;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!_isAlive) return;

        // Проверяем клик мыши
        if (GameInput.Instance.TryGetMouseClickPoint(out Vector3 clickPoint))
        {
            // Вычисляем направление от игрока к точке клика
            _targetDirection = clickPoint - transform.position;
            _targetDirection.y = 0; // Игнорируем разницу по высоте
        }

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
    // private void Start()
    // {

    // }

    // private void FixedUpdate()
    // {
    //     if (!_isAlive) return;

    //     Vector3 inputVector = GameInput.Instance.GetMovementVector();

    //     if (inputVector.x != 0 || inputVector.z != 0)
    //     {
    //         transform.position += inputVector * (Time.fixedDeltaTime * _speed);
    //     }

    //     // Поворот к точке, если есть направление
    //     if (_targetDirection != Vector3.zero)
    //     {
    //         // Вычисляем поворот к направлению
    //         Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
    //         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
    //             _rotationSpeed * Time.fixedDeltaTime);

    //         // Если почти повернулись, сбрасываем направление
    //         if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
    //         {
    //             _targetDirection = Vector3.zero;
    //         }
    //     }
    // }
    private void FixedUpdate()
    {
        if (!_isAlive) return;

        // Получаем вектор движения
        Vector3 inputVector = GameInput.Instance.GetMovementVector();
        
        // Движение в глобальных координатах
        if (inputVector.magnitude > 0.1f) // Если есть ввод с клавиатуры
        {
            _animator.SetBool("IsWalk", true);

            // Запоминаем направление движения (для поворота)
            _lastMovementDirection = inputVector;
            
            // Движение
            transform.position += inputVector * (Time.fixedDeltaTime * _speed);
            
            // Плавный поворот в направлении движения (только если нет активного поворота по клику)
            if (_targetDirection == Vector3.zero)
            {
                RotateTowardsDirection(_lastMovementDirection, _movementRotationSpeed);
            }

            _model.transform.localPosition = new Vector3(0, 0, 0);
            _model.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else
        {
            _animator.SetBool("IsWalk", false);
        }

        // Поворот к точке по клику мыши (если есть)
        if (_targetDirection != Vector3.zero)
        {
            RotateTowardsDirection(_targetDirection, _rotationSpeed);

            // Сброс цели при достижении
            if (Quaternion.Angle(transform.rotation,
                Quaternion.LookRotation(_targetDirection)) < 1f)
            {
                _targetDirection = Vector3.zero;
            }
        }
    }

    // Новый метод для плавного поворота к направлению
    private void RotateTowardsDirection(Vector3 direction, float speed)
    {
        if (direction == Vector3.zero) return;
        
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            targetRotation, 
            speed * Time.fixedDeltaTime
        );
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
