using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float fallLimit = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update(){
        if(rb.velocity.y < fallLimit){
            anim.SetInteger("State", 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Coin")){
            AudioManager.instance.PlayCoinPickupSound(other.gameObject);
            SFXManager.instance.ShowCoinParticles(other.gameObject);
            Destroy(other.gameObject);
            LevelManager.instance.IncrementCoinCount();
            impulse(10);
            
        }
         if (other.gameObject.CompareTag("Gift")){
           StopMusicAndTape();
           AudioManager.instance.PlaySoundLevelComplete(gameObject);
           DestroyPlayer();
           LevelManager.instance.ShowLevelCompletePanel();
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Enemies")){
            KillPlayer();
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Forbidden")){
           KillPlayer(); 
        }
    }
    void StopMusicAndTape(){
        Camera.main.GetComponentInChildren<AudioSource>().mute = true;
        LevelManager.instance.SetTapeSpeed(0);
    }
    void KillPlayer(){
        StopMusicAndTape();
        AudioManager.instance.PlaySoundFail(gameObject);
        SFXManager.instance.ShowDieParticles(gameObject);
        DestroyPlayer();
        LevelManager.instance.ShowGameOverPanel();
    }
    void impulse(float force)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * force, ForceMode2D.Impulse);
        anim.SetInteger("State", 1);
    }
    void DestroyPlayer(){
        Camera.main.GetComponent<CameraFollow>().TurnOff();
        Destroy(gameObject);

    }
}
