using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float speed;
    public Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        try
        {
            gameObject.transform.position = player.transform.position;
        }
        catch
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "BCoin")
        {
            try
            {
                collision.transform.position = Vector2.MoveTowards(collision.transform.position, player.transform.position, speed * Time.deltaTime);
            }
            catch
            {

            }
        }
        else if (collision.tag == "SCoin")
        {
            try
            {
                collision.transform.position = Vector2.MoveTowards(collision.transform.position, player.transform.position, speed * Time.deltaTime);
            }
            catch
            {

            }
        }
        else if (collision.tag == "GCoin")
        {
            try
            {
                collision.transform.position = Vector2.MoveTowards(collision.transform.position, player.transform.position, speed * Time.deltaTime);
            }
            catch
            {

            }
        }
    }

}
