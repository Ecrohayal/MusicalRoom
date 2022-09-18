using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject collectArrow, DropAreaArrow, UpgradeArrow, arrow;
    public Transform collectPos, DropAreaPos, UpgradePos, itemPos;
    public CollectController col;
    public PlayerManager manager;
    public CanvasControl canvas;
    public TambourineController tamb;
   
    void Update()
    {
        if(col.start==false)
        {
            transform.LookAt(collectPos);
        }
        if(col.start)
        {
            collectArrow.SetActive(false);
            DropAreaArrow.SetActive(true);
            transform.LookAt(DropAreaPos);
        }
        if(col.dropArea)
        {
            DropAreaArrow.SetActive(false);
            arrow.SetActive(false);
        }
        if(manager.moneyCount>=50)
        {
            arrow.SetActive(true);
            UpgradeArrow.SetActive(true);
            transform.LookAt(UpgradePos);
        }
        if(canvas.isUpgrade)
        {
            arrow.SetActive(false);
            UpgradeArrow.SetActive(false);
        }

        if(tamb.areaCount==5)
        {
            arrow.SetActive(true);
            transform.LookAt(itemPos);
        }
        if(col.isfinishItem)
        {
            arrow.SetActive(false);
        }
    }
}
