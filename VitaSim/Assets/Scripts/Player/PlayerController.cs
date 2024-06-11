using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// The central player class which handles movement and core gameflow elements
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float speed = 1f;

        [Header("References")]
        [SerializeField] private CharacterController characterController;

        [Header("Runtime Variables")]
        [SerializeField] Vector2 input;

        private void Update()
        {
            HandleMovement();
        }

        /// <summary>
        /// Applies movement input to the player
        /// </summary>
        private void HandleMovement()
        {
            if (input.magnitude > 0f)
            {
                var movement = new Vector3(input.x, 0f, input.y);               // Convert input to xz-vector
                characterController.Move(movement * speed * Time.deltaTime);    // Apply movement
                transform.forward = movement.normalized;                        // Rotate player in direction of movement
            }
        }

        /// <summary>
        /// Sets the current normalized input vector
        /// </summary>
        /// <param name="input"></param>
        public void SetInput(Vector2 input)
        {
            this.input = input.normalized;
        }

    }
}