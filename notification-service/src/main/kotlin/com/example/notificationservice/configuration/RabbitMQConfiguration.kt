package com.example.notificationservice.configuration

import org.springframework.amqp.core.Queue
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration

@Configuration
class RabbitMQConfiguration {

    @Bean
    fun bookChangedQueue(): Queue {
        return Queue("book-changed", true)
    }
}