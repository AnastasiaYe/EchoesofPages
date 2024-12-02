using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemInteractionManager : MonoBehaviour
{
    public static ItemInteractionManager Instance;
    public bool ShouldStopRayCast;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public Text HoverText;
    public GameObject[] NextPagesButton;
    public static BaseSceneManager GetCurrentSceneManager()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                return TutorialManager.Instance;
            case 2:
                return Level1Manager.Instance;
            case 3:
                return Level2Manager.Instance;
            case 4:
                return Level3Manager.Instance;
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShouldStopRayCast = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit) && !ShouldStopRayCast)
        {
            HandleMouseHovered(hit.transform.gameObject);
           if(Input.GetMouseButtonDown(0))
           {
               GetCurrentSceneManager().HandleItemInteracted(hit.transform.tag);
               if(hit.transform.GetComponent<Animator>()!=null)
               {
                   hit.transform.GetComponent<Animator>().SetTrigger("SHAKING");
               }
           }
        }
        else
        {
            HoverText.text = "";
        }
    }

    public void HandleMouseHovered(GameObject item)
    {
        if (item.GetComponent<ItemDescription>() == null) return;
        HoverText.text = item.GetComponent<ItemDescription>().ItemDesc;
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Title Page");
    }


}
