using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    // Time interval between which the garbage collector is called.
    public float interval;

    // A time counter.
    float time;

    /*
    * Remove tetrominoes that are no longer used.
    */
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time > interval)
        {
            time = 0;
            foreach (GameObject tetromino in GameObject.FindGameObjectsWithTag("Tetromino"))
            {
                if (tetromino.transform.childCount == 0)
                {
                    Destroy(tetromino.transform.parent.gameObject);
                }
            }
        }
    }
}
