using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Virus : MonoBehaviour
{
    public float speed = 0.5f;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < -Camera.main.orthographicSize - 1)
        {
            Destroy(gameObject);
        }

        DetectTouch();
    }

    void DetectTouch()
    {
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("DetectTouch");
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 touchPosition2D = new Vector2(touchPosition.x, touchPosition.y);

                RaycastHit2D hit = Physics2D.Raycast(touchPosition2D, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Destroyed Virus");
                    DestroyVirus();
                }
            }
        }*/

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        DestroyVirus();
                    }
                }
            }
        }
    }

    void DestroyVirus()
    {
        if (scoreManager != null)
        {
            scoreManager.IncrementScore();
        }
        Destroy(gameObject);
    }
}