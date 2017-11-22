using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    //public AudioClip powerupSound;
    public bool highJump;
    public float powerupLength;

    private PowerUpManager powerUpManager;
    //ParticleSystem[] particleSystem;
    //Collider col;


    // Use this for initialization
    void Start ()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        //particleSystem = GetComponentsInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            highJump = true;
            //GetComponent<AudioSource>().PlayOneShot(powerupSound);
            AudioManager.Instance.Play("PowerUp", true);
            powerUpManager.ActivatePowerup(highJump, powerupLength);
            //FlipStates();
        }
        gameObject.SetActive(false);
    }

    //public void FlipStates()
    //{
    //    col.enabled = !col.enabled;
    //    foreach (ParticleSystem pSys in particleSystem)
    //    {
    //        if (pSys.isPlaying) pSys.Stop();
    //        if (pSys.isStopped) pSys.Play();
    //    }
    //}
}
