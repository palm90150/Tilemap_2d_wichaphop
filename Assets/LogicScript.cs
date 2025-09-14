using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

public class LogicScript : MonoBehaviour
{
    [Header("UI")]
    public Text hpText;
    public GameObject gameOverUI;   // <- Drag Game Over (parent) มาวาง
    public Button playButton;       // <- Drag ปุ่ม Play มาวาง

    [Header("Player HP")]
    public float playerHP = 100f;

    void Start()
    {
        // ซ่อน GameOver ตอนเริ่ม
        gameOverUI.SetActive(false);

        // ผูกปุ่ม Play ให้รีโหลดฉาก
        playButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        // ตัวอย่าง: ถ้า HP <= 0 ให้ Game Over
        if (playerHP <= 0 && !gameOverUI.activeSelf)
        {
            GameOver();
        }
    }

    public void UpudatePlayerHP(float hp)
    {
        playerHP = hp;
        hpText.text = "HP: " + math.round(hp).ToString();

        if (playerHP <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);   // แสดง Game Over + ปุ่ม
        Time.timeScale = 0f;          // หยุดเวลาเกม (optional)
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  // คืนค่าเวลา
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

