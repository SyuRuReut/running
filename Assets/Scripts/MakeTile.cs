using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTile : MonoBehaviour
{
    public GameObject[] Spikes;
    public bool create = false;
    public float distance;
    public PlayManager play;
    // Start is called before the first frame update
    void Start()
    {
        play = FindObjectOfType<PlayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!play.pause)
        {
            if (gameObject.transform.position.x <= distance && !create)
            {
                GameObject a = Instantiate(Spikes[Random.Range(0, Spikes.Length)]);
                a.SetActive(true);
                create = true;
            }
        }
    }

}
