using UnityEngine;
using UnityEngine.UI; // Для работы с UI-элементами

public class StartCanvas : MonoBehaviour
{
    [SerializeField] private Button button;

    public void StartGame()
    {
        GameManager.Instance.FirstLevel();
        gameObject.SetActive(false);
    }   
}