using UnityEngine;

public class ExplosiveBall : MonoBehaviour
{
    private Vector3 _startScale;
    private Vector3 _endScale;
    private float _duration;
    private float _speed;

    private Vector3 _currentScale;
    private float _currentTime;

    private void Update()
    {
        _currentScale = Vector3.MoveTowards(_currentScale, _endScale, _speed * Time.deltaTime);
        transform.localScale = _currentScale;

        _currentTime += Time.deltaTime;

        if (_currentTime >= _duration)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetStartScale(Vector3 currentScale)
    {
        _currentScale = currentScale;
    }

    public void SetEndScale(Vector3 endScale)
    {
        _endScale = endScale;
    }

    public void SetDuration(float duration)
    {
        _duration = duration;
    }

    public void Reset(Vector3 startScale, Vector3 endScale, float duration, float speed)
    {
        _startScale = startScale;
        _endScale = endScale;
        _duration = duration;
        _speed = speed;

        _currentScale = _startScale;
        _currentTime = 0;
    }
}
