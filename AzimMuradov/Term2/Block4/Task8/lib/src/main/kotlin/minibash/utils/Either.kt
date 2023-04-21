package minibash.utils

sealed interface Either<out L, out R> {

    data class Left<L>(val value: L) : Either<L, Nothing>

    data class Right<R>(val value: R) : Either<Nothing, R>
}

fun <T> T.left() = Either.Left(value = this)

fun <T> T.right() = Either.Right(value = this)

inline fun <L, R, T> Either<L, R>.fold(
    onLeft: (L) -> T,
    onRight: (R) -> T,
) = when (this) {
    is Either.Left -> onLeft(value)
    is Either.Right -> onRight(value)
}
