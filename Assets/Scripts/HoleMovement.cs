using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HoleMovement : MonoBehaviour
{
	#region Singleton class: HoleMovement

	public static HoleMovement Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			
		}
	}
    #endregion

    float x, y;
	private Touch touch;
	private float speedForPc,speedForMobile;
	[SerializeField] Vector2 moveLimits;
	Vector3 targetPos, firstStep, secondStep;

	public bool isMoving,stageTwo;
	
	void Start ()
	{
		
		speedForPc = 0.07f;
		speedForMobile = 0.005f;

		isMoving = true;
		stageTwo = false;

	

	

		
	}

	

	void Update ()
	{
		if (GameController.Instance.stageOneCount == 0)
		{
			MoveHoleToSecond();

			Level.Instance.stageClear = true;
		}

		//Unity editor için hareket maalesef if unity editor kodu çalışmadı sürüm kaynaklı olan bir bug olarak düşünüyorum
		if (!Game.isGameover && Input.GetMouseButton(0) && isMoving)
		{

			x = Input.GetAxis("Mouse X");
			y = Input.GetAxis("Mouse Y");

			targetPos = new Vector3(
					transform.position.x + x * speedForPc,
					transform.position.y,
					transform.position.z + y * speedForPc);

			if (!Level.Instance.stageClear && (targetPos.x > -1 && targetPos.z > -2.7f && targetPos.x < 1 && targetPos.z < 1))
			{
				transform.position = targetPos;
			}
			if (Level.Instance.stageClear && (targetPos.x > -1 && targetPos.z > 7.3f && targetPos.x < 1 && targetPos.z < 11f))
			{
				stageTwo = false;
				transform.position = targetPos;

			}



		}
		//Mobile için hareket
		if (Input.touchCount > 0 && !Game.isGameover)
        {
            touch = Input.GetTouch(0);

         
		  

            if (touch.phase == TouchPhase.Moved)
            {
				if (!Level.Instance.stageClear && (targetPos.x > -1 && targetPos.z > -2.7f && targetPos.x < 1 && targetPos.z < 1))
				{
				 transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedForMobile,
                    transform.position.y,
                    transform.position.z + touch.deltaPosition.y * speedForMobile);
				}
				if (Level.Instance.stageClear && (targetPos.x > -1 && targetPos.z > 7.3f && targetPos.x < 1 && targetPos.z < 11f))
				{
					stageTwo = false;
					 transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedForMobile,
                    transform.position.y,
                    transform.position.z + touch.deltaPosition.y * speedForMobile);
				
				}
               
			}
		
		
        }
       
    



	}
	//Diğer stage'e hareket işlemi
	public void MoveHoleToSecond()
	{
		firstStep = new Vector3(0f, transform.position.y, transform.position.z);
		secondStep = new Vector3(transform.position.x, transform.position.y, 7.7f);

		transform.position = Vector3.MoveTowards(transform.position, firstStep, Time.smoothDeltaTime);
		transform.position = Vector3.MoveTowards(transform.position, secondStep, Time.smoothDeltaTime * 2);
		stageTwo = true;
		StartCoroutine(Wait());
		
	}

	//Stage Geçişinde hareket edememesi ve yanmaması için coroutine kullandım
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(5f);
		GameController.Instance.stageOneCount = 1;
		Level.Instance.stageClear = true;
		Magnet.Instance.clearStage = true;
	}

	

}
