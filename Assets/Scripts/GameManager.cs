using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject[] _levels;

    private int _curLevel = 0;

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
        FirstLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (IsLastLevel())
            {
                FirstLevel();
            }
            else
            {
                NextLevel();
            }
        }
    }

    public void FirstLevel()
    {
        _curLevel = 0;
        UpdateLevel(_curLevel);
    }

    public void NextLevel()
    {
        _curLevel++;
        UpdateLevel(_curLevel);
    }

    public void LoadLevel(int level)
    {
        _curLevel = level;
        UpdateLevel(_curLevel);
    }

    public bool IsLastLevel()
    {
        return _curLevel == _levels.Length - 1;
    }

    private void UpdateLevel(int curLevel)
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            if (i == curLevel)
            {
                _levels[i].SetActive(true);
                Vector3 startPoint = _levels[i].GetComponent<Level>().GetStartLevelPosition();
                Player.Instance.transform.position = startPoint;
            }
            else
            {
                _levels[i].SetActive(false);
            }
        }
    }
}
