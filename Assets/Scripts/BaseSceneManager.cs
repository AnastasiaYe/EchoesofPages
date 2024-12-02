using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSceneManager : MonoBehaviour
{
    public static BaseSceneManager Instance;
    public GameObject[] StageItems;
    public bool LevelEnd;
    public enum STAGE
    {
        STAGE_0,//Before truth reveal
        STAGE_1,//After truth reveal
        STAGE_2,
        STAGE_3,
    }
    public STAGE currentStage;

    public virtual void HandleItemInteracted(string itemTag)
    {
        //do nothing
    }

    public virtual int GetCurrentStageImageCheckNum()
    {
        return 0;
    }

    public virtual void HandleNavToNextStage()
    {
    }

    public void HandleLoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HandleActiveOneScene(int index)
    {
        if (index < 0 || index > StageItems.Length - 1) return;
        for(int i = 0;i<StageItems.Length;i++)
        {
            StageItems[i].SetActive(false);
        }
        StageItems[index].SetActive(true);
    }


    public void HandleStageActivated(STAGE stage)
    {
        switch (stage)
        {
            case STAGE.STAGE_0:
                break;
            case STAGE.STAGE_1:
                break;
            case STAGE.STAGE_2:
                break;
            case STAGE.STAGE_3:
                break;
        }
        HandleActiveOneScene((int)stage);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
