
/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
#if AADOTWEEN
using DG.Tweening;
#endif

#if APPADVISORY_ADS
using AppAdvisory.Ads;
#endif

namespace AppAdvisory.Boing
{
	public class GameManager : MonoBehaviour 
	{
		public int numberOfPlayToShowInterstitial = 5;

		public string VerySimpleAdsURL = "http://u3d.as/oWD";
		#region canvas
		public Text titleText;
		public Text bestScoreText;
		public Text pointText;
		public Text diamondText;
		public GameObject tutoAtStart;
		#endregion

		#region player
		public Transform playerParent;
		public Transform playerSphere;
		public Transform shadow;
		#endregion

		#region customizable variables
		public List<Color> colors;
		public float distanceZ = 5;
		public int numberOfPlatformToSpawnedAtStart = 6;
		public int changeColorEveryXPoints = 15;
		public float speedBounceInSeconds = 0.3f;
		float swipeSensibility = 7f;
		#endregion

		#region public variables
		public int point = 0;
		public int diamond = 0;
		#endregion

		#region references
		public GameObject platformPrefab;
		public Material plateformMaterial;
		#endregion

		#region sounds
		AudioSource audioSource;
		public AudioClip soundJump;
		void PlaySoundJump()
		{
			audioSource.PlayOneShot(soundJump);
		}
		public AudioClip soundFall;
		void PlaySoundFall()
		{
			audioSource.PlayOneShot(soundFall);
		}
		public AudioClip soundDiamond;
		void PlaySoundDiamond()
		{
			audioSource.PlayOneShot(soundDiamond);
		}
		#endregion

		bool gameIsStarted = false;

		bool isGameOver = false;

		int spawnPlatformCount = 0;

		float posZTarget;

		public Camera camGame;

		void Awake()
		{
			audioSource = GetComponent<AudioSource>();

			Time.fixedDeltaTime = 1f/60f;
			Time.maximumDeltaTime = 5f/60f;

			var go = GameObject.Find("[DOTween]");

			if(go == null)
			{
				#if AADOTWEEN
				DOTween.Init();
				#endif
			}
		}

		IEnumerator Start()
		{
			bestScoreText.text = "best score: " + Util.GetBestScore().ToString();

			pointText.gameObject.SetActive(false);

			tutoAtStart.SetActive(true);

			diamond = Util.GetDiamond();

			diamondText.text = diamond.ToString();

			for(int i = 0; i < numberOfPlatformToSpawnedAtStart; i++)
			{
				SpawnPlatform();
			}

			while(true)
			{
				if (Input.GetMouseButton(0)) 
				{

//					GetPositionTouchX();

					if(!gameIsStarted)
						StartTheGame();

					break;
				}

				yield return 0;
			}
		}

		void StartTheGame()
		{
			if(!gameIsStarted)
			{
				PlayerMove();

				gameIsStarted = true;
			}

			pointText.gameObject.SetActive(true);
			bestScoreText.gameObject.SetActive(false);
			titleText.gameObject.SetActive(false);
			tutoAtStart.SetActive(false);
		}

		void OnUpdate()
		{
			if(isGameOver)
				return;

			if (Input.GetMouseButton(0)) 
			{
				if(!gameIsStarted)
					StartTheGame();

				var playerPos = playerParent.position;

				playerPos.x = Mathf.Lerp( playerPos.x, GetPositionTouchX(), 10f * Time.deltaTime);

				playerParent.position = playerPos;
			}
		}

		float GetPositionTouchX()
		{
			
			float x = 0;

			if(Application.isMobilePlatform)
			{
				if( Input.touchCount > 0)
				{

					Touch touch = Input.GetTouch(0);

					x = touch.position.x / Screen.width - 0.5f;
				}
			}
			else
			{
				x = Input.mousePosition.x / Screen.width - 0.5f;
			}

			if(x < -0.5f)
				x = -0.5f;

			if(x > 0.5f)
				x = 0.5f;

			return swipeSensibility * x;
		}

		void UpdateCamPosZ()
		{
			var pCam = camGame.transform.position;
			pCam.z = playerParent.position.z - 8.63f;
			camGame.transform.position = pCam;

			pCam = camGame.transform.position;
			pCam.z = playerParent.position.z - 8.63f;
			camGame.transform.position = pCam;
		}

		#if AADOTWEEN
		Ease playerAnimEase = Ease.OutCubic; //SetEase(Ease.OutQuad);
		#endif


