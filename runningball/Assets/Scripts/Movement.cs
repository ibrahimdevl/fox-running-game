using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // X축 이동
    private float moveXWidth = 1.5f;
    private float moveTimeX = 0.1f;
    private bool isXMove = false;

    // Y축 이동
    private float originY = 0.55F;
    private float gravity = -9.81f;
    private float moveTimeY = 0.3f;
    private bool isJump = false;

    // Z축 이동
    [SerializeField]
    private float moveSpeed = 20.0f;

    // 회전
    private float rotateSpeed = 300.0f;

    private float limitY = -1.0f;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        if(transform.position.y > limitY)
        {
            Debug.Log("게임 오버");
        }
    }

    public void MoveToX(int x)
    {
        if (isXMove == true) return;

        if( x > 0 && transform.position.x < moveXWidth )
        {
            StartCoroutine(OnMoveToX(x)); // 오른쪽 이동
        }
        else if ( x < 0 && transform.position.x > -moveXWidth )
        {
            StartCoroutine(OnMoveToX(x)); // 왼쪽 이동
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
        float v0 = -gravity; // Y방향의 초기 속도

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
