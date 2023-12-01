using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWorldPrefab : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float width;
    [SerializeField]
    private float length;
    [SerializeField]
    private GameObject cube;

    void Start()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                float height = Mathf.PerlinNoise(i * 0.3f, j * 0.2f) * 3;
                Vector3 pos = new Vector3(i, height, j);
                GameObject go = Instantiate(cube, pos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
