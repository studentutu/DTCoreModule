using DT;
﻿using System.Collections;
﻿using UnityEngine;

namespace DT {
	public class MathUtil {
		public const double epsilon = 0.0001;
		
		/// <summary>
		/// Pass in the relative position between two points, get QuadInOut (ref: easing.net)
		/// </summary>
		/// <param name="a">Start Point</param>
		/// <param name="b">End Point</param>
		/// <param name="r">0.0...1.0, Relative Position</param>
		public static float QuadInOut(float a, float b, float r) {
			if (r < 0.0f || r > 1.0f) {
				Debug.LogError("Invalid r (" + r + ") value for QuadInOut!");
				return 0.0f;
			}
			
			float m = (a + b) / 2.0f;
			r *= 2.0f;
			if (r < 1.0f) {
				return a + (Mathf.Pow(r, 2.0f) * (m - a));
			} else {
				return m + (Mathf.Pow((r - 1.0f), 1.0f / 2.0f) * (b - m));
			}
		}
	}
}