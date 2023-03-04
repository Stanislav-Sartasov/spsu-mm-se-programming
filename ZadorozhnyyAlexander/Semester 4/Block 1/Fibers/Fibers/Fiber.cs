using System;

namespace Fibers
{
    public class Fiber
    {

        /// <summary>
        /// The fiber action delegate.
        /// </summary>
        private Action action;

        /// <summary>
        /// Gets the fiber identifier.
        /// </summary>
        public uint Id { get; private set; }

        /// <summary>
        /// Gets the id of the primary fiber.
        /// </summary>
        /// <remarks>If the Id is 0 then this means that there is no primary Id on the fiber.</remarks>
        public static uint PrimaryId { get; private set; }

        /// <summary>
        /// Gets the flag identifing the primary fiber (a fiber that can run other fibers).
        /// </summary>
        public bool IsPrimary { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fiber"/> class.
        /// </summary>
        /// <param name='action'>Action.</param>
        public Fiber(Action action)
        {
            InnerCreate(action);
        }

        /// <summary>
        /// Deletes the current fiber.
        /// </summary>
        /// <remarks>This method should only be used in the fiber action that's executing.</remarks>
        public void Delete()
        {
            UnmanagedFiberAPI.DeleteFiber(Id);
        }

        /// <summary>
        /// Deletes the fiber with the specified fiber id.
        /// </summary>
        /// <param name='fiberId'>fiber id.</param>
        public static void Delete(uint fiberId)
        {
            UnmanagedFiberAPI.DeleteFiber(fiberId);
        }

        /// <summary>
        /// Switches the execution context to the next fiber.
        /// </summary>
        /// <param name='fiberId'>Fiber id.</param>
        public static void Switch(uint fiberId)
        {
            // for debug only and to show that indeed it works! Remove this line!!!
            Console.WriteLine(string.Format("Fiber [{0}] Switch", fiberId));

            UnmanagedFiberAPI.SwitchToFiber(fiberId);
        }

        /// <summary>
        /// Creates the fiber.
        /// </summary>
        /// <remarks>This method is responsible for the *actual* fiber creation.</remarks>
        /// <param name='action'>Fiber action.</param>
        private void InnerCreate(Action action)
        {
            this.action = action;

            if (PrimaryId == 0)
            {
                PrimaryId = UnmanagedFiberAPI.ConvertThreadToFiber(0);
                IsPrimary = true;
            }

            UnmanagedFiberAPI.LPFIBER_START_ROUTINE lpFiber = FiberRunnerProc;
            Id = UnmanagedFiberAPI.CreateFiber(100500, lpFiber, 0);
        }

        /// <summary>
        /// Fiber method that executes the fiber action.
        /// </summary>
        /// <param name='lpParam'>Lp parameter.</param>
        /// <returns>fiber status code.</returns>
        private uint FiberRunnerProc(uint lpParam)
        {
            uint status = 0;

            try
            {
                action();
            }
            catch (Exception)
            {
                status = 1;
                throw;
            }
            finally
            {
                if (status == 1)
                    UnmanagedFiberAPI.DeleteFiber((uint)Id);
            }

            return status;
        }
    }
}
