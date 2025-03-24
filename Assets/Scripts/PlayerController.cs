using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float maxChargeTime = 3f;
    public float maxForce = 20f;
    public Vector3 jumpDirection = new Vector3(0, 1, 1);
    public int maxJumps = 3;

    private float chargeTimer = 0f;
    private bool isCharging = false;
    private Rigidbody rb;
    private bool hasJumped = false;
    private int jumpCount = 0;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        while (jumpCount < maxJumps) 
        {

            if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
            {
                isCharging = true;
                chargeTimer = 0f;
            }

            if (Input.GetKey(KeyCode.Space) && isCharging)
            {
                chargeTimer += Time.deltaTime;
                chargeTimer = Mathf.Min(chargeTimer, maxChargeTime);
            }

            if (Input.GetKeyUp(KeyCode.Space) && isCharging)
            {
                float chargePercent = chargeTimer / maxChargeTime;
                float appliedForce = chargePercent * maxForce;

                rb.AddForce(jumpDirection.normalized * appliedForce, ForceMode.Impulse);

                isCharging = false;
                hasJumped = true;
                jumpCount++;

                Debug.Log(appliedForce);
                Debug.Log(jumpCount);
            }
            if (jumpCount >= maxJumps) 
            {
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("Ground")) 
        {
            hasJumped = false;
        }

        


    }

    


}
