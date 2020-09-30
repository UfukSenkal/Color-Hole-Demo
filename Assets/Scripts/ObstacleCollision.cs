using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
 

    void OnCollisionEnter(Collision col)
    {
        //Üst üste gelen objeler içinde obstacle olursa değdiği diğer objeleri de obstacle yapıyoruz levellar imkansız olmasın diye
        if (col.gameObject.tag == "Obstacle")
        {
            this.GetComponent<MeshRenderer>().material = GameController.Instance.obstacleMat;
            this.tag = "Obstacle";

           
        }
       


    }
   
   
    
}
