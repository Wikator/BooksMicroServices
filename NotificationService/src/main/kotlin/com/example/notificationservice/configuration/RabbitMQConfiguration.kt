package com.example.notificationservice.configuration

import org.springframework.amqp.core.Binding
import org.springframework.amqp.core.BindingBuilder
import org.springframework.amqp.core.DirectExchange
import org.springframework.amqp.core.Queue
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration


@Configuration
class RabbitMQConfiguration {

    @Bean
    fun notificationServiceQueue(): Queue {
        return Queue("book-changed-notifications", true)
    }

    @Bean
    fun exchange(): DirectExchange {
        return DirectExchange("book-changed-exchange", true, false)
    }

    @Bean
    fun binding(notificationServiceQueue: Queue?, exchange: DirectExchange?): Binding {
        return BindingBuilder.bind(notificationServiceQueue).to(exchange).with("book-changed-notifications")
    }
}