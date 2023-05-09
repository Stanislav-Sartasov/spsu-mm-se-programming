package domain

import kotlinx.serialization.Serializable
import kotlin.random.Random

@Serializable
data class User(
    val name: String = "_",
    val id: Int = Random.nextInt()
)
