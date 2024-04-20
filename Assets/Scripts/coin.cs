using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collected");

        if (other.transform.tag == "Player")
        other.GetComponent<PlayerController>().score += 1;
        Destroy(gameObject);
    }
}
