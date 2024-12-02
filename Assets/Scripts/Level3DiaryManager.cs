using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3DiaryManager : BaseDiaryPageManager
{
    public static Level3DiaryManager Instance;

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
