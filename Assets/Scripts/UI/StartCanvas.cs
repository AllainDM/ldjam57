using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.FirstLevel();
        gameObject.SetActive(false);
    }   
}