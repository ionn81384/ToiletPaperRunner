using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public static TerrainGenerator instance = null;

    [Header("Dev settings")]
    public bool startOnStart = false;

    public float velocity = 1f;
    public bool canMove = true; // use this for when in pause

    [Header("Menu Objects")]
    public GameObject InitalWorld;

    public GameObject TilesExample;

    private Vector3 initialPosition;

    [Header("Tiles Type")]
    public GameObject[] tiles;

    public GameObject parentTiles;

    private bool isWhiteTile = false;
    private int placeToiletPaper = 5;
    public float maxTP = 5f, minTP = -5f;
    public GameObject toiletPaper;

    public bool itCHecked = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }

    private void Start()
    {
        initialPosition = InitalWorld.transform.position; // save for it to put back
        if (startOnStart)
        {
            StartGenerating();
        }
    }

    private void Update()
    {
        if (canMove)
        {
            if (InitalWorld.transform.position.z <= 70)
            {
                InitalWorld.transform.Translate(Vector3.forward * Time.deltaTime * velocity);
            }
        }
    }

    public void ResetPosition(bool menu = false)
    {
        InitalWorld.transform.position = initialPosition;

        if(GameData.d.bestScore < GameData.d.score)
        {
            GameData.d.bestScore = GameData.d.score;
        }
        GameData.d.score = 0;

        // Instantiate tile example prefab
        //for (int i = 0; i < 7 || TilesExample.transform.childCount != 0 ; i++)
        //{
        //Destroy(TilesExample.transform.GetChild(i).gameObject);
        //}

        foreach (Transform child in TilesExample.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 7; i++)
        {
            var tileGen = (GameObject)Instantiate(tiles[0]);
            var vect = tileGen.transform.localPosition;
            vect.z = -19.11f - i * 3;
            vect.x = -18.82f;
            vect.y = -0.59f;
            tileGen.gameObject.transform.parent = TilesExample.transform;
            tileGen.transform.localPosition = vect;
        }
        PlayerManager.instance.score = 0;
        GameData.d.score = 0;
        if (menu)
        {
            UiManager.instance.clickToMenu();
        }
        else
        {
            UiManager.instance.clickToReplay();
        }
        PlayerManager.instance.resetPosition();
    }



    public void StartGenerating()
    {
    }

    public void PlaceOneTile()
    {
        GameObject tileGen = null;
        if (isWhiteTile)
        {
            tileGen = (GameObject)Instantiate(tiles[0]);
        }
        else
        {
            tileGen = (GameObject)Instantiate(tiles[placeWitchTile()]);
        }
        isWhiteTile = !isWhiteTile;
        var vect = tileGen.transform.localPosition;
        vect.z = -37;
        tileGen.gameObject.transform.parent = parentTiles.transform;
        tileGen.transform.localPosition = vect;
        itCHecked = true;
        PlaceToiletPaperRandom();
    }

    private void PlaceToiletPaperRandom()
    {
        //placeToiletPaper--;
        if (PlayerManager.instance.score <= 10)
        {
            return;
        }

        if (PlayerManager.instance.score % 5 == 0)
        {
            //placeToiletPaper = 5;

            // place toilet paper
            var tileGen = (GameObject)Instantiate(toiletPaper);
            Vector3 positionToPut = new Vector3(0, 0.66f, -9.52f);
            positionToPut.x = Random.Range(minTP, maxTP);
            tileGen.transform.position = positionToPut;
            tileGen.transform.parent = TilesExample.transform;
        }
    }

    private int placeWitchTile()
    {
        var sc = GameData.d.score;
        //return 6;

        if (sc < 1)
        {
            return 1;
        }
        else if (sc < 3)
        {
            return 2;
        }
        else if (sc < 8)
        {
            return 3;
        }
        else if (sc < 10)
        {
            return 4;
        }
        else if (sc < 14)
        {
            return 5;
        }
        else if (sc < 18)
        {
            return 6;
        }
        else if (sc >= 18)
        {
            return Random.Range(2, tiles.Length);
        }

        return 1;
    }
}