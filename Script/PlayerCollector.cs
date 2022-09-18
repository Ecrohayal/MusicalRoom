using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerCollector : MonoBehaviour
{
    public Transform itemHolderTransform;

    int numItemsHolding = 0;
   

    public void AddNewItem(Transform _itemToAdd)
    {
        _itemToAdd.DOJump(itemHolderTransform.position + new Vector3(0, 0.25f * numItemsHolding, 0), 1.5f, 1, .15f).OnComplete(() =>
                 {

                     _itemToAdd.SetParent(itemHolderTransform, true);
                     _itemToAdd.localPosition = new Vector3(0, 0.35f * numItemsHolding, 0);
                     _itemToAdd.localRotation = Quaternion.identity;
                     numItemsHolding++;




                 });
    }
}
