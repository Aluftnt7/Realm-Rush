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
    [SerializeField] AudioClip enemyDeathSfx;
    [SerializeField] AudioClip enemyHitSfx;
    [SerializeField] AudioClip enemyExplodes;


    [SerializeField] public bool isDead;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
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
            myAudioSource.PlayOneShot(enemyHitSfx, 0.4f);
             hitParticlePrefab.Play();
        }
    }

    private void KillEnemy()
    {
        DeadEnemiesCounter.instace.UpdateDeadEnemyCount();
        SendMessage("OnPlayerDeath");
        animator.SetBool("isDead", isDead);  
        Invoke("HandleEnemyDeathFx", 2.5f);
        myAudioSource.PlayOneShot(enemyDeathSfx, 0.4f);
    }

    

    private void HandleEnemyDeathFx()
    {
        var vfx = Instantiate(hitDeathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();
        float destroyDelay = vfx.main.duration;
        AudioSource.PlayClipAtPoint(enemyExplodes, Camera.main.transform.position, 0.08f);//different audio soutce from the one on the game object (that wont play cause the component is destroyed, plays in 3d instead of 2d)
        Destroy(gameObject);
        Destroy(vfx.gameObject, destroyDelay);
    }
}
