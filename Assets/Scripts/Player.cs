using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private AudioSource audioSource;
    private SceneLoader sceneLoader;
    private GameState gameState;

    [Header("Player Movement")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float paddingX = 0.02f;
    [SerializeField] private float paddingY = 0.03f;
    [SerializeField] private float paddingTop = 0.1f;

    [Header("Player Weapon")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed = 20.0f;
    [SerializeField] private float fireCooldownPeriod = 0.15f;
    [SerializeField] private AudioClip deathClip;

    private Vector3 boundsLowerLeft;
    private Vector3 boundsUpperRight;
    private Vector3 listenerPos;

    private int health = 500;
    private int score = 0;
    private bool canFire = true;
    

    void Start()
    {
        SetPlayerBounds();
        listenerPos = Camera.main.transform.position;
        audioSource = GetComponent<AudioSource>();

        sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
        gameState = GameObject.Find("GameState").GetComponent<GameState>();

        gameState.UpdatePlayerHealth(health);
        gameState.UpdatePlayerScore(score);
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1") && canFire)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            audioSource.Play();
            
            laser.GetComponent<Rigidbody2D>().velocity = Vector2.up * projectileSpeed;
            StartCoroutine(FireCooldown());
        }
    }

    IEnumerator FireCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(fireCooldownPeriod);
        canFire = true;
    }

    private void SetPlayerBounds()
    {
        Camera gameCamera = Camera.main;
        boundsLowerLeft = gameCamera.ViewportToWorldPoint(new Vector3((0 + paddingX), (0 + paddingY), 0));
        boundsUpperRight = gameCamera.ViewportToWorldPoint(new Vector3((1 - paddingX), (1 - (paddingY + paddingTop)), 0));
    }

    private void Move()
    {
        float xInput = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float yInput = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Translate(xInput, yInput, 0);

        float xClamp = Mathf.Clamp(transform.position.x, boundsLowerLeft.x, boundsUpperRight.x);
        float yClamp = Mathf.Clamp(transform.position.y, boundsLowerLeft.y, boundsUpperRight.y);

        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
        damageDealer.Hit();
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        gameState.UpdatePlayerHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathClip, listenerPos);
        sceneLoader.LoadGameOver(2.0f);
        Destroy(gameObject);
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        gameState.UpdatePlayerScore(score);
    }
}
