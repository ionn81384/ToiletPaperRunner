using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    [Header("Player Show")]
    public GameObject playerSkin;
    private float swipeSensibility = 11f;

    public GameObject[] chars;

    public GameObject cilinder;
    public Vector3 startPostion;

    public int score = 0;


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
    }
    // Start is called before the first frame update
    void Start()
    {
        startPostion = playerSkin.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (TerrainGenerator.instance.canMove)
        {
            // rotate the player towards front
            if (Input.GetMouseButton(0))
            {
                var playerPos = playerSkin.transform.position;
                playerPos.x = Mathf.Lerp(playerPos.x, GetPositionTouchX(), 10f * Time.deltaTime);
                playerSkin.transform.position = playerPos;
            }
            var pos = playerSkin.transform.localPosition;
            pos.y = -1.9f;
            pos.x -= 0.39f;
            cilinder.gameObject.transform.localPosition = pos;

            PutAnimationRun();
        }
        else
        {
            PutAnimationIdle();
        }
    }

    private void PutAnimationIdle()
    {
        foreach (var c in chars)
        {
            c.GetComponent<Animator>().SetFloat("Speed_f", 0f);
        }
    }

    private void PutAnimationRun()
    {
        foreach (var c in chars)
        {
            c.GetComponent<Animator>().SetFloat("Speed_f", 2f);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        var vectorr = playerSkin.transform.position;
        vectorr.z += 0.5f;
        if (Physics.Raycast(playerSkin.transform.position, Vector3.down / 4, out hit))
        {
            if (hit.collider.transform.tag == "TileEmpty" && TerrainGenerator.instance.canMove)
            {
                if (Physics.Raycast(vectorr, Vector3.down / 8, out hit))
                {
                    if (hit.collider.transform.tag == "t")
                    {
                        // tile score++
                        if (TerrainGenerator.instance.itCHecked)
                        {
                            GameData.d.score++;
                            UiManager.instance.Score.text = GameData.d.score.ToString();
                            TerrainGenerator.instance.itCHecked = false;
                            AudioManager.instance.playClick();
                        }
                            
                    }
                    else
                    {
                        // died
                        if(GameData.d.score >= 2)
                        {
                            Died();
                        }
                    }
                }


                // -1.287 1.7
                Sequence sq = DOTween.Sequence();

                sq.Append(playerSkin.transform.DOLocalMoveY(1.7f, 0.4f));
                sq.Append(playerSkin.transform.DOLocalMoveY(-1.278f, 0.4f));
                sq.Play();

                //playerSkin.transform.DOJump(playerSkin.transform.position, 5f, 0, 0.8f);
                return;
            }
            
            //Debug.Log("Found an object : " + hit.collider.transform.tag);
            Debug.DrawRay(playerSkin.transform.position, Vector3.down / 4, Color.red);
            Debug.DrawRay(vectorr, Vector3.down / 8, Color.cyan);

        }
    }  

    private void Died()
    {
        TerrainGenerator.instance.canMove = false;
        if (GameData.d.bestScore < GameData.d.score)
        {
            GameData.d.bestScore = GameData.d.score;
            UiManager.instance.PublishBigScore();
        }
        UiManager.instance.ShowCanvasDead();

        AudioManager.instance.changeMusic(2);

    }

    float GetPositionTouchX()
    {

        float x = 0;

        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {

                Touch touch = Input.GetTouch(0);

                x = touch.position.x / Screen.width - 0.5f;
            }
        }
        else
        {
            x = Input.mousePosition.x / Screen.width - 0.5f;
        }

        if (x < -0.5f)
            x = -0.5f;

        if (x > 0.5f)
            x = 0.5f;

        return -swipeSensibility * x;
    }

    public void resetPosition()
    {
        playerSkin.transform.localPosition = startPostion;
    }

}
 