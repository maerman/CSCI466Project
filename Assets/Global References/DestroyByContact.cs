using UnityEngine;
using System.Collections;
using static GameStates;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            //change state to game over
            gameState = GameState.LostGame;
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
