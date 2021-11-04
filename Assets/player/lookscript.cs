using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookscript : MonoBehaviour
{
    public Transform cam_trans; public Camera cam;
    public bool ragdoll = false;
    float mouse_sensx = 4, mouse_sensy = 2, time_start = 0;
    Vector3 cur_rotation = new Vector3(0, 0, 0), gravity = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //hide player's mouse
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            cam.fieldOfView = cam.fieldOfView - 10 * Input.mouseScrollDelta.y;
            if (cam.fieldOfView < 10)
                cam.fieldOfView = 10;
            if (cam.fieldOfView > 60)
                cam.fieldOfView = 60;
        }
        else
            cam.fieldOfView = 60;
        //handle rotation, we do it out here so the player can look around when they're dead
        cur_rotation.y += Input.GetAxis("Mouse X") * mouse_sensx; cur_rotation.x += Input.GetAxis("Mouse Y") * -mouse_sensy;
        if (cur_rotation.x > 90)
            cur_rotation.x = 90;
        if (cur_rotation.x < -95)
            cur_rotation.x = -95;
        if (!ragdoll)
        {
            transform.rotation = Quaternion.Euler(gravity.x, cur_rotation.y + gravity.y, gravity.z);
            cam_trans.rotation = Quaternion.Euler(cur_rotation.x + gravity.x, cur_rotation.y + gravity.y, gravity.z);
        }
        else
        {
            if (cur_rotation.y > 90)
                cur_rotation.y = 90;
            if (cur_rotation.y < -90)
                cur_rotation.y = -90;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                player play = GetComponent<player>();
                cur_rotation.z = 0;
                play.enabled = true;
                play.player_body.freezeRotation = true;
                ragdoll = false;
            }
            cam_trans.rotation = Quaternion.Euler(-transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
    }
}
