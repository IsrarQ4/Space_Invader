using System;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManeger : MonoBehaviour
{
    private List<Level> currentLevels = new List<Level>();
    private int index = 0;
    [SerializeField] private Player _player;
    private const int WIN_SCORE = 3945;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevels.Add(new Level(Resources.Load<GameObject>("Chatgpt"), Resources.Load<GameObject>("Claude"),
            Resources.Load<GameObject>("Chatgpt"), Resources.Load<GameObject>("Claude"),
            Resources.Load<GameObject>("Chatgpt"), Resources.Load<GameObject>("Claude"),
             3f, 8f, 1.5f, 3, 0.5f));
        /*GameObject waveOneEnemyOne, GameObject waveOneEnemyTwo, GameObject waveTwoEnemyOne, GameObject waveTwoEnemyTwo,
        GameObject waveThreeEnemyOne, GameObject waveThreeEnemyTwo,
        
        float height, float width, float spawn_interval, int enemyMax, float enemyOneWeight*/
        currentLevels.Add(new Level(Resources.Load<GameObject>("Copilot"), Resources.Load<GameObject>("Deepseek"),
            Resources.Load<GameObject>("Copilot"), Resources.Load<GameObject>("Deepseek"),
            Resources.Load<GameObject>("Copilot"), Resources.Load<GameObject>("Deepseek"),
             2f, 5f, 1f, 5, 0.5f));
        currentLevels.Add(new Level(Resources.Load<GameObject>("Gemini"), Resources.Load<GameObject>("Grok"),
            Resources.Load<GameObject>("Gemini"), Resources.Load<GameObject>("Grok"),
            Resources.Load<GameObject>("Gemini"), Resources.Load<GameObject>("Grok"),
             1f, 3f, 0.5f, 7, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsShipSelected() == false)
        {
            return;
        }

        // win once the score reaches the maximum
        if (ScoreManager.Instance.GetScore() >= WIN_SCORE)
        {
            SceneManager.LoadScene("Win");
            return;
        }

        // all levels have finished spawning: wait for the player to defeat the rest
        if (index >= currentLevels.Count)
        {
            return;
        }

        if (currentLevels[index].IsDone() == true)
        {
            ScoreManager.Instance.AddScore(1000);   
            index++;
        }
        else
        {
            currentLevels[index].Update();
        }
    }
}
