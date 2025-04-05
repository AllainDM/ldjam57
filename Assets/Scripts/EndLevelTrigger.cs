using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.IsLastLevel())
        {
            GameManager.Instance.NextLevel();
        }
        else
        {
            GameManager.Instance.ShowWinPanel();
        }
    }
}
