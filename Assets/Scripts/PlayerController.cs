using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    public float minY = -10f;   
    public float maxY = 50f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        if (transform.position.y < minY || transform.position.y > maxY)
        {
            
            ReloadScene();
        }

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
            Gravity();
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Ground")) 
        {
            hasJumped = false;
        }

    }

    void ReloadScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    void Gravity() 
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






}
