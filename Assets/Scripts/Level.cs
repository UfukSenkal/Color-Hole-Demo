using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    #region Singleton class: Level

    public static Level Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
    }

    #endregion

    public bool stageClear;
    public int level;

    [SerializeField] ParticleSystem victory;
    
    

  

    void Start()
    {
        if (level == 0)
        {
            level = 1;
        }

        level = PlayerPrefs.GetInt("Level");
       
        stageClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.stageTwoCount == 0 && !Game.isGameover)
        {
            PlayerPrefs.SetInt("Level", ++level);
            UIManager.Instance.buttonText.text = "Next Level";
            victory.Play();
            Game.isGameover = true;
          
        }
        
    }
}
