using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 25f; // Speed of the player

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Vertical"); // A/D or Left/Right arrows
        float moveVertical = Input.GetAxis("Horizontal"); // W/S or Up/Down arrows
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
