
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    #region Enums
    public enum BallColor { Blue, Red, Yellow }
    #endregion

    #region Variables
    [Header("Effects")]
    [SerializeField] private GameObject _clickVFX;
    [SerializeField] private GameObject _destroyVFX;

    [Header("Ball Properties")]
    [SerializeField] private BallColor _currentColor;
    private BallColor _originalColor;
    private int _groundHitCount = 0;
    private Renderer _renderer;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        SetRandomColor();
    }

    /// <summary>
    /// This method now handles the click detection using a more reliable raycast approach.
    /// </summary>
    private void Update()
    {
        // Check for left mouse button down
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit this specific ball object
                if (hit.transform == transform)
                {
                    HandleClick();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _groundHitCount++;
            if (_groundHitCount >= 3)
            {
                GameManager.Instance.AddScore(-10);

                // Play destroy visual effect if assigned
                if (_destroyVFX != null)
                {
                    Instantiate(_destroyVFX, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
            else
            {
                ChangeColorOnHit();
            }
        }
    }
    #endregion

    #region Private Methods

    /// <summary>
    /// Contains the logic that was previously in OnMouseDown.
    /// </summary>
    private void HandleClick()
    {
        int points = 0;
        switch (_currentColor)
        {
            case BallColor.Blue:
                points = 8;
                break;
            case BallColor.Red:
                points = 5;
                break;
            case BallColor.Yellow:
                points = 3;
                break;
        }
        GameManager.Instance.AddScore(points);

        // Play click visual effect if assigned
        if (_clickVFX != null)
        {
            Instantiate(_clickVFX, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void SetRandomColor()
    {
        _currentColor = (BallColor)Random.Range(0, 3);
        _originalColor = _currentColor; // Store the initial color
        UpdateColorVisuals();
    }

    private void ChangeColorOnHit()
    {
        if (_groundHitCount == 1)
        {
            BallColor newColor;
            do
            {
                newColor = (BallColor)Random.Range(0, 3);
            } while (newColor == _originalColor);
            _currentColor = newColor;
        }
        else if (_groundHitCount == 2)
        {
            int originalColorIndex = (int)_originalColor;
            int currentColorIndex = (int)_currentColor;
            BallColor lastColor = (BallColor)(3 - originalColorIndex - currentColorIndex);
            _currentColor = lastColor;
        }

        UpdateColorVisuals();
    }

    private void UpdateColorVisuals()
    {
        switch (_currentColor)
        {
            case BallColor.Blue:
                _renderer.material.color = Color.blue;
                break;
            case BallColor.Red:
                _renderer.material.color = Color.red;
                break;
            case BallColor.Yellow:
                _renderer.material.color = Color.yellow;
                break;
        }
    }
    #endregion
}
