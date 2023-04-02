package channels


interface Store<T : Any> {

    /**
     * `True` if store is running, `false` otherwise.
     */
    val isRunning: Boolean


    /**
     * Send the [element] to the store.
     *
     * Store will reject the [element] if it [is not running][isRunning].
     *
     * @return `True` if the [element] was accepted, `false` otherwise.
     */
    fun send(element: T): Boolean

    /**
     * Receive an element from the store.
     *
     * This method may block the thread until the store is stopped
     * or some element becomes available to the callee.
     *
     * @return Non-null element if the store [is running][isRunning], `null` otherwise.
     */
    fun receive(): T?


    /**
     * Stop the store.
     */
    fun stop()
}
