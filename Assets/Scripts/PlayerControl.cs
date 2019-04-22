using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Coin")){
            AudioManager.instance.PlayCoinPickupSound(other.gameObject);
            SFXManager.instance.ShowCoinParticles(other.gameObject);
            Destroy(other.gameObject);
            LevelManager.instance.IncrementCoinCount();
            
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Enemies")){
            KillPlayer();
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Forbidden")){
           KillPlayer(); 
        }
    }
    void KillPlayer(){
        Camera.main.GetComponentInChildren<AudioSource>().mute = true;
        LevelManager.instance.SetTapeSpeed(0);
        AudioManager.instance.PlaySoundFail(gameObject);
        SFXManager.instance.ShowDieParticles(gameObject);
        Destroy(gameObject);
    }
}
