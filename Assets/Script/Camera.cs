using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
    }
}
