using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] private Animator animator;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem hitDeathParticlePrefab;


    [SerializeField] bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }


    // Update is called once per frame
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if(hitPoints <= 0 && !isDead)
        {
            isDead = true;
            KillEnemy();
        }
    }

   

    void ProcessHit()
    {
        hitPoints--;
        if (!isDead)
        {
             hitParticlePrefab.Play();
        }
    }

    private void KillEnemy()
    {
        SendMessage("OnPlayerDeath");
        animator.SetBool("isDead", isDead);  
        Invoke("HandleEnemyDeathFx", 3.0f);
       
    }

    private void HandleEnemyDeathFx()
    {
        var vfx = Instantiate(hitDeathParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        vfx.Play();
    }
}
