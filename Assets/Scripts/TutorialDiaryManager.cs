using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDiaryManager : BaseDiaryPageManager
{
    public static TutorialDiaryManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
   

    // Start is called before the first frame update
    void Start()
    {
        base.OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        base.OnUpdate();
    }
}
