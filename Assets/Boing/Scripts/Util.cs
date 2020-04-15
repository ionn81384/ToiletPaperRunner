
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
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
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif


namespace AppAdvisory.Boing
{
	/// <summary>
	/// Utility class.
	/// </summary>
	public static class Util
	{
		static System.Random random = new System.Random();

		public static double GetRandomNumber(double minimum, double maximum)
		{ 
			return random.NextDouble() * (maximum - minimum) + minimum;
		}

		public static float GetRandomNumber(float minimum, float maximum)
		{ 
			return (float)random.NextDouble() * (maximum - minimum) + minimum;
		}

		/// <summary>
		/// Real shuffle of List
		/// </summary>
		public static void Shuffle<T>(this IList<T> list)  
		{  
			int n = list.Count;  
			while (n > 1) {  
				n--;  
				int k = random.Next(n + 1);  
				T value = list[k];  
				list[k] = list[n];  
				list[n] = value;  
			}  
		}

		public static void SetLastScore(int score)
		{
			PlayerPrefs.SetInt("_LASTSCORE",score);

			PlayerPrefs.Save();

			SetBestScore(score);

			PlayerPrefs.Save();
		}

		public static void SetDiamond(int diamond)
		{
			PlayerPrefs.SetInt("_DIAMOND",diamond);

			PlayerPrefs.Save();
		}

		public static int GetDiamond()
		{
			return PlayerPrefs.GetInt("_DIAMOND", 0);
		}

		static void SetBestScore(int score)
		{
			int b = GetBestScore();

			if(score > b)
				PlayerPrefs.SetInt("_BESTSCORE",score);
		}

		public static int GetBestScore()
		{
			return PlayerPrefs.GetInt("_BESTSCORE",0);
		}

		public static int GetLastScore()
		{
			return PlayerPrefs.GetInt("_LASTSCORE",0);
		}

		/// <summary>
		/// Clean the memory and reload the scene
		/// </summary>
		public static void ReloadLevel()
		{
			CleanMemory();

			#if UNITY_5_3_OR_NEWER
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
			#else
			Application.LoadLevel(Application.loadedLevel);
			#endif

			CleanMemory();
		}
		/// <summary>
		/// Clean the memory
		/// </summary>
		public static void CleanMemory()
		{
			#if AADOTWEEN
			DOTween.KillAll();
			#endif
			GC.Collect();
			Application.targetFrameRate = 60;
		}

		public static void SetAlpha(this Text text, float alpha)
		{
			Color c = text.color;
			c.a = alpha;
			text.color = c;
		}

		public static void SetScaleX(this RectTransform rect, float scale)
		{
			var s = rect.localScale;
			s.x = scale;
			rect.localScale = s;
		}
	}
}