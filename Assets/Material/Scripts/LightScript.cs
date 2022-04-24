using UnityEngine;
using UnityEngine.SceneManagement;

public class LightScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene("Zone_A1");
        }
    }
}