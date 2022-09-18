using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class DropArea : MonoBehaviour
{

    public int NumToUnlock;
    public TextMeshPro TxtAmtUnlock;
    public int NumAdded = 0;
    public bool fin = false;
   
    public GameObject thisObje, openObje;
   
    public void UnlockControll(int num)
    {
        NumAdded++;
        TxtAmtUnlock.text = string.Format("{0}/{1}", NumAdded, NumToUnlock);
        TxtAmtUnlock.DOBlendableColor(Color.green, .5f).OnComplete(() => TxtAmtUnlock.DOColor(Color.white, .2f));

        if(NumAdded==NumToUnlock)
        {

            fin = true;
            TxtAmtUnlock.gameObject.SetActive(false);
           
            openObje.SetActive(true);
            thisObje.SetActive(false);
        }

    }

   

   

}
