using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public enum ItemType
    {
        Gold50,
        NoAds
    }

    public ItemType itemType;

    public Text priceText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LateUpdate()
    {
        if (!GameData.d.isAddOn)
        {
            this.gameObject.SetActive(false);
        }
        
    }
    //https://www.youtube.com/watch?v=j98jrUPHVYw
    public void clickBuy()
    {
        IAPManager.Instance.BuyNoADS();
    }
}
