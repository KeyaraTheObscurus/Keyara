using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerPrefs.SetFloat("position.x", transform.position.x);
            PlayerPrefs.SetFloat("position.y", transform.position.y);
            PlayerPrefs.SetFloat("position.z", transform.position.z);
        }
    }
}