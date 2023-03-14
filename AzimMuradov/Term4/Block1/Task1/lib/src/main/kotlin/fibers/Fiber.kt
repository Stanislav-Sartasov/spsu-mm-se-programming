package fibers


/**
 * Fiber.
 *
 * @property [action] The fiber action.
 */
class Fiber(private var action: () -> Unit) {

    /** The fiber identifier. */
    var id: Long = 0
        private set

    /** The flag identifying the primary fiber (a fiber that can run other fibers). */
    val isPrimary: Boolean get() = id == primaryId

    init {
        create()
    }

    /**
     * Creates the fiber.
     *
     * This method is responsible for the *actual* fiber creation.
     */
    private fun create() {
        if (primaryId == 0L) {
            primaryId = UnmanagedFiberAPI.cnt // UnmanagedFiberAPI.kernel32.ConvertThreadToFiber(lpParameter = 0)
        }

        id = UnmanagedFiberAPI.cnt++ /* UnmanagedFiberAPI.kernel32.CreateFiber(
            dwStackSize = 100500,
            lpStartAddress = ::fiberRunnerProc,
            lpParameter = 0
        ) */
    }

    /**
     * Fiber method that executes the fiber action.
     *
     * @param [lpParam] Lp parameter.
     * @return Fiber status code.
     */
    private fun fiberRunnerProc(lpParam: Int): Int {
        var status = 0

        try {
            action()
        } catch (e: Exception) {
            status = 1
            throw e
        } finally {
            if (status == 1) {
                // UnmanagedFiberAPI.kernel32.DeleteFiber(id)
            }
        }

        return 0
    }

    /**
     * Deletes the current fiber.
     *
     * This method should only be used in the fiber action that's executing.
     */
    fun delete() = delete(id)


    companion object {

        /**
         * The id of the primary fiber.
         *
         * If the id is 0 then this means that there is no primary id on the fiber.
         */
        var primaryId = 0L
            private set

        /**
         * Deletes the fiber with the specified fiber id.
         *
         * @param [fiberId] Fiber id.
         */
        fun delete(fiberId: Long) {
            // UnmanagedFiberAPI.kernel32.DeleteFiber(fiberId)
        }

        /**
         * Switches the execution context to the next fiber.
         *
         * @param [fiberId] Fiber id.
         */
        fun switch(fiberId: Long) {
            // For debug only and to show that indeed it works! Remove this line!!!
            // println("Fiber [$fiberId] Switch")
            // UnmanagedFiberAPI.kernel32.SwitchToFiber(fiberId)
        }
    }
}
