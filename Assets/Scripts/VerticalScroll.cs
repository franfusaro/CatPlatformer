using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] float scrollRate = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * scrollRate * Time.deltaTime);
    }
}
