using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _startLevelPoint;
    [SerializeField] private Transform _endLevelPoint;

    public Vector3 GetStartLevelPosition()
    {
        return _startLevelPoint.position;
    }
}
