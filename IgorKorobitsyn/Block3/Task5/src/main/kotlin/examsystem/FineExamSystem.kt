package examsystem

import java.util.concurrent.atomic.AtomicInteger

class FineExamSystem : IExamSystem {
    private val finalElement: Node<Exam> = Node(Exam(-1, -1))
    private val head: Node<Exam> = Node(Exam(-1, -1), finalElement)
    private var count: AtomicInteger = AtomicInteger(0)

    override val Count: Int
        get() = count.get()

    //the first element is always -1 -1, and there's a closing element after which 'next' element is none

    private fun find(element: Exam): Pair<Node<Exam>, Node<Exam>> {
        var current = head
        var found = false
        current.lock.lock()
        while (!found) {
            current.next!!.lock.lock()
            try {
                found =
                    (current.next!! == finalElement) || (current.value == element) || (current.next!!.value > element)
            } finally {
                if (!found) {
                    val oldCurrent = current
                    current = current.next!!
                    oldCurrent.lock.unlock()
                }
            }
        }
        return Pair(current, current.next!!)
    }

    override fun Add(studentId: Long, courseId: Long): Boolean {
        val element = Exam(courseId, studentId)
        val (node, next) = find(element)
        return try {
            if (node.value != element) {
                val newNode = Node<Exam>(element, next)
                node.next = newNode
                count.incrementAndGet()
                true
            } else {
                false
            }
        } finally {
            next.lock.unlock()
            node.lock.unlock()
        }

    }

    override fun Contains(studentId: Long, courseId: Long): Boolean {
        val element = Exam(courseId, studentId)
        val (node, next) = find(element)
        return try {
            node.value == element
        } finally {
            next.lock.unlock()
            node.lock.unlock()
        }
    }

    override fun Remove(studentId: Long, courseId: Long): Boolean {
        val element = Exam(courseId, studentId)

        //the next element should be equal to the one which needs to found instead of the first one

        var current = head
        var found = false
        current.lock.lock()
        while (!found) {
            current.next!!.lock.lock()
            try {
                found =
                    (current.next!! == finalElement) || (current.next!!.value == element) || (current.next!!.value > element)
            } finally {
                if (!found) {
                    val oldCurrent = current
                    current = current.next!!
                    oldCurrent.lock.unlock()
                }
            }
        }

        val (node, next) = Pair(current, current.next!!)
        return try {
            if (next.value == element) {
                node.next = next.next
                count.decrementAndGet()
                true
            } else {
                false
            }
        } finally {
            next.lock.unlock()
            node.lock.unlock()
        }
    }
}