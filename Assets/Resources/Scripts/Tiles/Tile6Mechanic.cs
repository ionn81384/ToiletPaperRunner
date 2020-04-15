using System.Collections;
using UnityEngine;

public class Tile6Mechanic : MonoBehaviour
{
    public GameObject miniCube;
    public GameObject miniEnemy;
    public float max = 372;
    public float min = -372;

    public TextMesh textCountingCube;
    public TextMesh textCountingEnemy;

    public bool isCubeEnemy = false;
    public int startCount = 0;
    public int count = 4; //sec
    private Coroutine corotine;

    public Material red;
    public Material green;

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
        startCount = Random.Range(3, 7);
        count = startCount;
        Vector3 newPosition = new Vector3(newX / 1000, 0.044f, 0f);
        miniCube.transform.localPosition = newPosition;

        if (newX >= 0)
        {
            newX = Random.Range(min, -100);
        }
        else
        {
            newX = Random.Range(100, max);
        }

        newPosition = new Vector3(newX / 1000, 0.044f, 0f);
        miniEnemy.transform.localPosition = newPosition;

        textCountingEnemy.gameObject.SetActive(true);
        textCountingCube.gameObject.SetActive(false);
        textCountingEnemy.text = count.ToString();

        miniEnemy.gameObject.GetComponent<Renderer>().material = green;
        miniCube.gameObject.GetComponent<Renderer>().material = red;
        miniEnemy.gameObject.tag = "t";
        miniCube.gameObject.tag = "x";

        //InvokeRepeating("changeTextPerSecond", 0, 1.0f);
        StartCoroutine(changeTextPerSecond());
    }

    private IEnumerator changeTextPerSecond()
    {
        //yield return new WaitForSecondsRealtime(1);
        while (true)
        {
            count--;
            if (count <= 0)
            {
                count = startCount;
                if (textCountingEnemy.gameObject.active)
                {
                    textCountingEnemy.gameObject.SetActive(false);
                    textCountingCube.gameObject.SetActive(true);
                    miniCube.gameObject.GetComponent<Renderer>().material = green;
                    miniEnemy.gameObject.GetComponent<Renderer>().material = red;
                    miniCube.gameObject.tag = "t";
                    miniEnemy.gameObject.tag = "x";
                }
                else
                {
                    textCountingEnemy.gameObject.SetActive(true);
                    textCountingCube.gameObject.SetActive(false);
                    miniEnemy.gameObject.GetComponent<Renderer>().material = green;
                    miniCube.gameObject.GetComponent<Renderer>().material = red;
                    miniCube.gameObject.tag = "x";
                    miniEnemy.gameObject.tag = "t";
                }
            }

            if (textCountingEnemy.gameObject.active)
            {
                textCountingEnemy.text = count.ToString();
            }
            else
            {
                textCountingCube.text = count.ToString();
            }
            yield return new WaitForSeconds(1);
        }
    }
}