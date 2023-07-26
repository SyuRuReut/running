using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundMove : MonoBehaviour
{
    public GameObject tiles;
    public float speed;
    public PlayManager play;
    //public GameObject returnTile;
    //public GameObject respawnTile;
    // Start is called before the first frame update
    void Start()
    {
        play = FindObjectOfType<PlayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!play.pause)
            gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
        //tiles.GetComponent<TilemapRenderer>().transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
        /*
        if (tiles.transform.position.x < returnTile.transform.position.x)
        {
            Debug.Log(respawnTile.transform.position.x);
            tiles.GetComponent<Tilemap>().transform.position = new Vector3(respawnTile.transform.position.x, 0, 0);
            //tiles.GetComponent<TilemapRenderer>().transform.position.Set(respawnTile.transform.position.x, tiles.transform.position.y, tiles.transform.position.z);
            
        }
        else
        {
            tiles.GetComponent<Tilemap>().transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
        }*/
    }

     void FixedUpdate()
    {
       
    }

}
