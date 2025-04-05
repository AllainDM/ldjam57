using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPause : MonoBehaviour
{
    public void UnpauseGame()
    {
        GameManager.Instance.Unpause();
        gameObject.SetActive(false);
    }   
}
