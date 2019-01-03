using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LUT
{

	/// <summary>
	/// Controls the fades called by the ExtendedRenderer
	/// Developed by Lawendt. 
	/// Available @ https://github.com/Lawendt/UnityLawUtilities
	/// </summary>
	public class FadeManager : Singleton<FadeManager>
	{

		Dictionary<SpriteRenderer, Coroutine> fadeSpr;
		Dictionary<Text, Coroutine> fadeTextSize;
		Dictionary<Material, Coroutine> fadeMat;
		Dictionary<AudioSource, Coroutine> fadeSource;

		FadeManager()
		{
			fadeSpr = new Dictionary<SpriteRenderer, Coroutine>();
			fadeTextSize = new Dictionary<Text, Coroutine>();
			fadeMat = new Dictionary<Material, Coroutine>();
			fadeSource = new Dictionary<AudioSource, Coroutine>();

		}

		#region SpriteRenderer
		IEnumerator _fade(SpriteRenderer render, float from, float to, float second)
		{
			float vel = (to - from) / second;
			float time = second;

			Color cImg = render.color;
			cImg.a = from;
			while (time > 0)
			{
				time -= Time.deltaTime;

				cImg.a += vel * Time.deltaTime;

				render.color = cImg;

				yield return new WaitForFixedUpdate();
			}
			// Debug.Log("Exit Fade In " + btn.name);
			cImg.a = to;
			render.color = cImg;

			if (fadeSpr.ContainsKey(render))
			{
				fadeSpr.Remove(render);
			}
		}

		public void Fade(SpriteRenderer render, float from, float to, float second)
		{
			if (fadeSpr.ContainsKey(render))
			{
				Debug.LogWarning("There is a fade coroutine with " + render.name + " in progress.\n Stoping it to start the called");
				StopCoroutine(fadeSpr[render]);
				fadeSpr.Remove(render);
			}
			fadeSpr.Add(render, StartCoroutine(_fade(render, from, to, second)));

		}

		public void StopFade(SpriteRenderer render)
		{
			if (fadeSpr.ContainsKey(render))
			{
				fadeSpr.Remove(render);
			}
		}
		#endregion

		#region Text Size
		IEnumerator _fadeSize(Text render, float from, float to, float second)
		{
			float vel = (to - from) / second;
			float time = second;

			float size = from;
			render.fontSize = (int)size;

			while (time > 0)
			{
				time -= Time.deltaTime;

				size += vel * Time.deltaTime;

				render.fontSize = (int)size;
				yield return new WaitForFixedUpdate();
			}
			// Debug.Log("Exit Fade In " + btn.name);
			render.fontSize = (int)to;

			if (fadeTextSize.ContainsKey(render))
			{
				fadeTextSize.Remove(render);
			}
		}


		public void FadeSize(Text render, float from, float to, float second)
		{
			if (fadeTextSize.ContainsKey(render))
			{
				Debug.LogWarning("There is a fade size coroutine with " + render.name + " in progress.\n Stoping it to start the called");
				StopCoroutine(fadeTextSize[render]);
				fadeTextSize.Remove(render);
			}
			fadeTextSize.Add(render, StartCoroutine(_fadeSize(render, from, to, second)));

		}

		public void StopFadeSize(Text render)
		{
			if (fadeTextSize.ContainsKey(render))
			{
				fadeTextSize.Remove(render);
			}
		}
		#endregion

		#region Material
		IEnumerator _fade(Material render, float from, float to, float second)
		{
			float vel = (to - from) / second;
			float time = second;

			Color cImg = render.color;
			cImg.a = from;
			while (time > 0)
			{
				time -= Time.deltaTime;

				cImg.a += vel * Time.deltaTime;

				render.color = cImg;

				yield return new WaitForFixedUpdate();
			}
			// Debug.Log("Exit Fade In " + btn.name);
			cImg.a = to;
			render.color = cImg;

			if (fadeMat.ContainsKey(render))
			{
				fadeMat.Remove(render);
			}
		}

		public void Fade(Material render, float from, float to, float second)
		{
			if (fadeMat.ContainsKey(render))
			{
				Debug.LogWarning("There is a fade coroutine with " + render.name + " in progress.\n Stoping it to start the called");
				StopCoroutine(fadeMat[render]);
				fadeMat.Remove(render);
			}
			fadeMat.Add(render, StartCoroutine(_fade(render, from, to, second)));

		}

		public void StopFade(Material render)
		{
			if (fadeMat.ContainsKey(render))
			{
				fadeMat.Remove(render);
			}
		}
		#endregion

		#region AudioSource
		IEnumerator _fade(AudioSource render, float from, float to, float second, bool stopAfterEnd)
		{
			float vel = (to - from) / second;
			float time = second;

			float volume = from;
			render.volume = volume;
			while (time > 0)
			{
				time -= Time.deltaTime;

				volume += vel * Time.deltaTime;

				render.volume = volume;

				yield return new WaitForFixedUpdate();
			}
			// Debug.Log("Exit Fade In " + btn.name);


			if (fadeSource.ContainsKey(render))
			{
				fadeSource.Remove(render);
			}
			if (stopAfterEnd)
			{
				render.Stop();
			}
		}

		public void Fade(AudioSource render, float from, float to, float second, bool stopAfterEnd)
		{
			if (fadeSource.ContainsKey(render))
			{
				Debug.LogWarning("There is a fade coroutine with " + render.name + " in progress.\n Stoping it to start the called");
				StopCoroutine(fadeSource[render]);
				fadeSource.Remove(render);
			}
			fadeSource.Add(render, StartCoroutine(_fade(render, from, to, second, stopAfterEnd)));
		}

		public void StopFade(AudioSource render)
		{
			if (fadeSource.ContainsKey(render))
			{
				fadeSource.Remove(render);
			}
		}
		#endregion
	}
}
