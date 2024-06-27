package examsystem

import java.util.concurrent.locks.ReentrantLock

class Node<T : Comparable<T>>(val value: T, var next: Node<T>? = null) {
    val lock = ReentrantLock()
}