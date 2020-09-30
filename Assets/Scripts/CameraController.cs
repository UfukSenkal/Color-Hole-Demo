using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singleton class: CameraController

    public static CameraController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
    }

    #endregion


    [SerializeField] GameObject hole;

    public float power;
    public float duration;
    public float slowDownAmount;
    public bool shouldShake;

    Vector3 startPos;
    float initialDuration;
  
    void Start()
    {
        Game.isGameover = false;
       
        startPos = transform.position;
        initialDuration = duration;
    }

    
    void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                transform.position = startPos + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                duration = initialDuration;
                transform.position = startPos;
                shouldShake = false;
            }

        }

        if (HoleMovement.Instance.stageTwo)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, hole.transform.position.z - 2.5f);
        }

        
    }
    public void ShakeCamera()
    {
        shouldShake = true;
    }
}
