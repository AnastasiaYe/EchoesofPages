using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : BaseSceneManager
{

    private bool poster_is_fixed = false;
    public GameObject poster;
    public Sprite poster_fixed;

    [SerializeField] GameObject musicSheet1;
    [SerializeField] GameObject musicSheet2;
    [SerializeField] GameObject collectedSheet1;
    [SerializeField] GameObject collectedSheet2;
    private bool sheet1_collected = false;
    private bool sheet2_collected = false;
    private bool sheet_page_collected = false;

    private void FixedUpdate()
    {
        if (sheet1_collected && sheet2_collected && !sheet_page_collected)
        {
            Debug.Log("Player collect music sheet page");
            TutorialDiaryManager.Instance.HandlePageCollected(TutorialDiaryManager.PAGES.PAGE_2);
            sheet_page_collected = true;
        }
    }

    public override void HandleItemInteracted(string itemTag)
    {
        switch(itemTag)
        {
            //add three page cases
            case GameTagManager.TUTORIAL_GUITAR:
                //collect page
                Debug.Log("Player collect guitar page");
                TutorialDiaryManager.Instance.HandlePageCollected(TutorialDiaryManager.PAGES.PAGE_0);
                break;
            case GameTagManager.TUTORIAL_POSTER:
                //collect page
                if (poster_is_fixed == false)
                {
                    Debug.Log("Player collect poster page");
                    TutorialDiaryManager.Instance.HandlePageCollected(TutorialDiaryManager.PAGES.PAGE_1);
                    poster.GetComponent<SpriteRenderer>().sprite = poster_fixed;
                    poster_is_fixed = true;
                }
                break;
            case GameTagManager.TUTORIAL_MUSIC_SHEET:
                //collect page
                if (sheet1_collected == false)
                {
                    sheet1_collected = true;
                    musicSheet1.SetActive(false);
                    collectedSheet1.SetActive(true);
                }
                
                break;
            case GameTagManager.TUTORIAL_MUSIC_SHEET2:
                //collect page
                if (sheet2_collected == false)
                {
                    sheet2_collected = true;
                    musicSheet2.SetActive(false);
                    collectedSheet2.SetActive(true);
                }

                break;
            case GameTagManager.TUTORIAL_WINDOW:
                Debug.Log("Player clicked window");
                break;
            case GameTagManager.TUTORIAL_DAIRY:
                if(LevelEnd)
                {
                    //play animation here
                    base.HandleLoadNextScene();
                }
                break;
        }
    }

    public override int GetCurrentStageImageCheckNum()
    {
        switch (currentStage)
        {
            case STAGE.STAGE_0:
                return 2;
            case STAGE.STAGE_1:
                return 3;
            case STAGE.STAGE_2:
                return 3;
        }
        return 0;
    }




    public override void HandleNavToNextStage()
    {
        currentStage++;
        if((int)currentStage <2)
        {
            HandleStageActivated(currentStage);
        }
        else if((int)currentStage == 2)
        {
            HandleStageActivated(currentStage);
            LevelEnd = true;
            //change diary icon.
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
        LevelEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
