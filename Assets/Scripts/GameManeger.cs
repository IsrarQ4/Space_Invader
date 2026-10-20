using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    private List<Level> currentLevels = new List<Level>();
    private int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevels.Add(new Level(Resources.Load<GameObject>("Chatgpt"), Resources.Load<GameObject>("Claude"),
            Resources.Load<GameObject>("Chatgpt"), Resources.Load<GameObject>("Claude"),
            Resources.Load<GameObject>("Chatgpt"), Resources.Load<GameObject>("Claude"),
             1f, 5f, 2f, 1, 0.5f));
        /*GameObject waveOneEnemyOne, GameObject waveOneEnemyTwo, GameObject waveTwoEnemyOne, GameObject waveTwoEnemyTwo,
        GameObject waveThreeEnemyOne, GameObject waveThreeEnemyTwo,
        
        float height, float width, float spawn_interval, int enemyMax, float enemyOneWeight*/
        currentLevels.Add(new Level(Resources.Load<GameObject>("Copilot"), Resources.Load<GameObject>("Deepseek"),
            Resources.Load<GameObject>("Copilot"), Resources.Load<GameObject>("Deepseek"),
            Resources.Load<GameObject>("Copilot"), Resources.Load<GameObject>("Deepseek"),
             5f, 5f, 2f, 10, 0.5f));
        currentLevels.Add(new Level(Resources.Load<GameObject>("Gemini"), Resources.Load<GameObject>("Grok"),
            Resources.Load<GameObject>("Gemini"), Resources.Load<GameObject>("Grok"),
            Resources.Load<GameObject>("Gemini"), Resources.Load<GameObject>("Grok"),
             5f, 5f, 2f, 10, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevels[index].IsDone() == true)
        {
            index++;
            if (index >= currentLevels.Count)
            {
                Debug.Log("All levels completed!");
            }
        }
        currentLevels[index].Update();
    }
}
