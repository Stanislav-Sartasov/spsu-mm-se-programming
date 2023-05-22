package examsystem

import collections.ConcurrentSet

class ExamSystemImpl(
    private val storage: ConcurrentSet<Credit>
) : ExamSystem {

    override val count: Int get() = storage.size

    override fun add(studentId: Long, courseId: Long) {
        storage.add(Credit(studentId, courseId))
    }

    override fun remove(studentId: Long, courseId: Long) {
        storage.remove(Credit(studentId, courseId))
    }

    override fun contains(studentId: Long, courseId: Long): Boolean =
        storage.contains(Credit(studentId, courseId))
}