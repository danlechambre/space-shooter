using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameState gameState;
    private UIManager ui;
    
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject explosionVFXprefab;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip fireClip;

    [SerializeField] private int health = 100;
    [SerializeField] private float timeToNextFire;
    [SerializeField] private float minTimeBetweenShots = 0.1f;
    [SerializeField] private float maxTimeBetweenShots = 0.5f;
    [SerializeField] private float projectileSpeed = 8f;
    
    private Vector3 listenerPos;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
        ui = FindObjectOfType<UIManager>();
        listenerPos = Camera.main.transform.position;

        StartCoroutine(NextFire());
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = Vector2.down * projectileSpeed;
        AudioSource.PlayClipAtPoint(fireClip, listenerPos, 0.6f);
    }

    private IEnumerator NextFire()
    {
        while (true)
        {
            timeToNextFire = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            yield return new WaitForSeconds(timeToNextFire);
            Fire();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DamageDealer>())
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            ProcessHit(damageDealer);
            damageDealer.Hit();
        }
        
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        int damage = damageDealer.GetDamage();
        health -= damage;

        FindObjectOfType<Player>().AddToScore(damage);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject explosionVFX = Instantiate(explosionVFXprefab, transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(explosionVFX, 0.5f);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathClip, listenerPos, 1.0f);
    }
}
