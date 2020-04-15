using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangingTextSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Scale()
    {
        
        this.gameObject.transform.DOScale(1.5f, 2f)
            .OnComplete(() => { this.gameObject.transform.DOScale(1f, 2f); }).WaitForCompletion();
        Scale();
    }

    private void Update()
    {
        if(this.gameObject.transform.localScale.x == 1)
        {
            this.gameObject.transform.DOScale(1.3f, 2f);
        }

        if(this.gameObject.transform.localScale.x == 1.3f)
        {
            this.gameObject.transform.DOScale(1f, 2f);
        }
    }
}
