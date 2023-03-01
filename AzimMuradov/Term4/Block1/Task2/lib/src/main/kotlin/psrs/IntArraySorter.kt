package psrs


/**
 * An integer array sorter.
 */
fun interface IntArraySorter {

    /**
     * Sorts the [array] in-place in ascending numerical order.
     */
    fun sort(array: IntArray)
}
