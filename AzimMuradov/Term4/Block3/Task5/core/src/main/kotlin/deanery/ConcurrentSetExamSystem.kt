package deanery

import deanery.set.ConcurrentSet


class ConcurrentSetExamSystem(private val set: ConcurrentSet<Pair<StudentId, CourseId>>) : ExamSystem {

    override val count: Int get() = set.count


    override fun contains(studentId: StudentId, courseId: CourseId) = set.contains(studentId to courseId)

    override fun add(studentId: StudentId, courseId: CourseId) = set.add(studentId to courseId)

    override fun remove(studentId: StudentId, courseId: CourseId) = set.remove(studentId to courseId)
}
