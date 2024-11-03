using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset from the player

    // Start is called before the first frame update
    void Start()
    {
        // Set an initial offset if needed
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Update the camera position to follow the player
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}
