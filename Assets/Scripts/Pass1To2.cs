using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pass1To2 : MonoBehaviour
{
    public string nextSceneName = "Map2";
    public float waitTime = 3f;

    private Coroutine timerCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            
            timerCoroutine = StartCoroutine(WaitAndPass());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
                Debug.Log("Cancel Time Count");
            }
        }
    }

    private IEnumerator WaitAndPass()
    {
        Debug.Log("CheckPoint TimeCount.....");
        yield return new WaitForSeconds(waitTime);

        Debug.Log("You Pass");
        SceneManager.LoadScene(nextSceneName);
    }
}
