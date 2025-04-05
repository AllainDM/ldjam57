using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject[] _levels;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _diePanel;
    [SerializeField] private GameObject _pausePanel;

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
        Unpause();
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

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }

    public void ShowWinPanel()
    {
        Pause();
        _winPanel.gameObject.SetActive(true);
    }

    public void ShowDiePanel()
    {
        _diePanel.gameObject.SetActive(true);
    }

    public void ShowPausePanel()
    {
        Pause();
        _pausePanel.gameObject.SetActive(true);
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
                Player.Instance.transform.Translate(0, 1, 0);
            }
            else
            {
                _levels[i].SetActive(false);
            }
        }
    }
}
