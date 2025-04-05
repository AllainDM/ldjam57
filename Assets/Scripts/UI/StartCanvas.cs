using UnityEngine;
using UnityEngine.UI; // ��� ������ � UI-����������

public class StartCanvas : MonoBehaviour
{
    [SerializeField] private Button button;

    public void StartGame()
    {
        GameManager.Instance.FirstLevel();
        gameObject.SetActive(false);
    }   
}