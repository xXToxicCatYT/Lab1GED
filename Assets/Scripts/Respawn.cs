using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float threshold;

    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            GameManager.Instance.AddDeathScore(1);
            transform.position = new Vector3(-23.28f, 0.62f, 23.87f);
        }
    }
}
