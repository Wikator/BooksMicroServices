package com.example.notificationservice.service

import com.example.notificationservice.model.Notification
import com.example.notificationservice.repository.NotificationRepository
import org.springframework.amqp.rabbit.annotation.RabbitListener
import org.springframework.messaging.simp.SimpMessagingTemplate
import org.springframework.stereotype.Component

@Component
class NotificationListener(
    private val notificationRepository: NotificationRepository,
    private val messagingTemplate: SimpMessagingTemplate
) {
    
    @RabbitListener(queues = ["book-changed"])
    fun receiveMessage(message: String) {
        val notification = Notification(message = message)
        println(message)
        notificationRepository.save(notification)
        
        messagingTemplate.convertAndSend("/topic/notifications")
    }
}