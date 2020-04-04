using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{

    public float movementSpeed, movementDamping, jumpVelocity;
    public float canJumpAfterUnground;
    public float wallSlideMaxSpeed;

    public List<Sprite> cloakSprites = new List<Sprite>();

    public float movementModifier = 1, jumpModifier = 1;

    public GameObject cloak;
    public GameObject baseCloak;

    int facing = 1;

    SpriteRenderer sr;

    float jumpTimer;
    float noDoubleJump = 0.5f;
    bool jumped = false;
    float hVel, vVel;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Horizontal Velocity Calculation

        hVel += Input.GetAxisRaw("Horizontal") * movementSpeed * movementModifier;
        hVel *= Mathf.Pow(1f - movementDamping, Time.deltaTime * 10f);

        // Set velocity to the target horizontal velocity

        rb.velocity = new Vector2(hVel, rb.velocity.y);

        // Cloak Handling
        if (cloak != baseCloak)
        {
            Cloak c = cloak.GetComponent<Cloak>();
            if (c != null)
            {
                movementModifier = c.movementModifier;
                jumpModifier = c.jumpModifier;

            }
            else
            {
                print("Invalid Cloak");
                movementModifier = 1;
                jumpModifier = 1;
                cloak = baseCloak;
            }

        }
        else {
            movementModifier = 1;
            jumpModifier = 1;
        }

    }

    void Update()
    {   

        // Jumping

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {

            if (jumpTimer > 0 && !jumped)
            {
                jumpTimer = 0;
                vVel = jumpVelocity * jumpModifier;
                rb.velocity = new Vector2(hVel, vVel);
                print(IsGrounded());
                jumped = true;

            }

        }

        // Jump Timings

        if (jumped) { noDoubleJump -= Time.deltaTime; }
        else { noDoubleJump = 0.5f; }
        if (noDoubleJump <= 0) { jumped = false; }

        if (IsGrounded())
        {
            jumpTimer = canJumpAfterUnground;

        }
        else
        {
            jumpTimer -= Time.deltaTime;

        }

        if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) == -1)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            facing = -1;
        } else if (Mathf.Sign(rb.velocity.x) == 1)
        {
            transform.eulerAngles = Vector3.zero;
            facing = 1;

        }

        // Firing Projectiles

        if (Input.GetKeyDown(KeyCode.E)) {

            cloak.GetComponent<Cloak>().fireProjectile(transform.position, facing);


        }

        // Sprite Changes

        if (cloak != baseCloak) {

            sr.sprite = cloakSprites[cloak.GetComponent<Cloak>().spriteNumber];

        } else {

            sr.sprite = cloakSprites[0];

        }

        // Wall Sliding
        //bool wallSliding = false;

        //if ((DetectCollisions(Vector3.left) || DetectCollisions(Vector3.right)) && !DetectCollisions(Vector3.down))
        //{
        //    wallSliding = true;
        //    if (rb.velocity.y < -wallSlideMaxSpeed)
        //    {
        //        vVel = -wallSlideMaxSpeed;
        //        rb.velocity = new Vector2(hVel, vVel);

        //    }
        //}



    }

    bool IsGrounded() {
        return DetectCollisions(Vector3.down);
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.tag == "Respawn")
        {
            GameObject.Find("SpawnPoint").GetComponent<SpawnPlayer>().respawnPlayer();

        }

    }

    bool DetectCollisions(Vector3 direction)
    {
        return Physics2D.Raycast(transform.position, direction, GetComponent<BoxCollider2D>().bounds.extents.y + 0.1f);
    }
}
