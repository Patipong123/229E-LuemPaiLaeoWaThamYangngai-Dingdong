using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider chargeSlide;

    public float maxChargeTime = 3f;
    public float maxForce = 30f;
    public Vector3 jumpDirection = new Vector3(0, 1, 1);
    public int maxJumps = 3;

    private float chargeTimer = 0f;
    private bool isCharging = false;
    private Rigidbody rb;
    private bool hasJumped = false;
    

    public float minY = -10f;   
    public float maxY = 50f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (chargeSlide != null)
            chargeSlide.maxValue = maxForce;

    }

    void Update()
    {

        if (transform.position.y < minY || transform.position.y > maxY) // out of map = dead
        {
            
            ReloadScene(); // load scene
        }

        if (isCharging && chargeSlide != null) //logic chargeSlide
        {
            float chargePercent = chargeTimer / maxChargeTime;
            float currentForce = chargePercent * maxForce;
            chargeSlide.value = currentForce;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !hasJumped) // 1. hold spacebar to start charging and set time
        {
            isCharging = true;
            chargeTimer = 0f;
            hasJumped = true;
        }

        if (Input.GetKey(KeyCode.Space) && isCharging) // 2. if hold spacebar count time charge
        {
            chargeTimer += Time.deltaTime;
            chargeTimer = Mathf.Min(chargeTimer, maxChargeTime);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging) // 3. if leave spacebar run fuction Force and set slide = 0
        {
            Force();
            if (chargeSlide != null)
                chargeSlide.value = 0;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Ground")) 
        {
            hasJumped = false;
        }

        if (other.gameObject.CompareTag("BlackHole"))
        {
            ReloadScene();
        }



    }

    void ReloadScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    void Force() 
    {
        float chargePercent = chargeTimer / maxChargeTime; 
        float force = chargePercent * maxForce; 

        rb.AddForce(jumpDirection.normalized * force, ForceMode.Impulse); 

        isCharging = false;
        hasJumped = true;
        

        Debug.Log(force);
        
    }






}
