using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
  public Transform player;
  public GameObject control;
  public GameObject cameraMain;
  public Vector3 offset;
  
  void Update () 
  {
        control.transform.position = new Vector3 (player.position.x, player.position.y, offset.z);
        cameraMain.transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z);
  }
}
