using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile5Mechanic : MonoBehaviour
{
    public GameObject miniCube;
    public GameObject miniEnemy;
    public float max = 372;
    public float min = -372;

    public float sizeCube = 127;
    public float sizeEnemy = 100f;



    // Start is called before the first frame update
    private void Start()
    {
        setTile();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (TerrainGenerator.instance.canMove)
        {
            miniCube.transform.parent.gameObject.transform
                .Translate(Vector3.forward * Time.deltaTime * TerrainGenerator.instance.velocity);
        }
    }

    public void setTile()
    {
        var newX = Random.Range(min, max);
        Vector3 newPosition = new Vector3(newX / 1000, 0.044f, 0f);
        miniCube.transform.localPosition = newPosition;

        if(newX >= 0)
        {
            newX = Random.Range(min, 0);
        }
        else
        {
            newX = Random.Range(0, max);
        }

        
        newPosition = new Vector3(newX / 1000, 0.044f, 0f);
        miniEnemy.transform.localPosition = newPosition;
    }
}
