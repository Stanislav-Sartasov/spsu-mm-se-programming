package deanery


interface ExamSystem {

    val count: Int


    fun add(studentId: StudentId, courseId: CourseId)

    fun remove(studentId: StudentId, courseId: CourseId)

    fun contains(studentId: StudentId, courseId: CourseId): Boolean
}

typealias StudentId = Long

typealias CourseId = Long
