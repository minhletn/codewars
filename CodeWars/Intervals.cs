using System.Linq;
using System.Collections.Generic;

public class Intervals {
	public static int SumIntervals((int, int)[] intervals) {
		int sum = 0;
		List<(int, int)> trimmedIntervals = new List<(int, int)>();

		foreach ((int, int) pair in intervals) {
			List<(int, int)> trimmedPairs = new List<(int, int)>();
			bool hasOverlapped = false;

			foreach ((int, int) tracking in trimmedIntervals) {
				if (AreOverlapped(pair, (tracking.Item1, tracking.Item2)) || AreOverlapped((tracking.Item1, tracking.Item2), pair)) {
					hasOverlapped = true;

					trimmedPairs.AddRange(TrimPair(tracking, pair));
				}
			}

			trimmedIntervals.AddRange(hasOverlapped ? trimmedPairs : new List<(int, int)>() { pair });
		}

		sum = trimmedIntervals.Sum(x => x.Item2 - x.Item1);

		return sum;
	}

	private static bool AreOverlapped((int, int) pair1, (int, int) pair2) {
		return (pair1.Item1 <= pair2.Item1 && pair1.Item2 >= pair2.Item1 && pair1.Item2 <= pair2.Item2)
		|| (pair1.Item1 <= pair2.Item1 && pair1.Item2 >= pair2.Item2);
	}

	/// <summary>
	/// Trim pair2 off any overlapping with pair1 and return
	/// </summary>
	/// <param name="pair1"></param>
	/// <param name="pair2"></param>
	/// <returns></returns>
	private static List<(int, int)> TrimPair((int, int) pair1, (int, int) pair2) {

		//1-3 and 2-4
		if(pair1.Item1 <= pair2.Item1 && pair1.Item2 >= pair2.Item1 && pair1.Item2 <= pair2.Item2) {
			return new List<(int, int)> { (pair1.Item2, pair2.Item2) };
		}

		//1-3 and 0-4
		if(pair2.Item1 <= pair1.Item1 && pair2.Item2 >= pair1.Item2) {
			return new List<(int, int)> {
				(pair2.Item1, pair1.Item1),
				(pair1.Item2, pair2.Item2)
			};
		}

		//2-4 and 1-3
		if(pair1.Item1 >= pair2.Item1 && pair1.Item1 <= pair2.Item2 && pair1.Item2 >= pair2.Item2) {
			return new List<(int, int)> { (pair2.Item1, pair1.Item1) };
		}

		return new List<(int, int)>();
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