using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibers
{
    public static class ProcessManager
    {
        private static List<Tuple<Fiber, int>> myFibers = new List<Tuple<Fiber, int>>();
        private static List<Fiber> toDelete = new List<Fiber>();
        private static List<int> priorities = new List<int>();
        private static bool isPriority = false;
        private static List<Process> myProcesses = new List<Process>();
        private static int runningFiber = 0;

        public static void AddProcess(Process process)
        {
            myProcesses.Add(process);
        }

        public static void Start(bool isPriorityStrategy)
        {
            if (isPriorityStrategy)
            {
                isPriority = true;
            }

            InitializeFibers();

            Fiber.Switch(myFibers[0].Item1.Id);

            for (int i = 0; i < toDelete.Count; i++)
            {
                Fiber.Delete(toDelete[i].Id);
            }
            myFibers.Clear();
            toDelete.Clear();
            myProcesses.Clear();
            priorities.Clear();
            runningFiber = 0;
            isPriority = false;
        }

        public static void InitializeFibers()
        {
            for (int i = 0; i < myProcesses.Count; i++)
            {
                Fiber newFiber = new Fiber(myProcesses[i].Run);
                if (isPriority)
                {
                    priorities.Add(myProcesses[i].Priority);
                }
                myFibers.Add(Tuple.Create(newFiber, myProcesses[i].TotalDuration));
            }
            
        }

        public static void Switch(bool fiberFinished)
        {
            bool isFiberPicked = false;
            if (fiberFinished)
            {
                toDelete.Add(myFibers[runningFiber].Item1);
                myFibers.RemoveAt(runningFiber);
                if (isPriority)
                {
                    priorities.RemoveAt(runningFiber);
                }
            }

            if (myFibers.Count == 0)
            {
                Fiber.Switch(Fiber.PrimaryId);
                return;
            }

            int nextIndex = -1;
            if (fiberFinished)
            {
                if (!isPriority)
                {
                    nextIndex = 0;
                }
                else
                { 
                    int maxPriority = priorities.Max();
                    for (int i = 0; i < priorities.Count; i++)
                    {
                        if (priorities[i] == maxPriority)
                        {
                            nextIndex = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (!isPriority)
                {
                    for (int i = 0; i < myFibers.Count; i++)
                    {
                        if (myFibers[i].Item2 < myFibers[runningFiber].Item2)
                        {
                            nextIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    int maxPriority = priorities.Max();
                    while (!isFiberPicked && maxPriority > -1)
                    {
                        for (int i = 0; i < myFibers.Count; i++)
                        {
                            if (priorities[i] == maxPriority && myFibers[i].Item2 < myFibers[runningFiber].Item2)
                            {
                                nextIndex = i;
                                isFiberPicked = true;
                                break;
                            }
                        }
                        maxPriority--;
                    }
                }
                if (nextIndex == -1)
                {
                    for (int i = 0; i < myFibers.Count; i++)
                    {
                        if (i != runningFiber)
                        {
                            nextIndex = i;
                            break;
                        }
                    }
                    if (myFibers.Count == 1)
                    {
                        nextIndex = 0;
                    }
                }
            }
            runningFiber = nextIndex;
            Fiber.Switch(myFibers[nextIndex].Item1.Id);
        }
    }
}
