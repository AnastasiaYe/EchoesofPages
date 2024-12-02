using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : BaseSceneManager
{
    //public GameObject Guitar;
    [SerializeField] GameObject tornLetter1;
    [SerializeField] GameObject tornLetter2;
    [SerializeField] GameObject loveletter_torn;
    private bool letter1_collected = false;
    private bool letter2_collected = false;
    public AudioSource RadioBroadcast;
    public override void HandleItemInteracted(string itemTag)
    {
        switch (itemTag)
        {
            //add three page cases
            case GameTagManager.LEVEL2_LOVE_LETTER:
                //collect page
                Debug.Log("Player collect love letter");
                Level2DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_0);
                break;
            case GameTagManager.LEVEL2_RADIO:
                //collect page
                Debug.Log("Player collect radio");
                RadioBroadcast.Play();
                Level2DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_1);
                break;
            case GameTagManager.LEVEL2_BLANKET:
                //collect page
                Debug.Log("Player collect blanket");
                Level2DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_2);
                break;
            case GameTagManager.LEVEL2_MUSIC_SCORE:
                //collect page
                Debug.Log("Player collect guitar");
                Level2DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_3);
                break;
            case GameTagManager.LEVEL2_BOY:
                //collect page
                Debug.Log("Player collect boy");
                Level2DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_4);
                break;
            case GameTagManager.LEVEL2_TORN_LETTER_1:
                //collect page
                if (letter1_collected == false)
                {
                    letter1_collected = true;
                    tornLetter1.SetActive(false);
                }

                break;
            case GameTagManager.LEVEL2_TORN_LETTER_2:
                //collect page
                if (letter2_collected == false)
                {
                    letter2_collected = true;
                    tornLetter2.SetActive(false);
                }

                break;
            case GameTagManager.LEVEL2_LOVE_LETTER_TORN:
                //collect page
                Debug.Log("Player collect torn love ltter");
                Level2DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_5);
                break;
            case GameTagManager.LEVEL2_GUITAR:
                //collect page
                Debug.Log("Player collect tree");
                Level2DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_6);
                break;
            case GameTagManager.LEVEL2_METEOR:
                //collect page
                Debug.Log("Player collect meteor");
                Level2DiaryManager.Instance.HandlePageCollected(BaseDiaryPageManager.PAGES.PAGE_7);
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
                return 5;
            case STAGE.STAGE_2:
                return 8;
        }
        return 0;
    }


    public override void HandleNavToNextStage()
    {
        currentStage++;
        if ((int)currentStage <= 2)
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
        if (letter1_collected & letter2_collected)
        {
            loveletter_torn.SetActive(true);
        }
    }
}
