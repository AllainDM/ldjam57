using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] float _speed = 4.0f;

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
        Vector3 inputVector = GameInput.Instance.GetMovementVector();

        if (inputVector.x != 0 || inputVector.z != 0)
        {
            transform.Translate(inputVector * (Time.fixedDeltaTime * _speed));
        }
    }
}
