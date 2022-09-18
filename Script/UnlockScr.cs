using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class UnlockScr : MonoBehaviour
{
    public int NumToUnlock;
    public TextMeshPro TxtAmtUnlock;
    public GameObject ItemToUnlock;
    public CollectController collect;
    public CanvasControl canvas;
    
    public int NumAdded = 0;
    public GameObject sprite,sprite1,partical,tamb1,sprite2,money,video;

    public bool acildi,videoOpened = false;

    public void Close()
    {
        if(collect.tambourine.currentTime<0)
        {
            videoOpened = true;
            StartCoroutine(videoCon());

           
        }
       
    }
    public void UnlockItem(int num)
    {
        NumAdded++;
        TxtAmtUnlock.text = string.Format("{0}/{1}", NumAdded, NumToUnlock);
        TxtAmtUnlock.DOBlendableColor(Color.green, .5f).OnComplete(() => TxtAmtUnlock.DOColor(Color.white, .2f));


        if(NumAdded ==NumToUnlock && !ItemToUnlock.activeInHierarchy)
        {
            collect.tambourine.areaCount ++;
            acildi = true;
            collect.isDropArea = false;
           
            ItemToUnlock.SetActive(false);
            TxtAmtUnlock.gameObject.SetActive(false);
           StartCoroutine(control());
        }
       
      


    }
   
    public IEnumerator control()
    {
            this.tag = "Untagged";
            sprite.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            canvas.partical.Play();
            partical.SetActive(true);
            yield return new WaitForSeconds(0.35f);
            tamb1.SetActive(false);

            yield return new WaitForSeconds(0.01f);
            sprite2.SetActive(false);
            partical.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            money.SetActive(true);
            sprite1.SetActive(true);

            collect.tambourine.currentTime = collect.tambourine.timer;
            yield return new WaitForSeconds(0.2f);
            collect.tambourine.scr.RemoveAt(0);
            yield return new WaitForSeconds(0.1f);

            Destroy(this.gameObject);



    }

    public IEnumerator videoCon()
    {
        videoOpened = true;
        this.tag = "Untagged";
        
        sprite.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        sprite2.SetActive(false);
        video.SetActive(true);
        yield return new WaitForSeconds(0.1f);
       
        sprite1.SetActive(true);
        collect.tambourine.currentTime = collect.tambourine.timer;
        yield return new WaitForSeconds(0.2f);
        collect.tambourine.scr.RemoveAt(0);
        yield return new WaitForSeconds(0.1f);

        Destroy(this.gameObject);
        
       
        
    }
   

}