		void PlayerMove()
		{
			CheckIfPlayerIsGrounded();

			if(isGameOver)
				return;

			speedBounceInSeconds -= 0.002f;

			if(speedBounceInSeconds < 0.15f)
				speedBounceInSeconds = 0.15f;

			AnimShadow();

			#if AADOTWEEN
			playerSphere.DOMoveY(2.5f, speedBounceInSeconds).SetLoops(2, LoopType.Yoyo).SetEase(playerAnimEase);
			#endif

			playerParent.position = new Vector3(playerParent.position.x, playerParent.position.y, posZTarget); 

			posZTarget += 5f;

			#if AADOTWEEN
			playerParent.DOMoveZ(posZTarget, speedBounceInSeconds * 2f)
				.SetEase(Ease.Linear)
				.OnUpdate(() => {
					OnUpdate();
					UpdateCamPosZ();
				})
				.OnComplete(() => {
					PlaySoundJump();
					PlayerMove();
				});
			#endif
		}

		void AnimShadow()
		{
			if(isGameOver)
				return;

			float shadowLocalScale = 0.08f;
			#if AADOTWEEN
			shadow.DOScale(0 * shadowLocalScale, speedBounceInSeconds)
				.SetEase(Ease.OutExpo)
				.OnComplete (()=>{
					shadow.DOScale(shadowLocalScale, speedBounceInSeconds)
						.SetEase(Ease.InExpo);
				});
			#endif
		}

		void CheckIfPlayerIsGrounded()
		{
			if(isGameOver)
				return;

			if(posZTarget > 4)
			{
				RaycastHit hit;

				Vector3 down = playerSphere.TransformDirection(Vector3.down);

				if (Physics.Raycast(playerSphere.position, down, out hit))
				{
					Platform platform = hit.transform.parent.GetComponent<Platform>();

					if(platform != null)
					{
						platform.OnPlayerBounce();
						Add1Point();
					}
				}
				else
				{
					GameOver();
				}
			}
		}

		void Add1Point()
		{
			if(isGameOver)
				return;

			point++;
			pointText.text = point.ToString();

			if(point % 3 == 0)
			{
				colors.Shuffle();

				#if AADOTWEEN
				plateformMaterial.DOColor(colors[0], 1);
				#endif
			}
		}

		public void Add1Diamond()
		{
			if(isGameOver)
				return;

			diamond++;
			diamondText.text = diamond.ToString();
			PlaySoundDiamond();
		}

		public void SpawnPlatform()
		{
			if(isGameOver)
				return;

			GameObject go = Instantiate(platformPrefab) as GameObject;

			Transform t = go.transform;

			SpawnPlatform(t);
		}

		const float posXmax = 3f; //-2.5f;

		public void SpawnPlatform(Transform t)
		{
			if(isGameOver)
				return;

			spawnPlatformCount ++;

			float posX = Util.GetRandomNumber(-posXmax, posXmax);

			if(Util.GetRandomNumber(0, 100f) < 70f)
			{
				if(Util.GetRandomNumber(0, 100f) < 50f)
				{
					posX = Util.GetRandomNumber(-posXmax, -1f);
				}
				else
				{
					posX = Util.GetRandomNumber(1f, posXmax);
				}
			}

			t.transform.position = new Vector3(posX, 0f, spawnPlatformCount * 5f);
		}

		void GameOver()
		{
			if(isGameOver)
				return;

			isGameOver = true;

			PlaySoundFall();

			Util.SetDiamond(diamond);
			Util.SetLastScore(point);

			#if AADOTWEEN
			DOTween.KillAll(false);
			playerParent.DOKill(false);
			playerSphere.DOKill(false);
			shadow.DOKill(false);
			#endif

			shadow.gameObject.SetActive(false);

			#if AADOTWEEN
			playerSphere.DOLocalMoveY(-10, 1).OnComplete(() => {
				ShowAds();

				Util.ReloadLevel();
			});
			#endif
		}

		void ShowAds()
		{
			int count = PlayerPrefs.GetInt("GAMEOVER_COUNT",0);
			count++;
			PlayerPrefs.SetInt("GAMEOVER_COUNT",count);
			PlayerPrefs.Save();

			#if APPADVISORY_ADS
			if(count > numberOfPlayToShowInterstitial)
			{

			if(AdsManager.instance.IsReadyInterstitial())
			{

			PlayerPrefs.SetInt("GAMEOVER_COUNT",0);
			AdsManager.instance.ShowInterstitial();
			}
			else
			{
			}

			}
			#endif
		}
	}
}