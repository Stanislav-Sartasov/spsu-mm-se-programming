package filters

open class Filter {
    protected fun Boolean.toInt() = if (this) 1 else 0
}