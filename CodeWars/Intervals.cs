using System;
using System.Linq;
using System.Collections.Generic;
using Interval = System.ValueTuple<int, int>;

public class Intervals {
	public static int SumIntervals((int, int)[] intervals) {
		int sum = 0;
		List<(int, int)> list = new List<(int, int)>(intervals);

		sum = intervals.Sum(x => x.Item2 - x.Item1);

		bool hasOverlap = true;

		while (hasOverlap) {
			List<(int, int)> newList = new List<(int, int)>();

			for (int i = 0; i < list.Count; i++) {
				(int, int) pair1 = list[i];

				for(int j = 0; j < list.Count; j++) {
					if (j == i)
						continue;

					(int, int) pair2 = list[j];

					if (AreOverlapped(pair1, pair2)) {
						newList.Add(MergePairs(pair1, pair2));
					}
				}

				if(newList.Count < list.Count) {
					list = newList;
					continue;
				}
			}

			hasOverlap = false;
		}

		return sum;
	}

	private static bool AreOverlapped((int, int) pair1, (int, int) pair2) {
		return
			(pair1.Item1 >= pair2.Item1 && pair1.Item1 <= pair2.Item2)
		|| (pair1.Item2 >= pair2.Item1 && pair1.Item2 <= pair2.Item2)
		|| (pair2.Item1 >= pair1.Item1 && pair2.Item1 <= pair1.Item2)
		|| (pair2.Item2 >= pair1.Item1 && pair2.Item2 <= pair1.Item2);
	}
	private static (int, int) MergePairs((int, int) pair1, (int, int) pair2) {
		return (GetMinValue(pair1.Item1, pair2.Item1), GetMaxValue(pair1.Item2, pair2.Item2));
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