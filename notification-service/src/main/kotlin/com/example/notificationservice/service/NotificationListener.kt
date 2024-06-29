package com.example.notificationservice.service

import com.example.notificationservice.model.Notification
import com.example.notificationservice.repository.NotificationRepository
import com.google.gson.JsonParser
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
        println(message)
        
        val title = JsonParser.parseString(message).asJsonObject.get("Title").asString
        val notification = Notification(message = "$title has been updated", type = "update")
        notificationRepository.save(notification)
        
        // messagingTemplate.convertAndSend("/topic/notifications")
    }
}