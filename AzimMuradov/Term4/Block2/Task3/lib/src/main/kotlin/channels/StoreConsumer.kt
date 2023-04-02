package channels


class StoreConsumer<T : Any>(
    private val store: Store<T>,
    private val consumer: (Sequence<T>) -> Unit,
) : Consumer {

    override fun consume() = consumer(generateSequence(store::receive))
}
