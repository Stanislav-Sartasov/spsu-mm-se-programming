package channels


class ConsumerImpl<T>(
    override val name: String,
    private val store: Store<T>,
) : Consumer<T> {

    init {
        store += this
    }


    override fun consume() {
        while (store.isRunning) {
            store.poll()?.run {
                println("$name: $this")
            }
            Thread.sleep(200)
        }
    }
}
