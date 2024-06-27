package examsystem

interface IExamSystem {
    fun Add(studentId: Long, courseId: Long): Boolean

    fun Remove(studentId: Long, courseId: Long): Boolean

    fun Contains(studentId: Long, courseId: Long): Boolean

    val Count: Int
}