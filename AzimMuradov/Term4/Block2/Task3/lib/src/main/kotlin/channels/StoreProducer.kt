package channels


class StoreProducer<T : Any>(
    private val store: Store<T>,
    private val producer: Sequence<T>,
) : Producer {

    override fun produce() {
        if (!store.isRunning) return
        producer.forEach { if (!store.send(it)) return }
    }
}
