
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

namespace AppAdvisory.Boing
{
	public class Platform : MonoBehaviour 
	{
		GameManager gameManager;

		public Transform jumpEffect;

		public Transform cube;

		public GameObject diamond;

		void Awake()
		{
			this.gameManager = FindObjectOfType<GameManager>();
			jumpEffect.transform.localScale = Vector3.zero;
			jumpEffect.gameObject.SetActive(false);
			diamond.SetActive(false);
		}

		void Start()
		{
			StartCoroutine(CoroutDestroyIfNotVisible());
		}

		public void OnPlayerBounce()
		{
			jumpEffect.transform.localScale = Vector3.zero;
			jumpEffect.gameObject.SetActive(true);

			#if AADOTWEEN
			jumpEffect.DOScale(new Vector3(3f, 0.3f, 3f), 0.2f).SetLoops(2, LoopType.Yoyo);
			#endif

			if(diamond.activeInHierarchy)
			{
				gameManager.Add1Diamond();
				diamond.SetActive(false);
			}
		}

		IEnumerator CoroutDestroyIfNotVisible()
		{
			if(gameManager.point > 0)
			{
				transform.position = new Vector3(transform.position.x, 10f, transform.position.z);

				#if AADOTWEEN
				transform.DOMoveY(0,0.2f);
				#endif
			}

			if(gameManager.point > 5 && Util.GetRandomNumber(0f, 100f) < 30f)
			{
				diamond.SetActive(true);
			}

			yield return new WaitForSeconds(1f);

			while(true)
			{
				if(gameManager.camGame.transform.position.z - transform.position.z > 5)
				{
					DODestroy();
					break;
				}

				yield return new WaitForSeconds(0.3f);
			}
		}

		void DODestroy()
		{
			StopAllCoroutines();
			gameManager.SpawnPlatform(this.transform);
			StartCoroutine(CoroutDestroyIfNotVisible());
		}
	}
}