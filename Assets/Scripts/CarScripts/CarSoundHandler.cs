using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundHandler : MonoBehaviour
{

    public AudioSource carEngineSound; // This variable stores the sound of the car engine.
    public AudioSource tireScreechSound; // This variable stores the sound of the tire screech (when the car is drifting).
    float initialCarEngineSoundPitch; // Used to store the initial pitch of the car engine sound.

    CarController Car;
    void Start()
    {
        Car = GetComponent<CarController>();
        initialCarEngineSoundPitch = carEngineSound.pitch;
    }

    void Update()
    {
        CarSound();
    }



    public void CarSound()
    {
        
          if(carEngineSound != null){
            float engineSoundPitch = initialCarEngineSoundPitch + (Mathf.Abs(Car.carRigidbody.velocity.magnitude) / 25f);
            carEngineSound.pitch = engineSoundPitch;
          }
          if((Car.isDrifting) || (Car.isTractionLocked && Mathf.Abs(Car.carSpeed) > 12f)){
            if(!tireScreechSound.isPlaying){
              tireScreechSound.Play();
            }
          }else if((!Car.isDrifting) && (!Car.isTractionLocked || Mathf.Abs(Car.carSpeed) < 12f)){
            tireScreechSound.Stop();
          }
        
      }
}
