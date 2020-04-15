using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToiletPaperScrit : MonoBehaviour
{
    private float rotationSpeed = 12;
    float smooth = 5.0f;
    float tiltAngle = 60.0f;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        //transform.RotateAround(transform.position, Vector3.up, 60 * Time.deltaTime);
        if (TerrainGenerator.instance.canMove)
        {
            transform
                .Translate(Vector3.down * Time.deltaTime * TerrainGenerator.instance.velocity);
        }
    }
    // Update is called once per frame
    void Update1()
    {
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        this.gameObject.transform.rotation  
            = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GameData.d.toiletPaper++;
            this.transform.DOScale(150, 1f).OnComplete(() => Destroy(this.gameObject)); ;
        }
    }

    IEnumerator DestroyAfterC()
    {
        yield return new WaitForSeconds(20);
        Destroy(this.gameObject);
    }
}
