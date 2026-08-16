using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int scoreValue = 50;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<Player>() == null) { return; }

        AudioSource.PlayClipAtPoint(coinPickUpSFX, otherCollider.transform.position);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
    }
}
