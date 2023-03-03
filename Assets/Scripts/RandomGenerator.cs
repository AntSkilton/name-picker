using System.Collections.Generic;
using UnityEngine;

namespace NamePicker
{
	public static class RandomGenerator
	{
		public static List<int> SetupRandomIntList()
		{
			var availableInts = new List<int>();
			var randomIntList = new List<int>();
            
			for (int i = 0; i < MainManager.Current.PersonData.Count; i++)
			{
				availableInts.Add(i);
			}
            
			for (int i = 0; i < MainManager.Current.PersonData.Count; i++)
			{
				int randomIdx = Random.Range(0, availableInts.Count);
				randomIntList.Add(availableInts[randomIdx]);
				availableInts.RemoveAt(randomIdx);
			}

			return randomIntList;
		}

		public static List<int> CalculateTeamQuantities(int totalListQuantity, UiController.RosterMode teamQuantity)
		{
			teamQuantity += 1;
			var teamQuantities = new List<int>();
            
			var remainder = totalListQuantity % (int)teamQuantity;
			for (int i = 0; i < (int)teamQuantity; i++)
			{
				teamQuantities.Add((totalListQuantity - remainder) / (int)teamQuantity);
			}
            
			var idx = 0;
			while (remainder > 0)
			{
				teamQuantities[idx] += 1;
				idx++;
				remainder--;
			}

			return teamQuantities;
		}
	}
}