package com.example.notificationservice.model

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document
import java.time.LocalDateTime

@Document(collection = "notifications")
data class Notification (
    @Id
    val id: String? = null,
    val message: String,
    val type: String,
    val createdAt: LocalDateTime = LocalDateTime.now()
)
