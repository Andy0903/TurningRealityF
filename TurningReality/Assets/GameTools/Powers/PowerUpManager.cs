using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PowerUpManager : MonoBehaviour
{
    private bool highJump;
    private bool powerupActive = false;

    private float powerupLengthCounter;
    private float normalJump;
    private float jumpMultiplier = 2.5f;

    private ThirdPersonCharacter movement;
    PowerUps powerUps;

    
	// Use this for initialization
	void Start ()
    {
        movement = FindObjectOfType<ThirdPersonCharacter>();
        powerUps = FindObjectOfType<PowerUps>();

        normalJump = movement.m_JumpPower;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (powerupActive)
        {
            powerupLengthCounter -= Time.deltaTime;

            if(highJump)
            {
                movement.m_JumpPower = normalJump * jumpMultiplier;
                //FlipStates();
                //Debug.Log(movement.jumpForce);
                
            }
            
            if(powerupLengthCounter <= 0)
            {
                movement.m_JumpPower = normalJump;
                powerupActive = false;
            }
        }
		
	}

    public void ActivatePowerup(bool jump, float time)
    {
        highJump = jump;
        powerupLengthCounter = time;
        normalJump = movement.m_JumpPower;
        powerupActive = true;
    }
}
