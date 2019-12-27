using System;
using System.Linq;
using System.Collections.Generic;

public class Intervals {
	public static int SumIntervals((int, int)[] intervals) {
		//return intervals
		//	.SelectMany(i => Enumerable.Range(i.Item1, i.Item2 - i.Item1))
		//	.Distinct()
		//	.Count();

		int sum = 0;
		List<(int, int)> mergedIntervals = new List<(int, int)>();

		bool sameSize = false;

		while (!sameSize) {
			mergedIntervals.Clear();

			foreach((int, int) pair in intervals) {
				//List<(int, int)> mergedPairs = new List<(int, int)>();
				bool hasOverlapped = false;

				for(int i = 0; i < mergedIntervals.Count; i++) {
					var tracking = mergedIntervals[i];

					if(AreOverlapped(pair, (tracking.Item1, tracking.Item2))
					|| AreOverlapped((tracking.Item1, tracking.Item2), pair)) {
						hasOverlapped = true;

						mergedIntervals[i] = MergePair(tracking, pair);
						break;
					}
				}

				if(!hasOverlapped)
					mergedIntervals.Add(pair);
			}

			sameSize = intervals.Length == mergedIntervals.Count;
			intervals = mergedIntervals.ToArray();
		}

		sum = mergedIntervals.Sum(x => x.Item2 - x.Item1);

		return sum;
	}

	private static bool AreOverlapped((int, int) pair1, (int, int) pair2) {
		return (pair1.Item1 <= pair2.Item1 && pair1.Item2 >= pair2.Item1 && pair1.Item2 <= pair2.Item2)
		|| (pair1.Item1 <= pair2.Item1 && pair1.Item2 >= pair2.Item2);
	}

	/// <summary>
	/// Merge 2 pairs
	/// </summary>
	/// <param name="pair1"></param>
	/// <param name="pair2"></param>
	/// <returns></returns>
	private static (int, int) MergePair((int, int) pair1, (int, int) pair2) {
		return (Math.Min(pair1.Item1, pair2.Item1), Math.Max(pair1.Item2, pair2.Item2));
	}
}