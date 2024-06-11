using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Touch-Screen Joystick
    /// </summary>
    public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        enum JoystickMode
        {
            Fixed,          // Fixed at a certain position relative to parent
            Floating,       // Jumps to cursor/touch position everytime the screen is clicked
            FloatingHidden  // Same as floating, but invisible while the screen is not being pressed
        }

        [Header("Settings")]
        [SerializeField] private JoystickMode joystickMode;
        [SerializeField] private float knobRadius = 25f;

        [SerializeField] private float fadeDuration = 1f;   // Used for floating modes

        [Header("References")]        
        [SerializeField] private RectTransform joystickBase;
        [SerializeField] private RectTransform joystickKnob;

        private RectTransform rt;
        private CanvasGroup cg;

        [Header("Runtime")]
        [SerializeField] private bool inAction = false;
        [SerializeField] private bool fadingOut = false;
        [SerializeField] private float fadeOutTimer = 0f;


        public System.Action<Vector2> onDrag;

        /// <summary>
        /// Get the current touch position. If none are available attempt to fall back to mouse position
        /// </summary>
        /// <returns></returns>
        Vector2 GetCurrentTouchPosition()
        {
            if (Input.touchCount > 0)
                return Input.GetTouch(0).position;
            else
                return Input.mousePosition;
        }

        /// <summary>
        /// Initialize the joystick. TODO: Make sure joystick mode can be changed at runtime
        /// </summary>
        private void Start()
        {
            rt = GetComponent<RectTransform>();

            if (joystickMode == JoystickMode.FloatingHidden)
            {
                cg = gameObject.AddComponent<CanvasGroup>();
                cg.alpha = 0f;
            }
        }

        // Use update function in case the joystick is floating
        private void Update()
        {
            if (joystickMode == JoystickMode.Fixed)
                return;
            
            // Check if we begin dragging on the screen
            if (!inAction && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
            {
                inAction = true;
                rt.anchoredPosition = GetCurrentTouchPosition() - rt.sizeDelta / 2f;

                if (joystickMode == JoystickMode.FloatingHidden)
                {
                    fadingOut = false;
                    cg.alpha = 1f;
                }
            }
            else if(inAction)
            {
                if (Input.touchCount > 0 || Input.GetMouseButton(0))
                    HandleDrag(GetCurrentTouchPosition());
                else
                {
                    inAction = false;
                    onDrag?.Invoke(Vector2.zero);

                    if (joystickMode == JoystickMode.FloatingHidden)
                    {
                        fadingOut = true;
                        fadeOutTimer = fadeDuration;
                    }
                }
            }
            else if(fadingOut && joystickMode == JoystickMode.FloatingHidden)
            {
                fadeOutTimer -= Time.deltaTime;

                if (fadeOutTimer < 0f)
                    fadeOutTimer = 0f;

                cg.alpha = fadeOutTimer / fadeDuration;
            }
        }

        // Use inbuilt UI drag methods in case the joystick is fixed
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (joystickMode == JoystickMode.Fixed)
            {
                inAction = true;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (joystickMode == JoystickMode.Fixed)
            {
                HandleDrag(eventData.position);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (joystickMode == JoystickMode.Fixed)
            {
                inAction = false;
                onDrag?.Invoke(Vector2.zero);
            }
        }


        void HandleDrag(Vector2 touchPosition)
        {
            var knobPosition = touchPosition - (Vector2)joystickBase.position;

            if(knobPosition.magnitude > knobRadius)
                knobPosition = knobPosition.normalized * knobRadius;

            joystickKnob.anchoredPosition = knobPosition;

            onDrag?.Invoke(knobPosition.magnitude / knobRadius * knobPosition.normalized);
        }
    }
}