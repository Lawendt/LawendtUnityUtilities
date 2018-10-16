using UnityEngine;
using UnityEngine.UI;

namespace LUT
{

	/// <summary>
	/// Add a linear fade to some classes.
	/// Developed by lawendt. Available on: https://github.com/Lawendt/UnityLawUtilities
	/// </summary>
	public static class ExtendedRenderer
	{


		#region SpriteRenderer

		public static void Fade(this SpriteRenderer renderer, float to, float second)
		{
			FadeManager.Instance.Fade(renderer, renderer.color.a, to, second);
		}

		public static void Fade(this SpriteRenderer renderer, float from, float to, float second)
		{
			FadeManager.Instance.Fade(renderer, from, to, second);
		}


		public static void StopFade(this SpriteRenderer renderer)
		{
			FadeManager.Instance.StopFade(renderer);
		}

		#endregion

		#region Text Size

		public static void FadeSize(this Text renderer, float to, float second)
		{
			FadeManager.Instance.FadeSize(renderer, renderer.fontSize, to, second);
		}

		public static void FadeSize(this Text renderer, float from, float to, float second)
		{
			FadeManager.Instance.FadeSize(renderer, from, to, second);
		}

		public static void StopFadeSize(this Text renderer)
		{
			FadeManager.Instance.StopFadeSize(renderer);
		}

		#endregion

		#region Material

		public static void Fade(this Material renderer, float to, float second)
		{
			FadeManager.Instance.Fade(renderer, renderer.color.a, to, second);
		}

		public static void Fade(this Material renderer, float from, float to, float second)
		{
			FadeManager.Instance.Fade(renderer, from, to, second);
		}

		public static void StopFade(this Material renderer)
		{
			FadeManager.Instance.StopFade(renderer);
		}

		#endregion

		#region AudioSource

		public static void Fade(this AudioSource renderer, float toVolume, float second, bool stopAfterEnd = false)
		{
			FadeManager.Instance.Fade(renderer, renderer.volume, toVolume, second, stopAfterEnd);
		}

		public static void Fade(this AudioSource renderer, float from, float toVolume, float second, bool stopAfterEnd = false)
		{
			FadeManager.Instance.Fade(renderer, from, toVolume, second, stopAfterEnd);
		}

		public static void StopFade(this AudioSource renderer)
		{
			FadeManager.Instance.StopFade(renderer);
		}
		#endregion

	}
}
