using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public Transform cam_trans;
    public Rigidbody player_body;
    public AudioSource foot_s;

    public float walkspeed = 0.5f, runspeed = 2, max_speed = 10, jump_speed = 5, max_health = 100, health = 100;
    float speed_mul = 1;
    public bool jumping = true, can_die = false;
    // Start is called before the first frame update
    void Start()
    {
        player_body = GetComponent<Rigidbody>();
        speed_mul = walkspeed;
        health = max_health;
    }
    public void Ragdoll(Vector3 force)
    {
        player_body.freezeRotation = false;
        player_body.AddForce(force, ForceMode.Impulse);
        player_body.GetComponent<lookscript>().ragdoll = true;
        this.enabled = false;
    }
    void FixedUpdate()
    {
        //now lets make sure that the player is on the ground
        Ray ground_ray = new Ray(transform.position, -transform.up), lookray = new Ray(cam_trans.position, cam_trans.forward);
        if (Physics.Raycast(ground_ray, 1))
        {
            player_body.AddForce(-new Vector3(player_body.velocity.x, 0, player_body.velocity.z) * 10);
            Vector3 direction = new Vector3(0,0,0);
            //now everything in here can only be done if the player is on the ground
            if (Mathf.Abs(player_body.velocity.x) + Mathf.Abs(player_body.velocity.z) < max_speed * (speed_mul / walkspeed))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    speed_mul = runspeed;
                else
                    speed_mul = walkspeed;
                if (Input.GetKey(KeyCode.W))
                    direction = transform.forward;
                if (Input.GetKey(KeyCode.S))
                    direction = -transform.forward;
                if (Input.GetKey(KeyCode.A))
                    direction = direction / 2 - transform.right / 2;
                if (Input.GetKey(KeyCode.D))
                    direction = direction / 2 + transform.right / 2;
                if (direction.magnitude <= 0.6f)
                    direction *= 2;
                player_body.AddForce(direction * speed_mul);
            }

            if (Input.GetKeyDown(KeyCode.Space) && jumping)
                player_body.AddForce(transform.up * jump_speed);
        }
    }
}

