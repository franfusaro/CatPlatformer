using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] bool startMovingOnPlayerCollsion;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<Player>() == null) { return; }

        if (startMovingOnPlayerCollsion)
        {
            var movingPlatform = GetComponentInParent<MovingPlatform>();
            if (movingPlatform != null)
            {
                movingPlatform.move = true;
            }
        }

        otherCollider.gameObject.transform.parent = transform;
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.GetComponent<Player>() == null) { return; }

        otherCollider.gameObject.transform.parent = null;
    }
}
