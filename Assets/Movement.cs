using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public struct powerUpBag
    {
        public powerUP[] StoredSpeedPowerUp;
        public powerUP[] StoredDeffensePowerUp;
        public powerUP[] StoredAtackPowerUp;
        public powerUP[] ActiveSpeedPowerUp;
        public powerUP[] ActiveDeffensePowerUp;
        public powerUP[] ActiveAtackPowerUp;

        public GameObject[] StoredSpeedPowerUpSprite;
        public GameObject[] StoredDeffensePowerUpSprite;
        public GameObject[] StoredAtackPowerUpSprite;
        public GameObject[] ActiveSpeedPowerUpSprite;
        public GameObject[] ActiveDeffensePowerUpSprite;
        public GameObject[] ActiveAtackPowerUpSprite;
    }

    private GameObject canvasObject;

    public static powerUpBag PowerUpBag;
    Rigidbody rb;
    private Vector3 moveDirection;
    private float VerticalMove;
    public float speedMultiplier;
    public int lives;
    private ParticleSystem HIGH_em;
    private ParticleSystem MED_em;
    private ParticleSystem LOW_em;

    // Use this for initialization
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        moveDirection = Vector3.zero;
        VerticalMove = Input.GetAxis("Vertical") * (1 / Mathf.Abs(Input.GetAxis("Vertical")));
        lives = 2;
        HIGH_em = transform.GetChild(0).GetChild(0).transform.GetComponent<ParticleSystem>();
        MED_em = transform.GetChild(0).GetChild(1).transform.GetComponent<ParticleSystem>();
        LOW_em = transform.GetChild(0).GetChild(2).transform.GetComponent<ParticleSystem>();
        HIGH_em.enableEmission = false;
        LOW_em.enableEmission = false;

        canvasObject = GameObject.Find("Canvas");

        PowerUpBag.ActiveAtackPowerUp = new AtackPowerUp[4];
        PowerUpBag.ActiveDeffensePowerUp = new DefensePowerUp[4];
        PowerUpBag.ActiveSpeedPowerUp = new SpeedPowerUp[4];
        PowerUpBag.StoredAtackPowerUp = new AtackPowerUp[4];
        PowerUpBag.StoredDeffensePowerUp = new DefensePowerUp[4];
        PowerUpBag.StoredSpeedPowerUp = new SpeedPowerUp[4];

        PowerUpBag.ActiveAtackPowerUpSprite = new GameObject[4];
        PowerUpBag.ActiveDeffensePowerUpSprite = new GameObject[4];
        PowerUpBag.ActiveSpeedPowerUpSprite = new GameObject[4];
        PowerUpBag.StoredAtackPowerUpSprite = new GameObject[4];
        PowerUpBag.StoredDeffensePowerUpSprite = new GameObject[4];
        PowerUpBag.StoredSpeedPowerUpSprite = new GameObject[4];

        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(0).childCount; i++)
            PowerUpBag.StoredSpeedPowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(0).gameObject;
        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(1).childCount; i++)
            PowerUpBag.ActiveSpeedPowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(1).gameObject;
        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(2).childCount; i++)
            PowerUpBag.StoredDeffensePowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(2).gameObject;
        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(3).childCount; i++)
            PowerUpBag.ActiveDeffensePowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(3).gameObject;
        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(4).childCount; i++)
            PowerUpBag.StoredAtackPowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(4).gameObject;
        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(5).childCount; i++)
            PowerUpBag.ActiveAtackPowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(5).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        AxisMovement();
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
                killPlayer();
                break;
            case 1:
                MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed / 2;
                HIGH_em.enableEmission = false;
                MED_em.enableEmission = false;
                LOW_em.enableEmission = true;
                break;
                case 2:
                MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed;
                HIGH_em.enableEmission = false;
                MED_em.enableEmission = true;
                LOW_em.enableEmission = false;
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

    public void activateTurbo()
    {
        MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed * 2;
        HIGH_em.enableEmission = true;
        MED_em.enableEmission = false;
        LOW_em.enableEmission = false;

        StopAllCoroutines();
        StartCoroutine(stopTurbo(2.0f));
    }

    public void killPlayer()
    {
        SceneManager.LoadScene("speed");
    }

    IEnumerator stopTurbo(float time)
    {
        yield return new WaitForSeconds(time);

        actualLivesConsec();
    }
}
