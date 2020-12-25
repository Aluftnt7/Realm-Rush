using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [Range(100, 200)]
    [SerializeField] float playerHealth = 200f;
    [SerializeField] float healthDecrease = 0.001f;
    [SerializeField] Text healthText;
    [SerializeField] Transform ParticleParenTransform;

    [SerializeField] AudioClip playerDamageSfx;



    bool isSmokePlaying = false;
    bool isFirePlaying = false;

    [SerializeField] ParticleSystem smokerParticalePrefab;
    [SerializeField] ParticleSystem fireParticalPrefab;
    // Start is called before the first frame update
    void Start()
    {
        healthText.text = (Mathf.Round(playerHealth)).ToString();
    }

  

    private void HandleFireAndSmoke()
    {
   
        if (playerHealth <= 130f && !isSmokePlaying)
        {
            var vfx = Instantiate(smokerParticalePrefab, ParticleParenTransform.transform.position, Quaternion.identity);
            vfx.transform.parent = ParticleParenTransform.transform;
            vfx.Play();
            isSmokePlaying = true;
        }
       else if(playerHealth <= 80f && !isFirePlaying)
        {
            var vfx = Instantiate(fireParticalPrefab, ParticleParenTransform.transform.position, Quaternion.identity);
            vfx.transform.parent = ParticleParenTransform.transform;
            vfx.Play();
            isFirePlaying = true;

        }
    }

    private void OnTriggerStay(Collider other)
   {
        PlayHittingSound();
        playerHealth -= healthDecrease;
        healthText.text = (Mathf.Round(playerHealth)).ToString();
        HandleFireAndSmoke();

    }

    private void PlayHittingSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource.isPlaying)
        {
            //do nothing
        }
        else
        {
            audioSource.PlayOneShot(playerDamageSfx);
        }
    }
}
