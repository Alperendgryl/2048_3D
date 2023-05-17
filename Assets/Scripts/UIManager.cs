using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("In Game")]
    [SerializeField] private TMP_Text currentScore;
    [SerializeField] private TMP_Text bestScore;

    [Header("End Game")]
    [SerializeField] private TMP_Text currentScoreE;
    [SerializeField] private TMP_Text bestScoreE;

    [Header("GameObject")]
    [SerializeField] private GameObject endPanel;

    void Start()
    {
        endPanel.SetActive(false);
    }

    void Update()
    {
        if (CubeCollision.currentScore > PlayerPrefs.GetInt("HighestScore"))
        {
            PlayerPrefs.SetInt("HighestScore", CubeCollision.currentScore);
        }

        bestScore.text = "Best: " + PlayerPrefs.GetInt("HighestScore").ToString();
        currentScore.text = CubeCollision.currentScore.ToString();

        if (RedZone.gameEnded)
        {
            StartCoroutine(gameEnded());
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        CubeCollision.currentScore = 0;
    }

    IEnumerator gameEnded()
    {
        RedZone.gameEnded = false;
        yield return new WaitForSeconds(1.5f);
        endPanel.SetActive(true);
        CubeCollision.currentScore = 0;
    }
}
