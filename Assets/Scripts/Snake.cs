using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    [SerializeField] private float moveDelay = 0.1f; 
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private FoodSpawner spawner;

    private Vector2 direction = Vector2.right;
    private List<Transform> segments = new List<Transform>(); //list of transforms
    private int score = 0;
    private float nextMoveTime = 0f;
    private bool isGameOver = false;

    void Awake()
    {
        segments.Add(transform); //Add this gameobject as first in list
      
    }

    void Update()
    {
        if (isGameOver)
            return;

        HandleInput();
        if (Time.time >= nextMoveTime) //check seconds from game start and move depends on delay
        {
            Move();
            CheckCollisions();
            nextMoveTime = Time.time + moveDelay;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down) direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) direction = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGameOver = true;
            string scene = SceneManager.GetActiveScene().name;
            if (scene == "SampleScene")
                SceneManager.LoadScene("Start");
            else if (scene == "Start")
                Application.Quit();
        }
    }

    private void Move()
    {
        for (int i = segments.Count - 1; i > 0; i--)
            segments[i].position = segments[i - 1].position; //moving each part of body to same position as previous body part

        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x, // Snake head move by 1, depends of its derection
            Mathf.Round(transform.position.y) + direction.y,
            0f
        );
    }

    private void Grow()
    {
        var segment = Instantiate(segmentPrefab).transform; // Create new body segment and set its position to the current
        segment.position = segments[segments.Count - 1].position; // position of the last segment (snake tail)
        segments.Add(segment);                                    
        score++;
        scoreText.text = "Score: " + score;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
            spawner.SpawnFood();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Wall"))
        {
            Gameover();
        }
    }

    private void CheckCollisions()
    {
        Vector3 headPos = segments[0].position;
        for (int i = 1; i < segments.Count; i++)
        {
            if (headPos == segments[i].position)
            {
                Gameover();
                return;
            }
        }
    }

    public void Gameover()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverText.gameObject.SetActive(true);
    }
}

