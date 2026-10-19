using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    public Player player;
    public Slider bar;
    public TMP_Text healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float health = player.GetPlayerHealth();

        bar.value = health;
        healthText.text = "Health: " + health.ToString();


    }
}
