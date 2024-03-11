package examsystem

import java.util.concurrent.atomic.AtomicInteger

class OptimisticExamSystem : IExamSystem {
    private val finalElement: Node<Exam> = Node(Exam(-1, -1))
    private val head: Node<Exam> = Node(Exam(-1, -1), finalElement)
    private var count: AtomicInteger = AtomicInteger(0)

    override val Count: Int
        get() = count.get()

    //the first element is always -1 -1, and there's a closing element after which 'next' element is none

    private fun find(element: Exam): Pair<Node<Exam>, Node<Exam>> {
        var current = head
        var found = false

        while (true) {
            while (!found) {
                found =
                    (current.next!! == finalElement) || (current.value == element) || (current.next!!.value > element)
                if (found) {
                    current.lock.lock()
                    current.next!!.lock.lock()
                } else {
                    current = current.next!!
                }
            }

            if (!((current.next!! == finalElement) || (current.value == element) || (current.next!!.value > element))) {
                current.lock.unlock()
                current.next!!.lock.unlock()
                continue
            }

            var check = head
            while ((check != current) && (check != finalElement)) {
                check = check.next!!
            }
            if (check == current)
                break
            else{
                current.lock.unlock()
                current.next!!.lock.unlock()
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

        while (true) {
            while (!found) {
                found =
                    (current.next!! == finalElement) || (current.next!!.value == element) || (current.next!!.value > element)
                if (found) {
                    current.lock.lock()
                    current.next!!.lock.lock()
                } else {
                    current = current.next!!
                }
            }

            if (!((current.next!! == finalElement) || (current.next!!.value == element) || (current.next!!.value > element))) {
                current.lock.unlock()
                current.next!!.lock.unlock()
                continue
            }

            var check = head
            while (check != current && check != finalElement)
                check = check.next!!
            if (check == current)
                break
            else{
                current.lock.unlock()
                current.next!!.lock.unlock()
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