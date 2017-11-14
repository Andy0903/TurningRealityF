using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private bool highJump;
    private bool powerupActive = false;

    private float powerupLengthCounter;
    private float normalJump;
    private float jumpMultiplier = 2.5f;

    private Movement movement;
    PowerUps powerUps;

    
	// Use this for initialization
	void Start ()
    {
        movement = FindObjectOfType<Movement>();
        powerUps = FindObjectOfType<PowerUps>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (powerupActive)
        {
            powerupLengthCounter -= Time.deltaTime;

            if(highJump)
            {
                movement.jumpForce = normalJump * jumpMultiplier;
                //FlipStates();
                Debug.Log(movement.jumpForce);
                
            }
            
            if(powerupLengthCounter <= 0)
            {
                movement.jumpForce = normalJump;
                powerupActive = false;
            }
        }
		
	}

    public void ActivatePowerup(bool jump, float time)
    {
        highJump = jump;
        powerupLengthCounter = time;
        normalJump = movement.jumpForce;
        powerupActive = true;
    }
}
