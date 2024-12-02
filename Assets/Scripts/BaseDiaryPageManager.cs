using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseDiaryPageManager : MonoBehaviour
{
    private List<GameObject> generatedLogo;
    public GameObject[] ImagePages;
    public GameObject MovingLogo;
    protected Vector2 LogoOriginPosition;
    public Button Diary;
    public AudioSource collectPage;

    public AudioSource turnPage;

    [Serializable]
    public struct PAGES_SPRITE_DATA
    {
        public Sprite Logo;
        public Sprite FullImage;
    }

    public enum PAGES
    {
        PAGE_0,
        PAGE_1,
        PAGE_2,
        PAGE_3,
        PAGE_4,
        PAGE_5,
        PAGE_6,
        PAGE_7,
        PAGE_8,
        PAGE_9,
        PAGE_10,
    }

    public GameObject Diarypage;
    public Dictionary<PAGES, bool> PageCollectionData;
    public PAGES_SPRITE_DATA[] PagesSpriteData;
    public List<PAGES> ShownPages;
    /// <summary>
    /// logo related
    /// </summary>
    public GameObject LogoPrefab;
    public Vector2 LogoInitialPosition;
    public Vector2 LogoGenerationIntervel;

    //generate FullImage on the right 
    public Vector2 ImageInitialPosition;
    public Vector2 ImageGenerationIntervel;
    //add a pointer that point to the first shown page (left page)
    private int PageDisplayIndex;
    private List<PAGES> pageCollectionOrder;



    public void HandlePageCollected(PAGES page)
    {
        if (!pageCollectionOrder.Contains(page)) pageCollectionOrder.Add(page);
        PageCollectionData[page] = true;
        Diary.GetComponent<Animator>().SetTrigger("Active");
        collectPage.Play();
    }

    public void OnStart()
    {
        PageCollectionData = new Dictionary<PAGES, bool>();
        PageCollectionData.Add(PAGES.PAGE_0, false);
        PageCollectionData.Add(PAGES.PAGE_1, false);
        PageCollectionData.Add(PAGES.PAGE_2, false);
        PageCollectionData.Add(PAGES.PAGE_3, false);
        PageCollectionData.Add(PAGES.PAGE_4, false);
        PageCollectionData.Add(PAGES.PAGE_5, false);
        PageCollectionData.Add(PAGES.PAGE_6, false);
        PageCollectionData.Add(PAGES.PAGE_7, false);
        PageCollectionData.Add(PAGES.PAGE_8, false);
        PageCollectionData.Add(PAGES.PAGE_9, false);
        PageCollectionData.Add(PAGES.PAGE_10, false);
        pageCollectionOrder = new List<PAGES>();
        ShownPages = new List<PAGES>();
        generatedLogo = new List<GameObject>();

        MovingLogo = null;
        //Image Data
        PageDisplayIndex = 0;

    }

    private void ReGenerateLogoList()
    {
        for (int i = generatedLogo.Count - 1; i >= 0; i--)
        {
            Destroy(generatedLogo[i]);
        }
        generatedLogo.Clear();
      //  for (int i = 0; i < Enum.GetValues(typeof(PAGES)).Length; i++)
        for(int i = 0;i< pageCollectionOrder.Count;i++)
        {
            if (PageCollectionData[pageCollectionOrder[i]] && !ShownPages.Contains(pageCollectionOrder[i]))
            {
                generatedLogo.Add(Instantiate(LogoPrefab));
                generatedLogo[generatedLogo.Count - 1].transform.position = LogoInitialPosition + LogoGenerationIntervel * (generatedLogo.Count - 1) + new Vector2(Camera.main.transform.position.x, 0) ;
                generatedLogo[generatedLogo.Count - 1].GetComponent<SpriteRenderer>().sprite = PagesSpriteData[(int)pageCollectionOrder[i]].Logo;
                generatedLogo[generatedLogo.Count - 1].GetComponent<Logo>().ThisLogoPageEnum = pageCollectionOrder[i];
                generatedLogo[generatedLogo.Count - 1].transform.SetParent(ImagePages[0].transform.parent);
            }
        }
    }

    public void HandleChangePage(bool NextPage)
    {
        if(NextPage)
        {
            PageDisplayIndex++;
            if (PageDisplayIndex >= ShownPages.Count -1) PageDisplayIndex = ShownPages.Count - 2;
            if (PageDisplayIndex < 0) PageDisplayIndex = 0;
        }
        else
        {
            PageDisplayIndex--;
            if (PageDisplayIndex < 0) PageDisplayIndex = 0;
        }
        ReGenerateImageList();
    }

    public void HandleShowPage(PAGES Page, int InsertIndex, bool shouldShowNextPage)
    {
        if (!PageCollectionData[Page]) return;
        if (ShownPages.Contains(Page)) return;
        if (InsertIndex >= ShownPages.Count)
        {
            ShownPages.Add(Page);
            turnPage.Play();
        }
        else ShownPages.Insert(InsertIndex, Page);
        if(shouldShowNextPage && ShownPages.Count>2)
        {
            PageDisplayIndex++;
        }
        ReGenerateLogoList();
        ReGenerateImageList();
    }

    //generate images in the diary
    private void ReGenerateImageList()
    {
        //display left page and right page
        if (PageDisplayIndex < ShownPages.Count)
        {
            ImagePages[0].GetComponent<SpriteRenderer>().sprite = PagesSpriteData[(int)ShownPages[PageDisplayIndex]].FullImage;
        }
        else
        {
            ImagePages[0].GetComponent<SpriteRenderer>().sprite = null;
        }
        if (PageDisplayIndex + 1 < ShownPages.Count)
        {
            ImagePages[1].GetComponent<SpriteRenderer>().sprite = PagesSpriteData[(int)ShownPages[PageDisplayIndex + 1]].FullImage;
        }
        else
        {
            ImagePages[1].GetComponent<SpriteRenderer>().sprite = null;
            if (ShownPages.Count > 1)
            {
                PageDisplayIndex--;
                ReGenerateImageList();
                return;
            }
        }
    }

    public void HandleOpenOrCloseMenu()
    {
        if (Diarypage.activeSelf)
        {
            OnMenuDeinit();
        }
        else OnMenuInit();
    }


    public void OnMenuInit()
    {
        ReGenerateLogoList();
        ReGenerateImageList();
        ItemInteractionManager.Instance.NextPagesButton[0].SetActive(true);
        ItemInteractionManager.Instance.NextPagesButton[1].SetActive(true);
        Diarypage.SetActive(true);
        ItemInteractionManager.Instance.ShouldStopRayCast = true;
    }


    public void OnMenuDeinit()
    {
        Diarypage.SetActive(false);
        ItemInteractionManager.Instance.NextPagesButton[0].SetActive(false);
        ItemInteractionManager.Instance.NextPagesButton[1].SetActive(false);
        if(HandleCheckPageResult())
        {
            print("Current Stage done");
            BaseSceneManager.Instance.HandleNavToNextStage();
        }
        ItemInteractionManager.Instance.ShouldStopRayCast = false;
    }

    public PAGES GetPage(GameObject logo)
    {
        for(int i = 0;i< generatedLogo.Count;i++)
        {
            if (generatedLogo[i] == logo) return generatedLogo[i].GetComponent<Logo>().ThisLogoPageEnum;
        }
        return PAGES.PAGE_0;
    }

    public void HandleResetPage(int index)
    {
        //index must be 0 or 1
       // if (index != 0 && index != 1) return;
        if (index >= ShownPages.Count) return;
        ShownPages.RemoveAt(index);
        ReGenerateLogoList();
        ReGenerateImageList();
    }

    public void HandleDecideLogoPosition()
    {
        float lowestDistance = float.MaxValue;
        int index = -1;
        for(int i = 0;i < 3;i++)
        {
            Vector3 comparePos = i == 0 ? LogoOriginPosition : ImagePages[i - 1].transform.position;
            if (Vector3.Distance(MovingLogo.transform.position,comparePos)<lowestDistance)
            {
                lowestDistance = Vector3.Distance(MovingLogo.transform.position, comparePos);
                index = i;
            }
        }
        if(index == 0)
        {
            MovingLogo.transform.position = LogoOriginPosition;
        }
        else
        {
            bool isBetweenAAndB = true;
            Vector3 tempLogoPos = MovingLogo.transform.position;
            tempLogoPos.y = ImagePages[0].transform.position.y;
            //here we need to calculate the distance and see if the logo is between two images page.
            float distToPageA = Vector3.Distance(tempLogoPos, ImagePages[0].transform.position);
            float distToPageB = Vector3.Distance(tempLogoPos, ImagePages[1].transform.position);
            float distBetweenAB = Vector3.Distance(ImagePages[0].transform.position, ImagePages[1].transform.position);
            if(distToPageA>distBetweenAB || distToPageB>distBetweenAB)
            {
                isBetweenAAndB = false;
            }
            int finalIndex = 0;
            bool shouldShowNextPage = false;
            if (index>1 && ShownPages.Count == 0)
            {
                finalIndex = 0;
            }
            else
            {
                if(isBetweenAAndB )
                {
                    finalIndex = PageDisplayIndex + 1;
                }
                else if(index == 1)
                {
                    finalIndex = PageDisplayIndex + index - 1;
                }
                else
                {
                    finalIndex = PageDisplayIndex + index;
                    shouldShowNextPage = true;
                }
            }

            HandleShowPage(GetPage(MovingLogo), finalIndex, shouldShowNextPage);
        }
    }


    public bool HandleCheckPageResult()
    {
        int numToCheck = BaseSceneManager.Instance.GetCurrentStageImageCheckNum();
        if (numToCheck > ShownPages.Count) return false;
        for(int i = 0;i<numToCheck;i++)
        {
            if(ShownPages[i] != (PAGES)i)
            {
                return false;
            }
        }
        return true;
    }

    protected void OnUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 20f);
        bool[] mouseHoverOnPage = new bool[2];

        for(int i = 0;i<hits.Length;i++)
        {
            if (hits[i].transform.tag == "DIARY_PAGE_0")
            {
                if (hits[i].transform.childCount != 0 && ShownPages.Count > 0)
                {
                    mouseHoverOnPage[0] = true;
                    hits[i].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else if (hits[i].transform.tag == "DIARY_PAGE_1")
            {
                if (hits[i].transform.childCount != 0 && ShownPages.Count > 1)
                {
                    mouseHoverOnPage[1] = true;
                    hits[i].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            if (hits[i].transform.tag == "LOGO" && Input.GetMouseButtonDown(0))
            {
                MovingLogo = hits[i].transform.gameObject;
                LogoOriginPosition = hits[i].transform.position;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (hits[i].transform.tag == "DIARY_PAGE_0")
                {
                    hits[i].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    HandleResetPage(PageDisplayIndex);
                }
                if (hits[i].transform.tag == "DIARY_PAGE_1")
                {
                    hits[i].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                    HandleResetPage(PageDisplayIndex + 1);
                }
            }
           
        }
        for (int i = 0; i < 2; i++)
        {
            if (!mouseHoverOnPage[i] && ImagePages[i].transform.childCount>0 && ShownPages.Count>i)
            {
                ImagePages[i].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (MovingLogo!=null)
       {
           if(Input.GetMouseButtonUp(0))
           {
               HandleDecideLogoPosition();
               MovingLogo = null;
           }
           else
           {
               MovingLogo.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
               MovingLogo.transform.position = new Vector3(MovingLogo.transform.position.x, MovingLogo.transform.position.y, 0);
           }
       }
    }
}
