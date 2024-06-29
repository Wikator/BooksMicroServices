package com.example.notificationservice.controller

import com.example.notificationservice.model.Notification
import com.example.notificationservice.repository.NotificationRepository
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController

@RestController
@RequestMapping("api/notifications")
class NotificationController(private val notificationRepository: NotificationRepository) {
    
    @GetMapping
    fun getAllNotifications(): List<Notification> = notificationRepository.findAll()
}