using System;
using System.Linq;
using System.Collections.Generic;
using Interval = System.ValueTuple<int, int>;

public class Intervals {
	public static int SumIntervals((int, int)[] intervals) {
		int sum = 0;
		List<(int, int)> trimmedIntervals = new List<(int, int)>();

		foreach ((int, int) pair in intervals) {
			(int, int) trimmedPair = pair;

			foreach ((int, int) tracking in trimmedIntervals) {
				if (AreOverlapped(pair, (tracking.Item1, tracking.Item2))) {
					trimmedPair = TrimPair(tracking, pair);
				}
			}

			trimmedIntervals.Add(trimmedPair);
		}

		sum = trimmedIntervals.Sum(x => x.Item2 - x.Item1);

		return sum;
	}

	private static bool AreOverlapped((int, int) pair1, (int, int) pair2) {
		return
			(pair1.Item1 >= pair2.Item1 && pair1.Item1 <= pair2.Item2)
		|| (pair1.Item2 >= pair2.Item1 && pair1.Item2 <= pair2.Item2)
		|| (pair2.Item1 >= pair1.Item1 && pair2.Item1 <= pair1.Item2)
		|| (pair2.Item2 >= pair1.Item1 && pair2.Item2 <= pair1.Item2);
	}

	/// <summary>
	/// Trim pair2 off any overlapping with pair1 and return
	/// </summary>
	/// <param name="pair1"></param>
	/// <param name="pair2"></param>
	/// <returns></returns>
	private static (int, int) TrimPair((int, int) pair1, (int, int) pair2) {

		if (pair2.Item1 < pair1.Item1 && pair2.Item2 < pair1.Item2)
			return (pair2.Item1, pair1.Item1);

		if(pair2.Item2 < pair1.Item1)
			return (pair2.Item1, pair1.Item1);
	}

	private static int GetMinValue(int value1, int value2) {
		if (value1 > value2)
			return value2;

		return value1;
	}

	private static int GetMaxValue(int value1, int value2) {
		if(value1 > value2)
			return value1;

		return value2;
	}
}