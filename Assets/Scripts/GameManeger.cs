using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    private List<Level> currentLevels = new List<Level>();
    private int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevels.Add(new Level(Resources.Load<GameObject>("EnemyPrefabOne"), Resources.Load<GameObject>("EnemyPrefabTwo"),
            Resources.Load<GameObject>("EnemyPrefabThree"), Resources.Load<GameObject>("EnemyPrefabFour"),
            Resources.Load<GameObject>("EnemyPrefabFive"), Resources.Load<GameObject>("EnemyPrefabSix"),
            1f, 1f, 5f, 5f, 2f, 10, 0.5f));
        currentLevels.Add(new Level(Resources.Load<GameObject>("EnemyPrefabOne"), Resources.Load<GameObject>("EnemyPrefabTwo"),
            Resources.Load<GameObject>("EnemyPrefabThree"), Resources.Load<GameObject>("EnemyPrefabFour"),
            Resources.Load<GameObject>("EnemyPrefabFive"), Resources.Load<GameObject>("EnemyPrefabSix"),
            1f, 1f, 5f, 5f, 2f, 10, 0.5f));
        currentLevels.Add(new Level(Resources.Load<GameObject>("EnemyPrefabOne"), Resources.Load<GameObject>("EnemyPrefabTwo"),
            Resources.Load<GameObject>("EnemyPrefabThree"), Resources.Load<GameObject>("EnemyPrefabFour"),
            Resources.Load<GameObject>("EnemyPrefabFive"), Resources.Load<GameObject>("EnemyPrefabSix"),
            1f, 1f, 5f, 5f, 2f, 10, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevels[index].IsDone())
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
