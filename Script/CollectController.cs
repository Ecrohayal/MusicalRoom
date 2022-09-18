using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CollectController : MonoBehaviour
{
    public GameObject HolderParent,bag;

    Stack<Transform> CollectedTrans = new Stack<Transform>();
    public bool isDropArea,isfinishItem,start,dropArea;
    Vector3 DropPos;
    UnlockScr unlockItemScr;
    DropArea area;
    CanvasControl canvas;
    public int notes = 0;
    public int stackCapacity;
    public TambourineController tambourine;
    public GameObject items;
    public Vector3 itemScale;
    private void Start()
    {
        canvas = GetComponent<CanvasControl>();
       
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Drop"))
        {
            isDropArea = false;
        }

        if (other.CompareTag("Drop2"))
        {
            isfinishItem = false;
        }

    }
    public void den()
    {
        StartCoroutine(DropSlow(.5f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Drop"))
        {
            isDropArea = true;
            DropPos = other.transform.position;
             other.TryGetComponent(out unlockItemScr);
            den();
           
            
           

           

        }
      
        else
        {
           
            Item localItem = null;
            other.TryGetComponent(out localItem);

            if(other.CompareTag("Note") && localItem && localItem.isCollected==false&&notes<stackCapacity)
            {
                start = true;
                canvas.music.Play();
                notes++;
                other.transform.SetParent(HolderParent.transform);
                other.transform.localPosition = new Vector3(0, CollectedTrans.Count * .45f, .1f);
                other.transform.localRotation = Quaternion.identity;
                other.transform.localScale = itemScale;
                CollectedTrans.Push(other.transform);
                localItem.isCollected = true;
            }

            if (other.CompareTag("FinishItem") && localItem && localItem.isCollected == false)
            {


                tambourine.areaCount++;
                other.transform.SetParent(bag.transform);
                other.transform.localPosition = new Vector3(0, CollectedTrans.Count * .45f, .1f);
                other.transform.localRotation = Quaternion.Euler(-90f, 90f, -90f);
                other.transform.localScale = new Vector3(0.5f, 0.5f, 0.6f);
                CollectedTrans.Push(other.transform);
                localItem.isCollected = true;

                

            }
        }

        if(other.CompareTag("Drop2"))
        {
            isfinishItem = true;
            DropPos = other.transform.position;
            other.TryGetComponent(out area);
            StartCoroutine(finishItem(.5f));
            
        }

       
    }

      

    IEnumerator DropSlow(float _delay=0)
    {
        while(isDropArea)
        {

            dropArea = true;
            yield return new WaitForSeconds(_delay);
            if(CollectedTrans.Count>0&&tambourine.gecti==false&&unlockItemScr.videoOpened==false)
            {
                canvas.drop.Play();
                notes--;
                Transform newItem = CollectedTrans.Pop();
                newItem.parent = items.transform;
                newItem.DOJump(DropPos, 2, 1, .2f).OnComplete(() => newItem.DOPunchScale(new Vector3(.2f, .2f, .2f), .1f).OnComplete(() => newItem.gameObject.SetActive(false)));
            
                if(unlockItemScr)
                {
                    yield return new WaitForSeconds(_delay);
                    unlockItemScr.UnlockItem(1);
                
                }
               
            }
            if(tambourine.gecti==true)
            {
                //unlockItemScr.videoOpened = true;
                

                if (unlockItemScr)
                {
                    
                    yield return new WaitForSeconds(_delay);
                    unlockItemScr.Close();
                   
                }
                yield return new WaitForSeconds(0.0005f);
               

                tambourine.gecti = false;
            }
           
        }

        yield return null;
    }

   IEnumerator finishItem(float delay=0)
    {
        while(isfinishItem)
        {

            yield return new WaitForSeconds(delay);
            if (CollectedTrans.Count > 0&&area.fin==false )
            {
                canvas.drop.Play();
                notes--;
                Transform newItem = CollectedTrans.Pop();
                newItem.parent = items.transform;
                newItem.DOJump(DropPos, 2, 1, .2f).OnComplete(() => newItem.DOPunchScale(new Vector3(.2f, .2f, .2f), .1f).OnComplete(() => newItem.gameObject.SetActive(false)));

                if (area)
                {
                    yield return new WaitForSeconds(delay);
                    area.UnlockControll(1);

                }

            }
            yield return null;
        }
    }
    
}
