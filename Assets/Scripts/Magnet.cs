using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Magnet : MonoBehaviour
{
    #region Singleton class: Magnet

    public static Magnet Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
    }

    #endregion

    public bool clearStage;
    // Start is called before the first frame update
    void Start()
    {
        clearStage = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        //Magnet kullanmak iç içe geçip sıkışabilen ve etrafa dağılabilen bir bug oluşturabilir diye collider kapatıp aşağı düşürmeyi tercih ettim
        if ((clearStage || !Level.Instance.stageClear) && (col.tag == "Object" || col.tag == "Obstacle"))
        {
            col.gameObject.GetComponent<Collider>().isTrigger = true;
           
            if (col.name == "First" && col.tag == "Object")
            {
               
               
                GameController.Instance.stageOneCount--;
                GameController.Instance.slider.value++;
            }
            if (col.name == "Second" && col.tag == "Object")
            {
               
              
                GameController.Instance.stageTwoCount--;
                GameController.Instance.slider.value++;
            }
            if (col.tag == "Obstacle")
            {
                Game.isGameover = true;
                CameraController.Instance.ShakeCamera();
                UIManager.Instance.buttonText.text = "Restart";
            }
           

        }
    }

 
  
}
