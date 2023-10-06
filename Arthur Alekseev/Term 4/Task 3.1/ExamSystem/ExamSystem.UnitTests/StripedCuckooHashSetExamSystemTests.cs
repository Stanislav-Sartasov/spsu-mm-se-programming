﻿using ExamSystem.Core.DataStructures;
using ExamSystem.Core.ExamSystems;
using ExamSystem.Core.Sets.StripedCuckooHashSet;

namespace ExamSystem.UnitTests;

public class StripedCuckooHashSetExamSystemTests
{
	[Test]
	public void TestOne()
	{
		var system = CreateLockFreeSystem();

		ExamSystemTestCreator.TestAddUser(system, 1, 1);
	}

	[Test]
	public void TestTen()
	{
		var system = CreateLockFreeSystem();

		ExamSystemTestCreator.TestAddUser(system, 10, 1);
	}

	[Test]
	public void TestTenFourThreads()
	{
		var system = CreateLockFreeSystem();

		ExamSystemTestCreator.TestAddUser(system, 10, 4);
	}

	[Test]
	public void TestThousandFourThreads()
	{
		var system = CreateLockFreeSystem();

		ExamSystemTestCreator.TestAddUser(system, 1000, 4);
	}

	[Test]
	public void TestTenThousand()
	{
		var system = CreateLockFreeSystem();

		ExamSystemTestCreator.TestAddUser(system, 10000, 1);
	}

	[Test]
	public void TestThousandSixThreads()
	{
		var system = CreateLockFreeSystem();

		ExamSystemTestCreator.TestAddUser(system, 1000, 6);
	}

	private IExamSystem CreateLockFreeSystem()
	{
		var set = new StripedCuckooHashSet<HashLongTuple>(40);

		return new Core.ExamSystems.ExamSystem(set);
	}
}