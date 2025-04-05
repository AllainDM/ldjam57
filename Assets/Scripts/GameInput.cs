using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

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

    public Vector3 GetMovementVector()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(horizontal, 0, vertical);
        inputVector.Normalize();

        return inputVector;
    }

    // public float GetRotationY()
    // {
    //     return Input.GetAxis("Mouse X");
    // }

    // Новый метод для получения точки клика мыши
    public bool TryGetMouseClickPoint(out Vector3 point)
    {
        point = Vector3.zero;
        
        if (Input.GetMouseButtonDown(0)) // Только при нажатии, а не удержании
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                point = hit.point;
                return true;
            }
        }
        return false;
    }
}
