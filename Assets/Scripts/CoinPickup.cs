using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int scoreValue = 50;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider is CapsuleCollider2D)
        {
            AudioSource.PlayClipAtPoint(coinPickUpSFX, FindObjectOfType<Player>().transform.position);
            FindObjectOfType<GameSession>().AddToScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
