package com.example.notificationservice.health

import org.springframework.amqp.rabbit.connection.ConnectionFactory
import org.springframework.boot.actuate.health.Health
import org.springframework.boot.actuate.health.HealthIndicator
import org.springframework.stereotype.Component

@Component
class RabbitMQHealthIndicator(
    private val connectionFactory: ConnectionFactory
) : HealthIndicator {

    override fun health(): Health {
        return try {
            val connection = connectionFactory.createConnection()
            if (connection.isOpen) {
                Health.up().withDetail("RabbitMQ", "Connected").build()
            } else {
                Health.down().withDetail("RabbitMQ", "Not connected").build()
            }
        } catch (ex: Exception) {
            Health.down(ex).withDetail("RabbitMQ", "Not connected").build()
        }
    }
}