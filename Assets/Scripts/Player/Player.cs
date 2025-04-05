using Unity.VisualScripting;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] float _speed = 4.0f;
    [SerializeField] float _sensitivityHor = 100.0f;

    private bool _isAlive = true;

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

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (!_isAlive)
        {
            return;
        }

        Vector3 inputVector = GameInput.Instance.GetMovementVector();

        if (inputVector.x != 0 || inputVector.z != 0)
        {
            transform.Translate(inputVector * (Time.fixedDeltaTime * _speed));
        }

        if (Input.GetMouseButton(0))
        {
            float rotationY = GameInput.Instance.GetRotationY();
            transform.Rotate(0, rotationY * _sensitivityHor * Time.fixedDeltaTime, 0);
        }
    }

    public void Die()
    {
        _isAlive = false;
    }

    public void Resurrect()
    {
        _isAlive = true;
    }
}
