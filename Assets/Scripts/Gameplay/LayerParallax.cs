using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayerParallax : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.1f;
    [SerializeField] float offSet = 0f;
    [SerializeField] float smoothing = 5f;
    [SerializeField] GameObject viewTarget;

    Vector3 prevCamPos;

    // Start is called before the first frame update
    void Start()
    {
        prevCamPos = viewTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaXPos = (prevCamPos.x - viewTarget.transform.position.x) * (scrollSpeed + offSet);
        Vector3 backgroundTargetPos = new Vector3(transform.position.x + deltaXPos, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, backgroundTargetPos, smoothing * Time.deltaTime);
        prevCamPos = viewTarget.transform.position;
    }
}
