using System.Collections;
using UnityEngine;

public class ExplosiveBall : MonoBehaviour
{
    private Vector3 _startScale;
    private Vector3 _endScale;
    private float _duration;
    private float _speed;
    private int _damage;

    private Vector3 _currentScale;
    private float _currentTime;

    private bool _isExplosive = false;

    private void Update()
    {
        if (_isExplosive)
        {
            return;
        }

        _currentScale = Vector3.MoveTowards(_currentScale, _endScale, _speed * Time.deltaTime);
        transform.localScale = _currentScale;

        _currentTime += Time.deltaTime;

        if (_currentTime >= _duration)
        {
            _isExplosive = true;
            GetComponent<SphereCollider>().enabled = true;
            //GetComponent<Ma>
            StartCoroutine(Explosion());
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

    public void Reset(Vector3 startScale, Vector3 endScale, float duration, float speed, int damage)
    {
        _isExplosive = false;

        _startScale = startScale;
        _endScale = endScale;
        _duration = duration;
        _speed = speed;
        _damage = damage;

        _currentScale = _startScale;
        _currentTime = 0;

        GetComponent<SphereCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(_damage);
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }
}
