using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip RevolverReloadingSound, RevolverCockingSound, RevolverShotSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update

    void Start()
    {
        RevolverReloadingSound = Resources.Load<AudioClip> ("RevolverReloading");
        RevolverCockingSound = Resources.Load<AudioClip>("GunCocking");
        RevolverShotSound = Resources.Load<AudioClip>("RevolverShot");
        


        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "RevolverReloading":
                Debug.Log ("Playing Reload Sound...");
                audioSrc.PlayOneShot (RevolverReloadingSound);
                break;


            case "GunCocking":
                //Debug.Log("Playing Reload Sound...");
                audioSrc.PlayOneShot (RevolverCockingSound);
                break;

            case "RevolverShot":
                //Debug.Log("Playing Shoot Sound...");
                audioSrc.PlayOneShot (RevolverShotSound);
                break;
        }

    }

}