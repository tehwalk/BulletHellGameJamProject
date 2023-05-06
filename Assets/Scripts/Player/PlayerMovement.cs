using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    Rigidbody2D rb;
    Vector2 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDir.Set(h, v);
        //transform.Translate(moveDir * moveSpeed * Time.deltaTime);
        rb.position += moveDir * moveSpeed * Time.deltaTime;

    }

    void Update()
    {
        if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y))
        {
            animator.SetBool("TopView", false);
            //animate left- right
            if (moveDir.x >= 0) spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
        }
        else if (Mathf.Abs(moveDir.x) < Mathf.Abs(moveDir.y))
        {
            animator.SetBool("TopView", true);
            //animate top-bottom
        }
    }
}
