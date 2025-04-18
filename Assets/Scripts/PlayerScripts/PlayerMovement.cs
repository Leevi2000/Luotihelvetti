using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    GameObject GameManager;
    InputHandler inputHandler;
    Rigidbody2D rb2d;
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        inputHandler = GameManager.GetComponent<InputHandler>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void MovePlayer()
    {
        Vector2 dir = new Vector2(0,0);

        //suunta oikealle
        if (inputHandler.IsRightPressed() && !inputHandler.IsLeftPressed())
        {
            dir.x = 1;
        }
        //suunta vasemmalle
        if (inputHandler.IsLeftPressed() && !inputHandler.IsRightPressed())
        {
            dir.x = -1;
        }
        //suunta ylös
        if (inputHandler.IsForwardPressed() && !inputHandler.IsBackwardPressed())
        {
            dir.y = 1;
        }
        //suunta alas
        if (inputHandler.IsBackwardPressed() && !inputHandler.IsForwardPressed())
        {
            dir.y = -1;
        }
        
        //jos kahta näppäintä painetaan niin nopeus säädetään sen mukaisesti
        if (dir.magnitude > 1)
        {
            dir /= 1.5f;
        }

        rb2d.velocity = new Vector2(Mathf.Lerp(rb2d.velocity.x, dir.x * speed, 0.1f), Mathf.Lerp(rb2d.velocity.y, dir.y * speed, 0.1f));
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
}
