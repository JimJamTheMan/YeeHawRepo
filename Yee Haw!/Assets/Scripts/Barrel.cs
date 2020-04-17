using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrel : MonoBehaviour
{
    //Calls Other things 
    public GameObject Gun;
    public Player_Controller_2D Player_Controller_2D;
    
    public int maxStoredAmmo;
    public int maxAmmo = 6;
    private int currentAmmo;
    public Text currentAmmoDisplay; 
    public Text maxStoredAmmoDisplay;

    [SerializeField] private Image zeroBullets;
    [SerializeField] private Image oneBullets;
    [SerializeField] private Image twoBullets;
    [SerializeField] private Image threeBullets;
    [SerializeField] private Image fourBullets;
    [SerializeField] private Image fiveBullets;
    [SerializeField] private Image sixBullets;



     

    //public Image[] cylinder;
    
    //[SerializeField] private Animation sixBullets;


   
    bool isReloading = false; 

    public int maxCocking = 1;
    private int currentCocking = 1;
    
    bool isCocking = false; 


    // Sets Animator
    Animator animator;

    [SerializeField]
    private Transform barrelTip;

    [SerializeField]
    private GameObject bullet;

    private Vector2 lookDirection;
    private float lookAngle;

    bool isFliped = false;

    SpriteRenderer spriteRenderer2;

    void Start(){


    spriteRenderer2 = GetComponent<SpriteRenderer>();
     animator = GetComponent<Animator>();
     animator.Play("New Animation");
        
        isReloading = false;
        isCocking = false;
        //StartCoroutine (Cocking());

           

        if (currentAmmo == - 1); 
            currentAmmo = maxAmmo;

             zeroBullets.enabled = false;
             oneBullets.enabled = false;
             twoBullets.enabled = false;
             threeBullets.enabled = false;
             fourBullets.enabled = false;   
             fiveBullets.enabled = false;
             sixBullets.enabled = false;
    }


   void FixedUpdate()
    {
     
        currentAmmoDisplay.text = currentAmmo.ToString(); 
        maxStoredAmmoDisplay.text = maxStoredAmmo.ToString();
        
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
 
        difference.Normalize();
        
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (rotationZ < -90 || rotationZ > 90)
        {
            
            if(Gun.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
                Player_Controller_2D.PlayerFlip();
            }



			 else if (Gun.transform.eulerAngles.y == 180){

              Player_Controller_2D.UnPlayerFlip();
            transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            

            }
            
        }
 
       else

       Player_Controller_2D.UnPlayerFlip();
        
        HudReload();

       if (isReloading)
           {
           return; 
           }
       
       if (isCocking)
           {
           return; 
           }
      
      if (maxStoredAmmo + currentAmmo == 0f && Input.GetMouseButtonDown(0 ))
        {
             StartCoroutine (Cocking());
             animator.Play("NotCocked");
            
            return; 
		}
      
      
      if (maxStoredAmmo + currentAmmo == 0f)
        {
            animator.Play("NotCocked");
            return; 
		}
       
       
       
       if (maxStoredAmmo + currentAmmo == 0f)
        {

            return; 
		}

       if (currentAmmo <= 0f)
        {
            StartCoroutine (Reload());  
            return; 
		}

       if (Input.GetKeyDown("r") && maxStoredAmmo >=1f)
        {
            StartCoroutine (Reload());  
            return; 
		}

        if (currentAmmo <= 0f)
        {
            StartCoroutine (Reload());
            return; 
		}
   

        else

        if (currentCocking <= 0f)
        {
            StartCoroutine (Cocking());  
            return; 
		}


        if (Input.GetMouseButtonDown(0))
        {
           StartCoroutine (Cocking()); 
           FireBullet();

        }


    }

    void FireBullet()
    {
       
       currentAmmo--;
       currentCocking--;
       
      SoundManagerScript.PlaySound ("RevolverShot");
      Debug.Log("Shooting");
      
      animator.Play("RevolverShooting");
      
      StartCoroutine (Cocking());
        

        GameObject firedBullet = Instantiate(bullet, barrelTip.position, barrelTip.rotation);
        firedBullet.GetComponent<Rigidbody2D>().velocity = barrelTip.up * 30f;

    }

    IEnumerator Reload()
    {
     
        isReloading = true;

        Debug.Log("Reloading");
        
        animator.Play("NotCocked");

        animator.Play("RevolverReloading");
        
            while (currentAmmo < maxAmmo)
            {
 
                SoundManagerScript.PlaySound ("RevolverReloading");
                
                yield return new WaitForSeconds(0.333333333f);
                maxStoredAmmo = maxStoredAmmo - 1;
                maxStoredAmmoDisplay.text = maxStoredAmmo.ToString();
                
                currentAmmoDisplay.text = currentAmmo.ToString();

                if (maxStoredAmmo == 0)
                {
                        currentAmmo = currentAmmo + 1; 
                        break; 
                
				}
                
                else
                currentAmmo = currentAmmo + 1;

             }

              SoundManagerScript.PlaySound ("GunCocking");
              yield return new WaitForSeconds(.3f);
              animator.Play("New Animation");

            isReloading = false;
    
	}

        IEnumerator Cocking()
    {
        isCocking = true;
        //Debug.Log("Cocking");
     
        yield return new WaitForSeconds(.3f);


        SoundManagerScript.PlaySound ("GunCocking");
        
        animator.Play("RevolverCocking");

        yield return new WaitForSeconds(.05f);
        animator.Play("New Animation");
        currentCocking = maxCocking;
        isCocking = false;
        }

        void HudReload()
        {
        
            switch (currentAmmo)
            {
                case 0:

                zeroBullets.enabled = true;
                oneBullets.enabled = false;
                twoBullets.enabled = false;
                threeBullets.enabled = false;
                fourBullets.enabled = false;
                fiveBullets.enabled = false;
                sixBullets.enabled = false;
                break;
                
                case 1:
                zeroBullets.enabled = false;
                oneBullets.enabled = true;
                twoBullets.enabled = false;
                threeBullets.enabled = false;
                fourBullets.enabled = false;
                fiveBullets.enabled = false;
                sixBullets.enabled = false;
                fiveBullets.enabled = false;

                
                break;

                case 2:

                zeroBullets.enabled = false;
                oneBullets.enabled = false;
                twoBullets.enabled = true;
                threeBullets.enabled = false;
                fourBullets.enabled = false;
                fiveBullets.enabled = false;
                sixBullets.enabled = false;
                break;
                
                case 3:
                zeroBullets.enabled = false;
                oneBullets.enabled = false;
                twoBullets.enabled = false;
                threeBullets.enabled = true;
                fourBullets.enabled = false;
                fiveBullets.enabled = false;
                sixBullets.enabled = false;
                break;

                case 4:

                zeroBullets.enabled = false;
                oneBullets.enabled = false;
                twoBullets.enabled = false;
                threeBullets.enabled = false;
                fourBullets.enabled = true;
                fiveBullets.enabled = false;
                sixBullets.enabled = false;
                break;
                
                case 5:
                zeroBullets.enabled = false;
                oneBullets.enabled = false;
                twoBullets.enabled = false;
                threeBullets.enabled = false;
                fourBullets.enabled = false;
                fiveBullets.enabled = true;
                sixBullets.enabled = false;
                break;
                
                case 6:
                zeroBullets.enabled = false;
                oneBullets.enabled = false;
                twoBullets.enabled = false;
                threeBullets.enabled = false;
                fourBullets.enabled = false;
                fiveBullets.enabled = false;
                sixBullets.enabled = true;
                break;


			}


        }




}
