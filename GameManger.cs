using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerHP = 3;
    public int kills = 0;

    public Text hpText;
    public Text killsText;
    public GameObject gameOverScreen;

    void Awake() { instance = this; }

    void Start()
    {
        UpdateUI();
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    public void PlayerTakeDamage()
    {
        playerHP--;
        UpdateUI();
        if (playerHP <= 0) GameOver();
    }

    public void EnemyDied()
    {
        kills++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (hpText != null) hpText.text = "HP: " + playerHP;
        if (killsText != null) killsText.text = "Kills: " + kills;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}