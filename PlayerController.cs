using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform playerModel;
    private bool isBoosting = false;
    private bool isAlive = true;

    private Rigidbody _rigidbody;

    [Header("Settings")]
    public bool joystick = false;

    [Space]
    [Header("Parameters")]
    public float xySpeed = 18f;
    public float lookSpeed = 340f;
    public float rotationSpeed = 2f;
    public float forwardSpeed = 6f;
    public float targetDistance = 1f;
    public float standardFov = 60f;
    private float currentFov;
    public float maxFov = 75f;
    public float fovTransitionSpeed = 120f;
    public float shootOffset = 2f;
    public float boomSize = 6f;
    public float interval = 0.05f;
    private float timer = 0.0f;

    [Space]
    [Header("Camera")]
    public Camera cam;

    [Space]
    [Header("Public References")]
    public Transform aimTarget;
    public Transform mouseCursor;
    public CinemachineDollyCart dolly;
    public Transform cameraParent;
    public CinemachineVirtualCamera virtualCamera;
    public PlayerBullet bulletPrefab;
    public CameraShake cameraShake;
    public ParticleSystem boostParticles;
    public ParticleSystem playerBoom;
    public TextMeshProUGUI deadText;
    public TextMeshProUGUI debugText;

    public AudioSource playerSound;
    public AudioClip shootSound;
    public AudioClip boostSound;
    public AudioClip crashSound;
    private float shootVolume;
    private Vector3 mousePos;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        deadText.text = "";
        currentFov = standardFov;
        Debug.Log("Starting da game!");
        playerModel = transform.GetChild(0);
        SetSpeed(forwardSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        virtualCamera.m_Lens.FieldOfView = currentFov;

        // does not work with 320x180 rendertexture - how to make it work?
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float h = joystick ? Input.GetAxis("Horizontal") : Mathf.Clamp01(mousePos.x) - 0.5f;
        float v = joystick ? Input.GetAxis("Vertical") : Mathf.Clamp01(mousePos.y) - 0.5f;

        debugText.text =
            "h: " + Mathf.Round(h * 100f) / 100f + " v: " + Mathf.Round(v * 100f) / 100f;

        if (isAlive)
        {
            LocalMove(h, v, xySpeed);
            // RotationLook(h, v, lookSpeed);
            // HorizontalLean(playerModel, h, 40, .1f);
            if (!joystick)
            {
                transform.LookAt(mouseCursor.position);
            }
        }

        if (Input.GetButtonDown("Fire2") && isAlive)
        {
            // float newFov;
            Debug.Log("Wheeeee!");
            isBoosting = true;
            SetSpeed(forwardSpeed * 2);
            boostParticles.Play();
        }

        if (Input.GetButtonUp("Fire2") && isAlive)
        {
            isBoosting = false;
            SetSpeed(forwardSpeed);
            boostParticles.Stop();
        }

        if (Input.GetButtonDown("Fire1") && isAlive)
        {
            Debug.Log("Shooting!");
            Shoot();
        }

        if (isBoosting && isAlive)
        {
            StartCoroutine(cameraShake.Shake(.15f, .15f));

            if (timer >= interval)
            {
                playerSound.PlayOneShot(boostSound, 0.5f);
                timer = 0f;
            }

            if (currentFov < maxFov)
            {
                currentFov += fovTransitionSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (currentFov > standardFov)
            {
                currentFov -= fovTransitionSpeed * Time.deltaTime;
            }
        }
        timer += Time.deltaTime;
    }

    void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        if (joystick)
        {
            ClampPosition();
        }
        else
        {
            ClampPosition();
        }
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, targetDistance);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(aimTarget.position),
            Mathf.Deg2Rad * speed * Time.deltaTime
        );
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngles = target.localEulerAngles;
        target.localEulerAngles = new Vector3(
            targetEulerAngles.x,
            targetEulerAngles.y,
            Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime)
        );
    }

    void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with something! Boom!");
        if (collision.gameObject.CompareTag("Ground") && isAlive)
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("Player is dead!");
        deadText.text = "D E A D";
        playerSound.PlayOneShot(crashSound, 1f);

        isAlive = false;
        boostParticles.Stop();
        SetSpeed(0);
        playerBoom.Play();
        StartCoroutine(cameraShake.Shake(.8f, .4f));
        _rigidbody.useGravity = true;
        Vector3 force = new Vector3(0f, boomSize, 0f);
        _rigidbody.AddForce(force, ForceMode.Impulse);
        Invoke("PlayerRespawn", 5f);
    }

    public void PlayerRespawn()
    {
        Debug.Log("Player is alive!");
        deadText.text = "";
        isAlive = true;
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        transform.position = dolly.transform.position;
        playerModel.localPosition = new Vector3(0f, 0f, 0f);
        transform.localRotation = Quaternion.identity;
        playerModel.localRotation = Quaternion.identity;
        SetSpeed(forwardSpeed);
    }

    private void Shoot()
    {
        shootVolume = Random.Range(0.4f, 0.5f);
        // how to randomize pitch without messing up engine sounds??
        // playerSound.pitch = (Random.Range(0.8f, 1f));

        playerSound.PlayOneShot(shootSound, shootVolume);
        Vector3 shootPos = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + shootOffset
        );
        PlayerBullet bullet = Instantiate(this.bulletPrefab, shootPos, this.transform.rotation);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
        bullet.Project(this.transform.forward);
        StartCoroutine(cameraShake.ShakeBack(.15f, .4f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);
    }
}
