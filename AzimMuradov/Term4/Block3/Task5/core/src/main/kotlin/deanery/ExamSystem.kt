package deanery


interface ExamSystem {

    val count: Int


    fun contains(studentId: StudentId, courseId: CourseId): Boolean

    fun add(studentId: StudentId, courseId: CourseId)

    fun remove(studentId: StudentId, courseId: CourseId)
}

typealias StudentId = Long

typealias CourseId = Long
