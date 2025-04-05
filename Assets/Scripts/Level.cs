using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _startLevelPoint;

    public Vector3 GetStartLevelPosition()
    {
        return _startLevelPoint.position;
    }
}
