using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddAndRemoveItems : MonoBehaviour
{
    // Virus Manager
    public List<Sprite> objectList = new List<Sprite>();
    public GameObject virus;
    public float spawnRate = 1f;
    public Vector3 virusScale = new Vector3(0.1f, 0.1f, 0.1f);

    // Score Manager
    [SerializeField] public TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        Debug.Log(objectList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Virus")
                {
                    score++;
                    scoreText.text = "Score: " + score;
                    GameObject temp = hit.transform.gameObject;
                    Destroy(temp);
                }
            } else
            {
                Touch myTouch = Input.GetTouch(0);
                MakeVirus(myTouch);
            }
        }

        if(score == 10)
        {
            GameStateManager.CurrentState = GameState.PickUpKit;
            SceneManager.LoadScene("Hospital");
        }
    }

    private void MakeVirus(Touch touchPos)
    {
        Vector3 objPos = Camera.main.ScreenToWorldPoint(touchPos.position);
        objPos.z = 1;
        virus.GetComponent<SpriteRenderer>().sprite = objectList[Random.Range(0, objectList.Count)];
        virus.transform.localScale = virusScale;
        virus.tag = "Virus";
        Instantiate(virus, objPos, Quaternion.identity);
    }
}
