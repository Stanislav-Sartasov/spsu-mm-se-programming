package examsystem

data class Exam(
    val courseId: Long,
    val studentId: Long,
) : Comparable<Exam> {
    companion object {
        private val comparator = compareBy<Exam>(
            { it.courseId },
            { it.studentId }
        )
    }

    override fun compareTo(other: Exam): Int = comparator.compare(this, other)
}