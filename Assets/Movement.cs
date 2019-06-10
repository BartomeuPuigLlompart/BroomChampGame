using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    private Vector3 moveDirection;
    private float VerticalMove;
    public float speedMultiplier;
    public int lives;

    // Use this for initialization
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        moveDirection = Vector3.zero;
        VerticalMove = Input.GetAxis("Vertical") * (1 / Mathf.Abs(Input.GetAxis("Vertical")));
        lives = 2;
    }

    // Update is called once per frame
    void Update()
    {
        AxisMovement();
        Debug.Log(lives);
    }

    void AxisMovement()
    {
        moveDirection = Vector3.zero;
        VerticalMovement();
        HorizontalMovement();
        transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), 0);
    }

    void VerticalMovement()
    {
        if (System.Single.IsNaN(VerticalMove))
        {
            VerticalMove = Input.GetAxis("Vertical") * (1 / Mathf.Abs(Input.GetAxis("Vertical")));
            if (!System.Single.IsNaN(VerticalMove))
            {
                moveDirection = new Vector3(0, VerticalMove, 0);
                RaycastHit hit = new RaycastHit();
                Ray raycast = new Ray(transform.position, moveDirection);
                if (!Physics.Raycast(raycast, out hit, 1, 1 << LayerMask.NameToLayer("Border")))
                    transform.position += moveDirection;
            }
        }
        else
            VerticalMove = Input.GetAxis("Vertical") * (1 / Mathf.Abs(Input.GetAxis("Vertical")));
    }

    public void actualLivesConsec()
    {
        switch(lives)
        {
            case 0:
                SceneManager.LoadScene("speed");
                break;
            case 1:
                MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed / 2;
                break;
                case 2:
                MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed;
                break;
            default:
                break;
        }
    }

    void HorizontalMovement()
    {
        if(lives > 1)rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * speedMultiplier, 0, 0));
    }

    public IEnumerator rideBroom(float time)
    {
        yield return new WaitForSeconds(time);

        lives ++;
        actualLivesConsec();
    }
}
