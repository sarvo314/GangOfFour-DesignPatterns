using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnvironment : MonoBehaviour
{
    public static GameEnvironment instance;
    private List<GameObject> obstacles = new List<GameObject>();
    public List<GameObject> Obstacles { get { return obstacles; } }

    public static GameEnvironment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnvironment();
            }
            return instance;
        }
    }
    public void AddObstacles(GameObject go)
    {
        obstacles.Add(go);
    }
    public void RemoveObstacles(GameObject go)
    {
        int index = obstacles.IndexOf(go);
        obstacles.RemoveAt(index);
    }
}
