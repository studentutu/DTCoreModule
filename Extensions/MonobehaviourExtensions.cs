using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DT {
	public static class MonoBehaviourExtensions {
		public static T GetRequiredComponent<T>(this MonoBehaviour m) {
			return m.gameObject.GetRequiredComponent<T>();
		}

		public static void AddComponentIfMissing<T>(this MonoBehaviour m) where T : UnityEngine.Component {
			m.gameObject.AddComponentIfMissing<T>();
		}

		public static void SetSelfAsParent(this MonoBehaviour m, GameObject other) {
			other.transform.parent = m.transform;
		}

		public static T GetCachedComponent<T>(this MonoBehaviour m, Dictionary<Type, MonoBehaviour> cache, bool searchChildren = false) where T : class {
			return m.gameObject.GetCachedComponent<T>(cache, searchChildren);
		}

		public static T GetRequiredComponentInChildren<T>(this MonoBehaviour m) {
			return m.gameObject.GetRequiredComponentInChildren<T>();
		}

		public static T GetOnlyComponentInChildren<T>(this MonoBehaviour m) {
			return m.gameObject.GetOnlyComponentInChildren<T>();
		}

		public static T GetComponentInParent<T>(this MonoBehaviour m, bool includeInactive) {
			return m.gameObject.GetComponentInParent<T>(includeInactive);
		}

		public static T GetRequiredComponentInParent<T>(this MonoBehaviour m) {
			return m.gameObject.GetRequiredComponentInParent<T>();
		}

		public static Coroutine DoAfterDelay(this MonoBehaviour m, float delay, Action callback) {
			if (delay < 0) {
				delay = 0;
			}
			return m.StartCoroutine(m.DoActionAfterDelayCoroutine(delay, callback));
		}

		public static IEnumerator DoActionAfterDelayCoroutine(this MonoBehaviour m, float delay, Action callback) {
			yield return new WaitForSeconds(delay);
			callback.Invoke();
		}

		public static Coroutine DoAfterFrame(this MonoBehaviour m, Action callback) {
			return m.StartCoroutine(m.DoAfterFrameCoroutine(callback));
		}

		public static IEnumerator DoAfterFrameCoroutine(this MonoBehaviour m, Action callback) {
			yield return null;
			callback.Invoke();
		}

		public static Coroutine DoEveryFrameForDuration(this MonoBehaviour m, float duration, Action<float, float> frameCallback, Action finishedCallback = null) {
			return m.StartCoroutine(m.DoEveryFrameForDurationCoroutine(duration, frameCallback, finishedCallback));
		}

		public static IEnumerator DoEveryFrameForDurationCoroutine(this MonoBehaviour m, float duration, Action<float, float> frameCallback, Action finishedCallback = null) {
			for (float time = 0.0f; time < duration; time += Time.deltaTime) {
				frameCallback.Invoke(time, duration);
				yield return new WaitForEndOfFrame();
			}

			// add frame callback for final frame (when time == duration)
			frameCallback.Invoke(duration, duration);
			if (finishedCallback != null) {
				finishedCallback.Invoke();
			}
		}

		public static Coroutine DoLerpFor(this MonoBehaviour m, float duration, Action<float> lerpCallback, Action finishedCallback = null) {
			return m.StartCoroutine(DoLerpCoroutine(duration, lerpCallback, finishedCallback));
		}

		private static IEnumerator DoLerpCoroutine(float duration, Action<float> lerpCallback, Action finishedCallback) {
			for (float time = 0.0f; time <= duration; time += Time.deltaTime) {
				lerpCallback.Invoke(time / duration);
				yield return null;
			}

			lerpCallback.Invoke(1.0f);
			if (finishedCallback != null) {
				finishedCallback.Invoke();
			}
		}

		public static GameObject[] FindChildGameObjectsWithTag(this MonoBehaviour m, string tag) {
			return m.gameObject.FindChildGameObjectsWithTag(tag);
		}

		public static T[] GetDepthSortedComponentsInChildren<T>(this MonoBehaviour m, bool greatestDepthFirst = false) {
			return m.gameObject.GetDepthSortedComponentsInChildren<T>(greatestDepthFirst);
		}

		public static void DestroyAllChildren(this MonoBehaviour m, bool immediate = false) {
			m.gameObject.DestroyAllChildren(immediate);
		}
	}
}
