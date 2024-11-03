using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Import the TextMeshPro namespace

public class PickUp : MonoBehaviour
{
    public static int totalScore = 0; // Static variable to keep track of the score across all instances
    public static Stack<int> collectedLetters = new Stack<int>(); // Stack to store collected letters
    private static List<int> availableValues = new List<int>(); // List to store available values
    private static HashSet<int> spawnedLetters = new HashSet<int>(); // Track spawned letters

    // TextMeshPro references
    public TextMeshProUGUI scoreText; // Reference for displaying the score
    public TextMeshProUGUI lettersText; // Reference for displaying collected letters

    private void Start()
    {
        // Initialize the available values (1-5)
        for (int i = 1; i <= 5; i++)
        {
            availableValues.Add(i);
        }

        // Spawn letters if they haven't been spawned
        SpawnLetters();

        // Display initial state
        DisplayCurrentState();
    }

    private void SpawnLetters()
    {
        foreach (int value in availableValues)
        {
            if (!spawnedLetters.Contains(value))
            {
                GameObject letterPrefab = Resources.Load<GameObject>($"Letters/letter{value}");
                if (letterPrefab != null)
                {
                    Instantiate(letterPrefab, GetRandomPosition(), Quaternion.identity);
                    spawnedLetters.Add(value);
                }
                else
                {
                    Debug.LogError($"Letter prefab for letter{value} not found.");
                }
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-8f, 8f);
        float y = Random.Range(-4f, 4f);
        return new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player1"))
        {
            int collectedValue = -1;

            // Determine the collected value based on the letter's tag
            if (CompareTag("letter1"))
            {
                collectedValue = 1;
                totalScore += 10;
            }
            else if (CompareTag("letter2"))
            {
                collectedValue = 2;
                totalScore += 5;
            }
            else if (CompareTag("letter3"))
            {
                collectedValue = 3;
                totalScore += 5;
            }
            else if (CompareTag("letter4"))
            {
                collectedValue = 4;
                totalScore += 5;
            }
            else if (CompareTag("letter5"))
            {
                collectedValue = 5;
                totalScore += 5;
            }

            if (collectedValue != -1)
            {
                collectedLetters.Push(collectedValue);
                Debug.Log($"Letter {collectedValue} picked up! Score: {totalScore}");
                Destroy(gameObject); // Destroy the letter after pickup
            }

            if (collectedLetters.Count == 5)
            {
                ShuffleStack();
            }

            DisplayCurrentState();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("h1") || other.gameObject.CompareTag("h2") || 
            other.gameObject.CompareTag("h3") || other.gameObject.CompareTag("h4") || 
            other.gameObject.CompareTag("h5"))
        {
            Debug.LogWarning("Collided with the house");

            int houseValue = 0;
            if (other.gameObject.CompareTag("h1")) houseValue = 1;
            else if (other.gameObject.CompareTag("h2")) houseValue = 2;
            else if (other.gameObject.CompareTag("h3")) houseValue = 3;
            else if (other.gameObject.CompareTag("h4")) houseValue = 4;
            else if (other.gameObject.CompareTag("h5")) houseValue = 5;

            if (collectedLetters.Count > 0)
            {
                int lastCollectedLetter = collectedLetters.Peek();
                if (lastCollectedLetter == houseValue)
                {
                    totalScore += 15;
                    Debug.Log($"Delivered Letter {houseValue} to House {houseValue}! Score: {totalScore}");
                    collectedLetters.Pop();
                }
                else
                {
                    totalScore -= 8;
                    Debug.Log($"Wrong delivery! Delivered Letter {lastCollectedLetter} to House {houseValue}. Score: {totalScore}");
                }

                // Log the current state of collected letters after delivery
                Debug.Log("Collected letters shuffled: " + string.Join(", ", collectedLetters));
            }
            else
            {
                Debug.Log("No letters to deliver.");
            }

            if (totalScore <= -1)
            {
                Debug.Log("Your score is too low, so you need more practice! " + totalScore);
                collectedLetters.Clear();
                SceneManager.LoadScene(0);
            }

            DisplayCurrentState();
        }
    }

    private void ShuffleStack()
    {
        List<int> letterList = new List<int>(collectedLetters);
        for (int i = letterList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = letterList[i];
            letterList[i] = letterList[j];
            letterList[j] = temp;
        }

        collectedLetters.Clear();
        foreach (int letter in letterList)
        {
            collectedLetters.Push(letter);
        }

        // Create a list of house values (1 to 5)
        List<int> houseValues = new List<int> { 1, 2, 3, 4, 5 };

        // Print shuffled letters and house values
        
        Debug.Log("Available house values: " + string.Join(", ", houseValues));
        Debug.Log("Collected letters shuffled: " + string.Join(", ", collectedLetters));
    }

   private void DisplayCurrentState()
{
    
}

}
