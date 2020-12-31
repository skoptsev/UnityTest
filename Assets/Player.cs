using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float leftX = -5.1f;
    float rightX = 5.1f;

    private Rigidbody2D rb;
    private float jumpForce = 40f;
    private bool isLeft = false;
    private bool isJump = false;

    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!isJump && Input.GetAxis("Jump") > 0)
        {
            isJump = true;
            rb.AddForce((!isLeft ? Vector3.left : Vector3.right) * jumpForce, ForceMode2D.Impulse);
        }

        if (isJump)
        {
            if (!isLeft && Mathf.Abs(transform.position.x - leftX) <= 0.5) fixX(leftX);
            if (isLeft && Mathf.Abs(transform.position.x - rightX) <= 0.5) fixX(rightX);
        }

        //transform.Translate((cube.transform.position - transform.position) * Time.fixedDeltaTime);

    }

    void fixX(float coordX) {
        transform.position = new Vector2(coordX, transform.position.y);
        rb.Sleep();
        isLeft = !isLeft;
        isJump = false;
    }
}
