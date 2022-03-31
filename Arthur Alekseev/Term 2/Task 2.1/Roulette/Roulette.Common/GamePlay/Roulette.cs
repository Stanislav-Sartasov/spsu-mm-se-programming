using System;
using System.Collections.Generic;

namespace Roulette.Common.GamePlay
{
	public class Roulette
	{
		private readonly List<Field> _fields;
		private readonly Random _random;

		public Roulette()
		{
			_fields = new List<Field>();
			_random = new Random();

			FillFields();
		}

		private void FillFields()
		{
			int[] numbers =
			{
				32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16,
				33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26
			};

			var index = 0;

			_fields.Add(new Field(0, Color.Green));

			foreach (var number in numbers)
			{
				if (index % 2 == 0)
					_fields.Add(new Field(number, Color.Red));
				else
					_fields.Add(new Field(number, Color.Black));
				index++;
			}
		}

		public Field GetRandomField()
		{
			return _fields[_random.Next() % 37];
		}
	}
}