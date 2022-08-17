using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private GameManager gameManager;
    private Vector3 movement;
    private Rigidbody rb;

    [Header("Partiküller")]
    [SerializeField] private ParticleSystem smoke;
    [SerializeField] private ParticleSystem yokOlma;

    [Header("Diðer")]
    [SerializeField] private float speed = 5;
    [SerializeField] private Transform bitisKapisiNoktasi;
    public bool goFinish;

    public float mesafe;

    void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        goFinish = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        #region Computer_Controller
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        movement = new Vector3(x, 0, z);
        transform.position += movement;
        #endregion

        #region Phone_Controller
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            float mobilX = touch.deltaPosition.x * speed * Time.deltaTime;
            float mobilZ = touch.deltaPosition.y * speed * Time.deltaTime;

            movement = new Vector3(mobilX, 0, mobilZ);
            //transform.position += movement;


            rb.velocity += movement / 4;
        }

        #endregion





        mesafe = Vector3.Distance(bitisKapisiNoktasi.position, transform.position);

        if (goFinish && mesafe <= 1.5f)
        {
            //gameManager.bolumGec = true;
            GameManager.instance.bolumGec = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<AnimationScript>().enabled)
        {
            smoke.Play();
            if (GameManager.instance.vibrationSlider)
            {
                Vibrator.Vibrate(9);

            }
            Destroy(other.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            yokOlma.transform.SetParent(transform.parent);
            yokOlma.Play();
            if (GameManager.instance.vibrationSlider)
            {
                Vibrator.Vibrate(80);

            }
            Destroy(gameObject);

            GameManager.instance.restartPanel.SetActive(true);


        }
        else
        {
            if (GameManager.instance.vibrationSlider)
            {
                Vibrator.Vibrate(20);

            }

        }

    }
}
