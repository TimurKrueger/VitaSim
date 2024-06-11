using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Relays input values to relevant objects
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Joystick playerJoystick;
        [SerializeField] private bool enableWASDMovement = false;
        [SerializeField] private PlayerController playerController;

        private void Start()
        {
            playerJoystick.onDrag += HandlePlayerMovement;
        }

        public void HandlePlayerMovement(Vector2 input)
        {
            playerController.SetInput(input);
        }
    }
}