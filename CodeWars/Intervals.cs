using System.Linq;
using System.Collections.Generic;

public class Intervals {
	public static int SumIntervals((int, int)[] intervals) {
		int sum = 0;
		List<(int, int)> mergedIntervals = new List<(int, int)>();

		bool sameSize = false;

		while (!sameSize) {
			foreach((int, int) pair in intervals) {
				List<(int, int)> mergedPairs = new List<(int, int)>();
				bool hasOverlapped = false;

				foreach((int, int) tracking in mergedIntervals) {
					if(AreOverlapped(pair, (tracking.Item1, tracking.Item2)) || AreOverlapped((tracking.Item1, tracking.Item2), pair)) {
						hasOverlapped = true;

						mergedPairs.Add(MergePair(tracking, pair));
					}
				}

				mergedIntervals.AddRange(hasOverlapped ? mergedPairs : new List<(int, int)>() { pair });
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
		return (Min(pair1.Item1, pair2.Item1), Max(pair1.Item2, pair2.Item2));
	}

	private static int Min(int value1, int value2) {
		if (value1 < value2)
			return value1;

		return value2;
	}

	private static int Max(int value1, int value2) {
		if (value1 > value2)
			return value1;

		return value2;
	}
}