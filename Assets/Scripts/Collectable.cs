using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    bool isCollected = false;

    private void ShowCoin()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
        this.isCollected = false;
    }

    private void HideCoin()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void CollectCoin()
    {
        isCollected = true;
        HideCoin();
        //Notificar al GameManager que hemos recogido una moneda
        GameManager.sharedInstance.CollectCoin();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            CollectCoin();
    }
}
