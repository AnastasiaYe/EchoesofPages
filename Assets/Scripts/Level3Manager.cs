using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Level3Manager : BaseSceneManager
{
    public GameObject Door_close;
    public GameObject Door_open;
    public Collider2D CameraExtendedBoundry;
    [SerializeField] GameObject TagDetailPage;
    public GameObject Player;
    public float playerspeed;
    public override void HandleItemInteracted(string itemTag)
    {
        switch (itemTag)
        {
            //add three page cases
            case GameTagManager.LEVEL3_DRUM:
                //collect page
                Debug.Log("Player collect drum");
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_0);
                break;
            case GameTagManager.LEVEL3_RECORD:
                //collect page
                Debug.Log("Player collect record");
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_1);
                break;
            case GameTagManager.LEVEL3_INVITATION:
                //collect page
                Debug.Log("Player collect invitation");
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_2);
                TagDetailPage.SetActive(true);
                break;
            case GameTagManager.LEVEL3_PHONE:
                //collect page
                Debug.Log("Player collect phone");
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_3);
                break;
            case GameTagManager.LEVEL3_FALLEN_MIC:
                //collect page
                Debug.Log("Player collect fallen mic");
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_4);
                break;
            case GameTagManager.LEVEL3_CALENDAR:
                //collect page
                Debug.Log("Player collect calendar");
                Door_close.SetActive(false);
                Door_open.SetActive(true);
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_5);
                break;
            case GameTagManager.LEVEL3_WINDOW:
                //collect page
                Debug.Log("Player collect window");
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_6);
                break;
            case GameTagManager.LEVEL3_CRYSTAL_BALL:
                //collect page
                Debug.Log("Player collect crystall ball");
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_7);
                break;
            case GameTagManager.LEVEL3_METEOR_ORNAMENT:
                //collect page
                Debug.Log("Player collect metoer ornament");
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_8);
                break;
            case GameTagManager.LEVEL3_PHOTO:
                //collect page
                Debug.Log("Player collect crystall phone");
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_9);
                break;
            case GameTagManager.LEVEL3_GUITAR:
                //collect page
                Debug.Log("Player collect crystall guitar");
                Level3DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_10);
                break;

        }
    }

    public override int GetCurrentStageImageCheckNum()
    {
        switch (currentStage)
        {
            case STAGE.STAGE_0:
                return 3;
            case STAGE.STAGE_1:
                return 5;
            case STAGE.STAGE_2:
                return 11;
        }
        return 0;
    }

    public void HandleCloseDetailedPage()
    {
        TagDetailPage.SetActive(false);
    }


    public override void HandleNavToNextStage()
    {
        currentStage++;
        if((int)currentStage == 1)
        {
            GameObject.FindGameObjectWithTag("VIRTUAL_CAMERA").GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = CameraExtendedBoundry;
        }
        if ((int)currentStage <= 2)
        {
            HandleStageActivated(currentStage);
            Player.transform.position = new Vector3(-8, -0.6f, 0);
            Player.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            base.HandleLoadNextScene();
        }
    }


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStage = STAGE.STAGE_0;
        HandleStageActivated(currentStage);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentStage == STAGE.STAGE_2) {
            Player.GetComponent<PlayerController>().Speed = 0;
            Player.GetComponent<PlayerController>().isAnimation = true;
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(playerspeed, 0);
        }
    }
}
