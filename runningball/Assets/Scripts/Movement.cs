using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    // X-axis movement
    private float moveXWidth = 1.5f;
    private float moveTimeX = 0.1f;
    private bool isXMove = false;

    // Y-axis movement
    private float originY = 0.55F;
    private float gravity = -9.81f;
    private float moveTimeY = 0.5f;
    private bool isJump = false;

    // Z-axis movement
    [SerializeField]
    private float moveSpeed = 20.0f;

    // Rotation speed
    private float rotateSpeed = 300.0f;

    private float limitY = -1.0f;

    private Rigidbody rigidbody;
    private bool hasAvatarModel = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        
        // Check if there's an avatar model (child object)
        AvatarCharacterHelper avatarHelper = GetComponent<AvatarCharacterHelper>();
        hasAvatarModel = avatarHelper != null;
    }

    private void Update()
    {
        if (gameController.IsGameStart == false) return;

        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        
        // Only rotate if there's no avatar model (backward compatibility with sphere)
        if (!hasAvatarModel)
        {
            transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
        }

        // Handle arrow key input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToX(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToX(1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            MoveToY();
        }

        if(transform.position.y < limitY)
        {
            Debug.Log("Game Over");
        }
    }

    public void MoveToX(int x)
    {
        if (isXMove == true) return;

        if( x > 0 && transform.position.x < moveXWidth )
        {
            StartCoroutine(OnMoveToX(x)); // Move right
        }
        else if ( x < 0 && transform.position.x > -moveXWidth )
        {
            StartCoroutine(OnMoveToX(x)); // Move left
        }
    }

    public void MoveToY()
    {
        if (isJump == true) return;

        StartCoroutine(OnMoveToY());
    }

    private IEnumerator OnMoveToX(int direction)
    {
        float current = 0;
        float percent = 0;
        float start = transform.position.x;
        float end = transform.position.x + direction * moveXWidth;

        isXMove = true;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / moveTimeX;

            float x = Mathf.Lerp(start, end, percent);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            yield return null;
        }

        isXMove = false;
    }

    private IEnumerator OnMoveToY()
    {
        float current = 0;
        float percent = 0;
        float v0 = -gravity; // Y-axis initial velocity

        isJump = true;
        rigidbody.useGravity = false;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTimeY;

            float y = originY + (v0 * percent) + (gravity * percent * percent);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

            yield return null;
        }

        isJump = false;
        rigidbody.useGravity = true;
    }
}
