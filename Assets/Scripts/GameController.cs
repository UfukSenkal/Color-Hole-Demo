using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Singleton class: GameController

    public static GameController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
    }

    #endregion

    [SerializeField] GameObject objectPrefab;
    [SerializeField] Material[] obstacleMats;
    [SerializeField] Material objectMat;
    [SerializeField] Transform spawnPointOne;
    [SerializeField] Transform spawnPointTwo;

    public Material obstacleMat;
    public Slider slider;
    public int stageOneCount, stageTwoCount;

    List<GameObject> objectPool;
    int levelDifficulty;
    int obstacleRandom,obstacleColor;
    
  
    float xOffset, zOffset, yOffset,x,z;
   

 
   
    void Start()
    {
       

        

        Game.isGameover = false;

        //Dynamic Map için spawnpointler arası uzaklık
        xOffset = .35f;
        zOffset = .5f;
        yOffset = .25f;

        //Stage'i bitirebilmek için gerekli olan obje sayıları
        stageOneCount = 0;
        stageTwoCount = 0;

        objectPool = new List<GameObject>();

        //Her levelda farklı renk obstacle
        obstacleColor = Random.Range(0, obstacleMats.Length);
        obstacleMat = obstacleMats[obstacleColor];


        if (Level.Instance.level < 5)
        {
            levelDifficulty = 5;
            
        }
        else
            levelDifficulty = 10;


        InstanceObjects(spawnPointOne); 
        InstanceObjects(spawnPointTwo);

        StartCoroutine(CheckStageCount());
    }

  
    void Update()
    {
       
    }

    //Object pooling sevdiğim bir tekniktir burada da kullanmak istedim.
    public void InstanceObjects(Transform spawnPoint)
    {
        
        Vector3 temp = spawnPoint.position;
        x = spawnPoint.position.x;
        z = spawnPoint.position.z;

        for (int i = 1; i <= Level.Instance.level * levelDifficulty; i++)
        {
           
            obstacleRandom = Random.Range(2, levelDifficulty * Level.Instance.level);
            spawnPoint.position = new Vector3(spawnPoint.position.x + xOffset, spawnPoint.position.y, spawnPoint.position.z);
            GameObject go = Instantiate(objectPrefab);
            go.tag = "Object";
            go.GetComponent<MeshRenderer>().material = objectMat;
            go.transform.position = spawnPoint.position;

           
            
            if (i % 4 == 0)
            {
                spawnPoint.position = new Vector3(x, spawnPoint.position.y, spawnPoint.position.z + zOffset);
               
                if (spawnPoint.position.z >= temp.z + 3f )
                {
                    spawnPoint.position = new Vector3(temp.x, spawnPoint.position.y + yOffset, temp.z);
                }


            }
            if (i % obstacleRandom == 0)
            {
                go.GetComponent<MeshRenderer>().material = obstacleMat;
                go.tag = "Obstacle";
               
               
            }
        
            if (go.transform.position.z < spawnPointTwo.transform.position.z - 2f)
            {
                go.name = "First";
                stageOneCount++;
            }
            else
            {
                stageTwoCount++;
                go.name = "Second";
            }
           
            objectPool.Add(go);

        }
       
    }

    IEnumerator CheckStageCount()
    {
        yield return new WaitForSeconds(1f);
       
        //Map oluştuktan sonra obstacle tagli objeleri çıkarıp stagedeki toplamamız gereken sayıları buluyoruz
            foreach (var item in objectPool)
            {
                if (item.tag == "Obstacle" && item.name == "First")
                {
                    stageOneCount--;
                }
                if (item.tag == "Obstacle" && item.name == "Second")
                {
                    stageTwoCount--;
                }
            }
        slider.maxValue = stageOneCount + stageTwoCount;
     
    }
}
