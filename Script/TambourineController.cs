using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TambourineController : MonoBehaviour
{
    public float duration, currentTime;
    public TextMeshPro timeText;
    public TextMeshProUGUI timeTextUi;
    public List< UnlockScr> scr;
    
    public GameObject tambourineObj,MaxText,tamb2d;
    public bool gecti;
    public int areaCount,timer,akanZaman;
    public GameObject finOpen, finClose;
  
    
    void Start()
    {
        Time();
        currentTime = duration;
        timeText.text = currentTime.ToString();
        timeTextUi.text = currentTime.ToString();
        
      
    }



    IEnumerator TimeIEn()
    {
        while (currentTime >=-10f )
        {
            float a = Mathf.InverseLerp(0, duration, currentTime);
            timeTextUi.text = currentTime.ToString();
            timeText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
           
            if(currentTime<0)
            {
                timeTextUi.gameObject.SetActive(false);
                timeText.gameObject.SetActive(false);
            }
            else
            {
                timeTextUi.gameObject.SetActive(true);
                timeText.gameObject.SetActive(true);
            }




            if( currentTime<0f&&scr[0].videoOpened)
            {
              
                currentTime += timer;

            }
            

           

        }
    }
   
    private void FixedUpdate()
    {
        if(scr[0].collect.notes==scr[0].collect.stackCapacity)
        {
            MaxText.SetActive(true);
        }

        if (scr[0].collect.notes < scr[0].collect.stackCapacity)
        {
            MaxText.SetActive(false);
        }
        
        if (currentTime>=0&&scr[0].acildi)
        {
           
            gecti = false;
            
        }
         if(currentTime<0&& areaCount<5&&scr[0].acildi==false)
        {

           
            gecti = true;
            
            
           
          
        }
       
       
       
        if (areaCount==5)
        {
            tamb2d.SetActive(false);
            tamb2d.GetComponent<CapsuleCollider>().isTrigger = true;
            currentTime = 0;
            timeTextUi.text = currentTime.ToString();
            timeText.text = currentTime.ToString();
            tambourineObj.SetActive(true);
            scr[0].collect.HolderParent.SetActive(false);
        }
        if(areaCount==6)
        {
            
            finOpen.SetActive(false);
            finClose.SetActive(true);
        }
       
    }
    public void Time()
    {
        StartCoroutine(TimeIEn());
    }

   
}
