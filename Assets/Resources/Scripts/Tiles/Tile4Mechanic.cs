using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile4Mechanic : MonoBehaviour
{
    public GameObject miniCube;
    public float max = 372;
    public float min = -372;
    private Coroutine cr;
    public bool left = true;


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

            StartCoroutine(moveCube());
        }
    }

    public void setTile()
    {
        var newX = Random.Range(min, max);
        Vector3 newPosition = new Vector3(newX / 1000, 0.044f, 0f);
        miniCube.transform.localPosition = newPosition;

        if (newX >= 0)
        {
            newX = Random.Range(min, 0);
        }
        else
        {
            newX = Random.Range(0, max);
        }
        cr = StartCoroutine(moveCube());
    }

    IEnumerator moveCube()
    {
        if (TerrainGenerator.instance.canMove)
        {
            if(miniCube.transform.localPosition.x >= max/1000 ||
                miniCube.transform.localPosition.x <= min/1000)
            {
                left = !left;
            }

            if (left)
            {
                miniCube.transform
                .Translate(Vector3.right * Time.deltaTime * TerrainGenerator.instance.velocity);
            }
            else
            {
                miniCube.transform
                .Translate(Vector3.left * Time.deltaTime * TerrainGenerator.instance.velocity);
            }

        }
        yield return null;
    }

    private void OnDestroy()
    {
        StopCoroutine(cr);
    }
}
