using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour
{
    public float mergeImpulseThreshold = 2f;  // Minimum impulse required to merge with another cube

    private TextMeshPro[] _numberTexts; // Text components to display the cube's number
    private Renderer _renderer; // Renderer to change cube color
    private int _level; // Current cube level
    private bool _hasBeenActivated = false; // Flag that shows whether the cube was launched into the arena.

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _numberTexts = GetComponentsInChildren<TextMeshPro>();
        
        Init();
    }

    // Called when colliding with another cube
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            Vector3 impulse = collision.impulse;
            ContactPoint contact = collision.contacts[0];
            float directedImpact = Vector3.Dot(impulse, -contact.normal);

            if (directedImpact >= mergeImpulseThreshold)
            {
                if (collision.gameObject.GetComponent<CubeController>().TryMerge(_level))
                    CubePool.Instance.ReturnCube(gameObject);
            }
        }
    }

    // Called when cubes are close together and an impulse has affected them
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            float impact = collision.relativeVelocity.magnitude;
            if (impact >= mergeImpulseThreshold)
            {
                if (collision.gameObject.GetComponent<CubeController>().TryMerge(_level))
                    CubePool.Instance.ReturnCube(gameObject);
            }
        }
    }

    /// <summary>
    /// Attempts to merge this cube with another cube of the same level.
    /// </summary>
    /// <param name="level">The level of the other cube</param>
    /// <returns>True if merge succeeded, false otherwise</returns>
    public bool TryMerge(int level)
    {
        if (_level == level)
        {
            _level *= 2;
            UpdateText();
            UpdateColor();

            return true;
        }
        else 
            return false;
    }

    /// <summary>
    /// Checks if the cube has crossed the starting border.
    /// Marks the cube as activated on first call.
    /// </summary>
    /// <returns>True if already crossed before, false if first time</returns>
    public bool IsCrossedBorder()
    {
        if (_hasBeenActivated)
        {
            return true;
        }
        else 
        {
            _hasBeenActivated = true;
            return false;
        }
    }

    /// <summary>
    /// Returns the current cube level.
    /// </summary>
    /// <returns>Cube level</returns>
    public int GetLevel() 
    {
        return _level;
    }

    /// <summary>
    /// Initializes the cube with a random starting value (2 or 4)
    /// and resets its activation state.
    /// </summary>
    public void Init()
    {
        _hasBeenActivated = false;

        float rand = Random.value;
        _level = (rand < 0.75f) ? 2 : 4; // 75% chance for 2, 25% for 4

        UpdateText();
        UpdateColor();
    }

    // Update the displayed text to match the cube's level
    private void UpdateText()
    {
        foreach (var t in _numberTexts)
        {
            if (t != null)
                t.text = _level.ToString();
        }
    }

    // Update the cube's color based on its level
    private void UpdateColor()
    {
        float hue = Mathf.Log(_level, 2) * 0.1f % 1f;
        _renderer.material.color = Color.HSVToRGB(hue, 0.9f, 0.9f);
    }
}