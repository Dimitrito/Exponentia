using UnityEngine;
using UnityEngine.InputSystem;

public class CubeManager : MonoBehaviour
{
    [Header("Cube Settings")]
    public float range = 3f;
    public float moveSpeed = 5f;
    public float throwForce = 10f;

    [Header("Techniacal")]
    public Transform spawnPoint;
    public Camera _cam;

    private GameObject currentCube;
    private AudioSource _audioSource;
    private bool isTouching = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        SpawnCube();
    }

    private void Update()
    {
        var touch = Touchscreen.current?.primaryTouch;

        if (touch == null || currentCube == null)
            return;
        
        // Touch begins
        if (touch.press.wasPressedThisFrame)
        {
            isTouching = true;
        }

        // Touch is held, move the cube horizontally under finger
        if (touch.press.isPressed && isTouching)
        {
            Vector2 screenPos = touch.position.ReadValue();

            Ray ray = _cam.ScreenPointToRay(screenPos);
            Plane plane = new Plane(Vector3.forward, currentCube.transform.position);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 worldPos = ray.GetPoint(distance);
                Vector3 pos = currentCube.transform.position;
                pos.x = Mathf.Clamp(worldPos.x, -range, range); // ограничение по границам
                currentCube.transform.position = pos;
            }
        }

        // Touch released, throw the cube forward
        if (touch.press.wasReleasedThisFrame && isTouching)
        {
            ThrowCube();
            isTouching = false;
        }
    }

    /// <summary>
    /// Spawns a new cube from the pool at the spawn point.
    /// Resets its Rigidbody to kinematic and zero velocities.
    /// </summary>
    private void SpawnCube()
    {
        currentCube = CubePool.Instance.GetCube(spawnPoint.position, spawnPoint.rotation);

        Rigidbody rb = currentCube.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    /// <summary>
    /// Throws the current cube forward using a force.
    /// Plays sound and schedules spawning of the next cube.
    /// </summary>
    private void ThrowCube()
    {
        if (currentCube == null) return;

        _audioSource?.Play();

        Rigidbody rb = currentCube.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(Vector3.forward * throwForce, ForceMode.Impulse);

        currentCube = null;
        Invoke(nameof(SpawnCube), 0.5f);
    }
}
