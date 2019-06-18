using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    private GameObject shield;
    public float lowHealthRef;

    private AudioSource source;

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
        source = GetComponent<AudioSource>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        moveDirection = Vector3.zero;
        VerticalMove = Input.GetAxis("Vertical") * (1 / Mathf.Abs(Input.GetAxis("Vertical")));
        lives = 2;
        HIGH_em = transform.GetChild(0).GetChild(0).transform.GetComponent<ParticleSystem>();
        MED_em = transform.GetChild(0).GetChild(1).transform.GetComponent<ParticleSystem>();
        LOW_em = transform.GetChild(0).GetChild(2).transform.GetComponent<ParticleSystem>();
        HIGH_em.enableEmission = false;
        LOW_em.enableEmission = false;

        lowHealthRef = 0.0f;

        canvasObject = GameObject.Find("Canvas");
        shield = transform.GetChild(1).GetChild(0).gameObject;
        shield.SetActive(false);

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
            PowerUpBag.StoredSpeedPowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(0).GetChild(i).gameObject;
        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(1).childCount; i++)
            PowerUpBag.ActiveSpeedPowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(1).GetChild(i).gameObject;
        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(2).childCount; i++)
            PowerUpBag.StoredDeffensePowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(2).GetChild(i).gameObject;
        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(3).childCount; i++)
            PowerUpBag.ActiveDeffensePowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(3).GetChild(i).gameObject;
        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(4).childCount; i++)
            PowerUpBag.StoredAtackPowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(4).GetChild(i).gameObject;
        for (int i = 0; i < canvasObject.transform.GetChild(0).GetChild(5).childCount; i++)
            PowerUpBag.ActiveAtackPowerUpSprite[i] = canvasObject.transform.GetChild(0).GetChild(5).GetChild(i).gameObject;

        Color c;
        for (int i = 0; i < PowerUpBag.ActiveAtackPowerUpSprite.Length; i++)
        {
            c = PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color;
            c.a = 0;
            PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color = c;

            c = PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color;
            c.a = 0;
            PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color = c;

            c = PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color;
            c.a = 0;
            PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color = c;

            c = PowerUpBag.StoredAtackPowerUpSprite[i].GetComponent<Image>().color;
            c.a = 0;
            PowerUpBag.StoredAtackPowerUpSprite[i].GetComponent<Image>().color = c;

            c = PowerUpBag.StoredDeffensePowerUpSprite[i].GetComponent<Image>().color;
            c.a = 0;
            PowerUpBag.StoredDeffensePowerUpSprite[i].GetComponent<Image>().color = c;

            c = PowerUpBag.StoredSpeedPowerUpSprite[i].GetComponent<Image>().color;
            c.a = 0;
            PowerUpBag.StoredSpeedPowerUpSprite[i].GetComponent<Image>().color = c;
        }

    }

    // Update is called once per frame
    void Update()
    {
        AxisMovement();
        powerUpBagUpdate();
        actualLivesConsec();
        if (lives == 1 && lowHealthRef + 5 < Time.realtimeSinceStartup) lives = 2;
        if (transform.position.x < -13 - 0f || transform.position.x > 13) killPlayer();
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
        if (lives == 1)
        {
            Color c = canvasObject.transform.GetChild(0).GetComponent<Image>().color;
            c.b = c.g = 0.0f;
            canvasObject.transform.GetChild(0).GetComponent<Image>().color = c;
        }
        else
        {
            Color c = canvasObject.transform.GetChild(0).GetComponent<Image>().color;
            c.b = c.g = 255.0f;
            canvasObject.transform.GetChild(0).GetComponent<Image>().color = c;
        }
    }

    void HorizontalMovement()
    {
        if(lives > 1)rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * speedMultiplier, 0, 0));
    }

    private void powerUpBagUpdate()
    {
        checkCombo();
        updateTime();
        updatePowers();
    }

    private void checkCombo()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            swapStoredPowerUps(powerUP.comboKey.UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            swapStoredPowerUps(powerUP.comboKey.DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            swapStoredPowerUps(powerUP.comboKey.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            swapStoredPowerUps(powerUP.comboKey.RIGHT);
        }
    }

    private void swapStoredPowerUps(powerUP.comboKey key)
    {
        source.Play();
        bool full1, full2, full3;
        int bonification = 0;
        full1 = PowerUpBag.StoredSpeedPowerUp[0] == null || key != PowerUpBag.StoredSpeedPowerUp[0].GetComponent<powerUP>().ComboKey ? false : true;
        full2 = PowerUpBag.StoredDeffensePowerUp[0] == null || key != PowerUpBag.StoredDeffensePowerUp[0].GetComponent<powerUP>().ComboKey ? false : true;
        full3 = PowerUpBag.StoredAtackPowerUp[0] == null || key != PowerUpBag.StoredAtackPowerUp[0].GetComponent<powerUP>().ComboKey ? false : true;
        for (int i = 0; i < 4; i++)
        {
            if (PowerUpBag.ActiveSpeedPowerUp[i] == null && full1)
            {
                Color c;
                c = PowerUpBag.StoredSpeedPowerUpSprite[0].GetComponent<Image>().color;
                c.a = 0;
                PowerUpBag.StoredSpeedPowerUpSprite[0].GetComponent<Image>().color = c;
                if (key == PowerUpBag.StoredSpeedPowerUp[0].GetComponent<powerUP>().ComboKey)
                {
                    bonification++;
                    if (i == 0)
                    {
                        PowerUpBag.ActiveSpeedPowerUp[i] = PowerUpBag.StoredSpeedPowerUp[0].GetComponent<powerUP>();
                        PowerUpBag.ActiveSpeedPowerUp[i].activationTime = Time.realtimeSinceStartup;
                    }
                    else
                    {
                        powerUP aux = PowerUpBag.ActiveSpeedPowerUp[i - 1].GetComponent<powerUP>();
                        PowerUpBag.ActiveSpeedPowerUp[i - 1] = PowerUpBag.StoredSpeedPowerUp[0].GetComponent<powerUP>();
                        c = PowerUpBag.ActiveSpeedPowerUpSprite[i - 1].GetComponent<Image>().color;
                        c.a = 1.0f;
                        PowerUpBag.ActiveSpeedPowerUpSprite[i - 1].GetComponent<Image>().color = c;
                        PowerUpBag.ActiveSpeedPowerUp[i] = aux.GetComponent<powerUP>();
                    }
                    c = PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color;
                    c.a = 255;
                    PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color = c;
                }
                full1 = false;
            }

            if (PowerUpBag.ActiveDeffensePowerUp[i] == null && full2)
            {
                Color c;
                c = PowerUpBag.StoredDeffensePowerUpSprite[0].GetComponent<Image>().color;
                c.a = 0;
                PowerUpBag.StoredDeffensePowerUpSprite[0].GetComponent<Image>().color = c;
                if (key == PowerUpBag.StoredDeffensePowerUp[0].GetComponent<powerUP>().ComboKey)
                {
                    bonification++;
                    if (i == 0)
                    {
                        PowerUpBag.ActiveDeffensePowerUp[i] = PowerUpBag.StoredDeffensePowerUp[0].GetComponent<powerUP>();
                        PowerUpBag.ActiveDeffensePowerUp[i].activationTime = Time.realtimeSinceStartup;
                        lives = 3;
                    }
                    else
                    {
                        powerUP aux = PowerUpBag.ActiveDeffensePowerUp[i - 1].GetComponent<powerUP>();
                        PowerUpBag.ActiveDeffensePowerUp[i - 1] = PowerUpBag.StoredDeffensePowerUp[0].GetComponent<powerUP>();
                        c = PowerUpBag.ActiveDeffensePowerUpSprite[i - 1].GetComponent<Image>().color ;
                        c.a = 1.0f;
                        PowerUpBag.ActiveDeffensePowerUpSprite[i - 1].GetComponent<Image>().color = c;
                        PowerUpBag.ActiveDeffensePowerUp[i] = aux.GetComponent<powerUP>();
                    }
                    c = PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color;
                    c.a = 255;
                    PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color = c;
                }
                full2 = false;
            }

            if (PowerUpBag.ActiveAtackPowerUp[i] == null && full3)
            {
                Color c;
                c = PowerUpBag.StoredAtackPowerUpSprite[0].GetComponent<Image>().color;
                c.a = 0;
                PowerUpBag.StoredAtackPowerUpSprite[0].GetComponent<Image>().color = c;
                if (key == PowerUpBag.StoredAtackPowerUp[0].GetComponent<powerUP>().ComboKey)
                {
                    bonification++;
                    if (i == 0)
                    {
                        PowerUpBag.ActiveAtackPowerUp[i] = PowerUpBag.StoredAtackPowerUp[0].GetComponent<powerUP>();
                        PowerUpBag.ActiveAtackPowerUp[i].activationTime = Time.realtimeSinceStartup;
                    }
                    else
                    {
                        powerUP aux = PowerUpBag.ActiveAtackPowerUp[i - 1].GetComponent<powerUP>();
                        PowerUpBag.ActiveAtackPowerUp[i - 1] = PowerUpBag.StoredAtackPowerUp[0].GetComponent<powerUP>();
                        c = PowerUpBag.ActiveAtackPowerUpSprite[i - 1].GetComponent<Image>().color;
                        c.a = 1.0f;
                        PowerUpBag.ActiveAtackPowerUpSprite[i - 1].GetComponent<Image>().color = c;
                        PowerUpBag.ActiveAtackPowerUp[i] = aux.GetComponent<powerUP>();
                    }
                    c = PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color;
                    c.a = 255;
                    PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color = c;
                }
                full3 = false;
            }
        }
        if (full1 || full2 || full3) killPlayer();

        if (bonification > 1)
        {
            if (PowerUpBag.StoredSpeedPowerUp[0] != null) PowerUpBag.StoredSpeedPowerUp[0].duration *= bonification;
            if (PowerUpBag.StoredDeffensePowerUp[0] != null) PowerUpBag.StoredDeffensePowerUp[0].duration *= bonification;
            if (PowerUpBag.StoredAtackPowerUp[0] != null) PowerUpBag.StoredAtackPowerUp[0].duration *= bonification;
        }

        Color color;
        PowerUpBag.StoredSpeedPowerUp[0] = PowerUpBag.StoredSpeedPowerUp[1] == null ? null : PowerUpBag.StoredSpeedPowerUp[1].GetComponent<powerUP>();
        PowerUpBag.StoredSpeedPowerUpSprite[0].GetComponent<Image>().sprite = PowerUpBag.StoredSpeedPowerUpSprite[1].GetComponent<Image>().sprite;
        PowerUpBag.StoredSpeedPowerUpSprite[0].GetComponent<Image>().color = PowerUpBag.StoredSpeedPowerUpSprite[1].GetComponent<Image>().color;
        PowerUpBag.StoredSpeedPowerUp[1] = PowerUpBag.StoredSpeedPowerUp[2] == null ? null : PowerUpBag.StoredSpeedPowerUp[2].GetComponent<powerUP>();
        PowerUpBag.StoredSpeedPowerUpSprite[1].GetComponent<Image>().sprite = PowerUpBag.StoredSpeedPowerUpSprite[2].GetComponent<Image>().sprite;
        PowerUpBag.StoredSpeedPowerUpSprite[1].GetComponent<Image>().color = PowerUpBag.StoredSpeedPowerUpSprite[2].GetComponent<Image>().color;
        PowerUpBag.StoredSpeedPowerUp[2] = PowerUpBag.StoredSpeedPowerUp[3] == null ? null : PowerUpBag.StoredSpeedPowerUp[3].GetComponent<powerUP>();
        PowerUpBag.StoredSpeedPowerUpSprite[2].GetComponent<Image>().sprite = PowerUpBag.StoredSpeedPowerUpSprite[3].GetComponent<Image>().sprite;
        PowerUpBag.StoredSpeedPowerUpSprite[2].GetComponent<Image>().color = PowerUpBag.StoredSpeedPowerUpSprite[3].GetComponent<Image>().color;
        PowerUpBag.StoredSpeedPowerUp[3] = null;
        color = PowerUpBag.StoredSpeedPowerUpSprite[3].GetComponent<Image>().color;
        color.a = 0.0f;
        PowerUpBag.StoredSpeedPowerUpSprite[3].GetComponent<Image>().color = color;

        PowerUpBag.StoredDeffensePowerUp[0] = PowerUpBag.StoredDeffensePowerUp[1] == null ? null : PowerUpBag.StoredDeffensePowerUp[1].GetComponent<powerUP>();
        PowerUpBag.StoredDeffensePowerUpSprite[0].GetComponent<Image>().sprite = PowerUpBag.StoredDeffensePowerUpSprite[1].GetComponent<Image>().sprite;
        PowerUpBag.StoredDeffensePowerUpSprite[0].GetComponent<Image>().color = PowerUpBag.StoredDeffensePowerUpSprite[1].GetComponent<Image>().color;
        PowerUpBag.StoredDeffensePowerUp[1] = PowerUpBag.StoredDeffensePowerUp[2] == null ? null : PowerUpBag.StoredDeffensePowerUp[2].GetComponent<powerUP>();
        PowerUpBag.StoredDeffensePowerUpSprite[1].GetComponent<Image>().sprite = PowerUpBag.StoredDeffensePowerUpSprite[2].GetComponent<Image>().sprite;
        PowerUpBag.StoredDeffensePowerUpSprite[1].GetComponent<Image>().color = PowerUpBag.StoredDeffensePowerUpSprite[2].GetComponent<Image>().color;
        PowerUpBag.StoredDeffensePowerUp[2] = PowerUpBag.StoredDeffensePowerUp[3] == null ? null : PowerUpBag.StoredDeffensePowerUp[3].GetComponent<powerUP>();
        PowerUpBag.StoredDeffensePowerUpSprite[2].GetComponent<Image>().sprite = PowerUpBag.StoredDeffensePowerUpSprite[3].GetComponent<Image>().sprite;
        PowerUpBag.StoredDeffensePowerUpSprite[2].GetComponent<Image>().color = PowerUpBag.StoredDeffensePowerUpSprite[3].GetComponent<Image>().color;
        PowerUpBag.StoredDeffensePowerUp[3] = null;
        color = PowerUpBag.StoredDeffensePowerUpSprite[3].GetComponent<Image>().color;
        color.a = 0.0f;
        PowerUpBag.StoredDeffensePowerUpSprite[3].GetComponent<Image>().color = color;

        PowerUpBag.StoredAtackPowerUp[0] = PowerUpBag.StoredAtackPowerUp[1] == null ? null : PowerUpBag.StoredAtackPowerUp[1].GetComponent<powerUP>();
        PowerUpBag.StoredAtackPowerUpSprite[0].GetComponent<Image>().sprite = PowerUpBag.StoredAtackPowerUpSprite[1].GetComponent<Image>().sprite;
        PowerUpBag.StoredAtackPowerUpSprite[0].GetComponent<Image>().color = PowerUpBag.StoredAtackPowerUpSprite[1].GetComponent<Image>().color;
        PowerUpBag.StoredAtackPowerUp[1] = PowerUpBag.StoredAtackPowerUp[2] == null ? null : PowerUpBag.StoredAtackPowerUp[2].GetComponent<powerUP>();
        PowerUpBag.StoredAtackPowerUpSprite[1].GetComponent<Image>().sprite = PowerUpBag.StoredAtackPowerUpSprite[2].GetComponent<Image>().sprite;
        PowerUpBag.StoredAtackPowerUpSprite[1].GetComponent<Image>().color = PowerUpBag.StoredAtackPowerUpSprite[2].GetComponent<Image>().color;
        PowerUpBag.StoredAtackPowerUp[2] = PowerUpBag.StoredAtackPowerUp[3] == null ? null : PowerUpBag.StoredAtackPowerUp[3].GetComponent<powerUP>();
        PowerUpBag.StoredAtackPowerUpSprite[2].GetComponent<Image>().sprite = PowerUpBag.StoredAtackPowerUpSprite[3].GetComponent<Image>().sprite;
        PowerUpBag.StoredAtackPowerUpSprite[2].GetComponent<Image>().color = PowerUpBag.StoredAtackPowerUpSprite[3].GetComponent<Image>().color;
        PowerUpBag.StoredAtackPowerUp[3] = null;
        color = PowerUpBag.StoredAtackPowerUpSprite[3].GetComponent<Image>().color;
        color.a = 0.0f;
        PowerUpBag.StoredAtackPowerUpSprite[3].GetComponent<Image>().color = color;
    }

    private void updateTime()
    {
        for (int i = PowerUpBag.ActiveAtackPowerUpSprite.Length - 1; i >= 0; i--)
        {
            if (PowerUpBag.ActiveSpeedPowerUp[i] != null && PowerUpBag.ActiveSpeedPowerUp[i].activationTime != 0 && PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color.a > 0)
            {
                if (i != 0 && PowerUpBag.ActiveSpeedPowerUp[i - 1].activationTime != 0) PowerUpBag.ActiveSpeedPowerUp[i - 1].activationTime = 0.0f;
                Color c = PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color;
                c.a = (((PowerUpBag.ActiveSpeedPowerUp[i].duration - (Time.realtimeSinceStartup - PowerUpBag.ActiveSpeedPowerUp[i].activationTime))) / PowerUpBag.ActiveSpeedPowerUp[i].duration);
                PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color = c;

                if (PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color.a <= 0)
                {
                    PowerUpBag.ActiveSpeedPowerUp[i] = null;
                    if (i != 0) PowerUpBag.ActiveSpeedPowerUp[i - 1].activationTime = Time.realtimeSinceStartup;
                }
            }
            else if (PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color.a > 0 && (i == 3 || PowerUpBag.ActiveSpeedPowerUpSprite[i + 1].GetComponent<Image>().color.a <= 0))
            {
                if (PowerUpBag.ActiveSpeedPowerUp[i] != null) PowerUpBag.ActiveSpeedPowerUp[i].activationTime = Time.realtimeSinceStartup;
                else
                {
                    Color c = PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color;
                    c.a = (((PowerUpBag.ActiveSpeedPowerUp[i].duration - (Time.realtimeSinceStartup - PowerUpBag.ActiveSpeedPowerUp[i].activationTime))) / PowerUpBag.ActiveSpeedPowerUp[i].duration);
                    PowerUpBag.ActiveSpeedPowerUpSprite[i].GetComponent<Image>().color = c;
                }
            }

                if (PowerUpBag.ActiveDeffensePowerUp[i] != null && PowerUpBag.ActiveDeffensePowerUp[i].activationTime != 0 && PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color.a > 0)
            {
                if (i != 0 && PowerUpBag.ActiveDeffensePowerUp[i - 1].activationTime != 0) PowerUpBag.ActiveDeffensePowerUp[i - 1].activationTime = 0.0f;
                if (lives == 3)
                {
                    Color c = PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color;
                    c.a = (((PowerUpBag.ActiveDeffensePowerUp[i].duration - (Time.realtimeSinceStartup - PowerUpBag.ActiveDeffensePowerUp[i].activationTime))) / PowerUpBag.ActiveDeffensePowerUp[i].duration);
                    PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color = c;
                }
                else
                {
                    Color c = PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color;
                    c.a = 0.0f;
                    PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color = c;
                }

                if (PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color.a <= 0)
                {
                    PowerUpBag.ActiveDeffensePowerUp[i] = null;
                    if (i != 0)
                    {
                        PowerUpBag.ActiveDeffensePowerUp[i - 1].activationTime = Time.realtimeSinceStartup;
                        lives = 3;
                    }
                    else lives = 2;
                }
            }
            else if (PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color.a > 0 && (i == 3 || PowerUpBag.ActiveDeffensePowerUpSprite[i + 1].GetComponent<Image>().color.a <= 0))
            {
                if (PowerUpBag.ActiveDeffensePowerUp[i] != null) PowerUpBag.ActiveDeffensePowerUp[i].activationTime = Time.realtimeSinceStartup;
                else
                {
                    Color c = PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color;
                    c.a = (((PowerUpBag.ActiveDeffensePowerUp[i].duration - (Time.realtimeSinceStartup - PowerUpBag.ActiveDeffensePowerUp[i].activationTime))) / PowerUpBag.ActiveDeffensePowerUp[i].duration);
                    PowerUpBag.ActiveDeffensePowerUpSprite[i].GetComponent<Image>().color = c;
                }
            }

            if (PowerUpBag.ActiveAtackPowerUp[i] != null && PowerUpBag.ActiveAtackPowerUp[i].activationTime != 0 && PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color.a > 0)
            {
                if (i != 0 && PowerUpBag.ActiveAtackPowerUp[i - 1].activationTime != 0) PowerUpBag.ActiveAtackPowerUp[i - 1].activationTime = 0.0f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Instantiate(Resources.Load("ResPrefabs/Projectile"));
                    PowerUpBag.ActiveAtackPowerUp[i].duration *= 0.75f;
                }
                Color c = PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color;
                c.a = (((PowerUpBag.ActiveAtackPowerUp[i].duration - (Time.realtimeSinceStartup - PowerUpBag.ActiveAtackPowerUp[i].activationTime))) / PowerUpBag.ActiveAtackPowerUp[i].duration);
                PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color = c;

                if (PowerUpBag.ActiveAtackPowerUp[i] != null && PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color.a <= 0)
                {
                    PowerUpBag.ActiveAtackPowerUp[i] = null;
                    if (i != 0) PowerUpBag.ActiveAtackPowerUp[i - 1].activationTime = Time.realtimeSinceStartup;
                }
            }
            else if (PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color.a > 0 && (i == 3 || PowerUpBag.ActiveAtackPowerUpSprite[i + 1].GetComponent<Image>().color.a <= 0))
            {
                if (PowerUpBag.ActiveAtackPowerUp[i] != null) PowerUpBag.ActiveAtackPowerUp[i].activationTime = Time.realtimeSinceStartup;
                else
                {
                    Color c = PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color;
                    c.a = (((PowerUpBag.ActiveAtackPowerUp[i].duration - (Time.realtimeSinceStartup - PowerUpBag.ActiveAtackPowerUp[i].activationTime))) / PowerUpBag.ActiveAtackPowerUp[i].duration);
                    PowerUpBag.ActiveAtackPowerUpSprite[i].GetComponent<Image>().color = c;
                }
            }
        }
    }

    private void updatePowers()
    {
        for (int i = PowerUpBag.ActiveAtackPowerUpSprite.Length - 1; i >= 0; i--)
        {
            if (PowerUpBag.ActiveSpeedPowerUp[i] != null)
            {
                if(lives > 1)
                {
                    MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed * 2;
                    HIGH_em.enableEmission = true;
                    MED_em.enableEmission = false;
                    LOW_em.enableEmission = false;
                }
                else
                {
                    MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed;
                    HIGH_em.enableEmission = false;
                    MED_em.enableEmission = true;
                    LOW_em.enableEmission = false;
                }
            }
            else if(i == 0)
            {
                if (Time.realtimeSinceStartup - turbo.turboRef > turbo.turboMax)
                {
                    if (lives == 1)
                    {
                        MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed / 2;
                        HIGH_em.enableEmission = false;
                        MED_em.enableEmission = false;
                        LOW_em.enableEmission = true;
                    }
                    else
                    {
                        MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed;
                        HIGH_em.enableEmission = false;
                        MED_em.enableEmission = true;
                        LOW_em.enableEmission = false;
                    }
                }
                else
                {
                    if(lives == 1)
                    {
                        MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed;
                        HIGH_em.enableEmission = false;
                        MED_em.enableEmission = true;
                        LOW_em.enableEmission = false;
                    }
                    else
                    {
                        MapMovement.Instance.mapSpeed = MapMovement.Instance.initialSpeed * 2;
                        HIGH_em.enableEmission = true;
                        MED_em.enableEmission = false;
                        LOW_em.enableEmission = false;
                    }
                }
            }

            if (lives == 3) shield.SetActive(true);
            else shield.SetActive(false);
        }
    }

    public void killPlayer()
    {
        SceneManager.LoadScene("speed");
    }
}
