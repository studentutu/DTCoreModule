using DT;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DT {
	public static class DateTimeUtil {
		public static bool SameDay(DateTime a, DateTime b) {
			return a.Date == b.Date;
		}
	}
}
