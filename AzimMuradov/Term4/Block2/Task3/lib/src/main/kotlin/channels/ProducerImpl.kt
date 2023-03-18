package channels


class ProducerImpl(
    override val name: String,
    private val store: Store<String>,
) : Producer<String> {

    init {
        store += this
    }


    override fun produce() = repeat(times = 7) {
        store.offer(element = "$name: message #$it")
        Thread.sleep(200)
    }
}
