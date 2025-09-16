using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the detection of cubes crossing a line in the game.
/// </summary>
public class LineManager : MonoBehaviour
{
    public Score score;

    // Checks if the cube has already crossed the border and updates score or ends the game.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube")) 
        {
            CubeController cube = other.gameObject.GetComponent<CubeController>();
            // If cube crosses for the first time, add score
            if (!cube.IsCrossedBorder())
                score.AddScore(cube.GetLevel());
            else
                SceneManager.LoadScene("GameOver"); // Already crossed, end the game
        }
    }
}