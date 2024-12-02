using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float Speed;
    public bool isAnimation;
    Animator animator;
    //private bool isWalk;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        //isWalk = animator.GetBool("isWalk");
        isAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(h * Speed, 0);

        if (h > 0)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);

        }
        else if (h<0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            }

        if (h != 0 || isAnimation)
        {
            animator.SetTrigger("isWalk");
        }
        else
        {
            animator.ResetTrigger("isWalk");
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LEVEL3_EXIT")
        {
            SceneManager.LoadScene("Title Page");
        }
       
    }
}
