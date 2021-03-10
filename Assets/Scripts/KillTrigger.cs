using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si nuestra zona de muerte colisiona con un objec que tenga la etiqueta 'Player' se llamara al metodo KillPlayer para que activemos la animacion de Morir
        if (collision.tag == "Player")
        {
            PlayerController.sharedInstance.KillPlayer();
        }
    }
}
