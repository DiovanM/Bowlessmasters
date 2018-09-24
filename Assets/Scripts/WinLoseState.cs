using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseState : MonoBehaviour {

    [SerializeField]
    GameObject WinLoseCanvas, win, lose;

    private void OnEnable()
    {
        Time.timeScale = 1;
        Life.EnemyDied += Win;
        Life.PlayerDied += Lose;
    }

    void Win()
    {
        Time.timeScale = 0;
        WinLoseCanvas.SetActive(true);
        win.SetActive(true);
    }

    void Lose()
    {
        Time.timeScale = 0;
        WinLoseCanvas.SetActive(true);
        lose.SetActive(true);
    }

	public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
