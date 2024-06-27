package com.example.notificationservice.repository

import com.example.notificationservice.model.Notification
import org.springframework.data.mongodb.repository.MongoRepository

interface NotificationRepository : MongoRepository<Notification, String> {
}