using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : BaseSceneManager
{
    [SerializeField] GameObject Guitar;
    [SerializeField] GameObject GiftTag;
    [SerializeField] GameObject Mark;
    [SerializeField] GameObject Award;
    [SerializeField] GameObject TagDetailPage;
    public AudioSource MusicBox;
    public override void HandleItemInteracted(string itemTag)
    {
        switch (itemTag)
        {
            //add three page cases
            
            case GameTagManager.LEVEL1_GUITAR:
                Guitar.GetComponent<Renderer>().sortingOrder = 3;
                GiftTag.SetActive(true);
                break;
            case GameTagManager.LEVEL1_GIFTTAG:
                //collect page
                Debug.Log("Player collect guitar page");
                Level1DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_0);
                TagDetailPage.SetActive(true);
                break;
            case GameTagManager.LEVEL1_FIREPLACE:
                //Play animation;
                Debug.Log("Player collect guitar page");
                Level1DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_1);
                break;
            case GameTagManager.LEVEL1_BANDAIDS:
                //collect page
                Debug.Log("Player collect band-aids page");
                Level1DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_2);
                break;
            case GameTagManager.LEVEL1_MARK:
                Mark.SetActive(false);
                Award.SetActive(true);
                break;
            case GameTagManager.LEVEL1_AWARD:
                //collect page
                Debug.Log("Player collect award page");
                Level1DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_3);
                break;
            case GameTagManager.LEVEL1_POLAROID:
                //collect page
                Debug.Log("Player collect polaroid page");
                Level1DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_4);
                break;
            case GameTagManager.LEVEL1_MUSICBOX:
                MusicBox.Play();
                break;

                //case GameTagManager.LEVEL1_MARK:
                //    //collect page
                //    Debug.Log("Player collect mark page");
                //    Level1DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_4);
                //    break;
        }
    }

    public void HandleCloseDetailedPage()
    {
        TagDetailPage.SetActive(false);
    }

    public override int GetCurrentStageImageCheckNum()
    {
        switch (currentStage)
        {
            case STAGE.STAGE_0:
                return 3;
            case STAGE.STAGE_1:
                return 5;
        }
        return 0;
    }


    public override void HandleNavToNextStage()
    {
        currentStage++;
        if ((int)currentStage <= 1)
        {
            HandleStageActivated(currentStage);
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
    void Update()
    {

    }
}
