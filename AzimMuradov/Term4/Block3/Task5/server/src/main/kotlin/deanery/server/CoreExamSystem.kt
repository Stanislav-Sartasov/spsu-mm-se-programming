package deanery.server

import deanery.ConcurrentSetExamSystem
import deanery.ExamSystem
import deanery.set.ConcurrentSetImpl


object CoreExamSystem : ExamSystem by ConcurrentSetExamSystem(set = ConcurrentSetImpl())
