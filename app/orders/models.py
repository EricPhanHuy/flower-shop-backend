from django.db import models

from django.contrib.auth.models import User


class Order(models.Model):
    class StatusChoices(models.TextChoices):
        PROCESSING = "processing", "Processing"
        SHIPPING = "shipping", "Shipping"
        COMPLETED = "completed", "Completed"
        CANCELLED = "cancelled", "Cancelled"

    user = models.ForeignKey(User, on_delete=models.CASCADE)

    status = models.CharField(
        max_length=255, choices=StatusChoices.choices, default=StatusChoices.PROCESSING
    )
    created_at = models.DateTimeField(auto_now_add=True)
    total_price = models.DecimalField(max_digits=10, decimal_places=2)

    def __str__(self):
        return f"Order {self.id} - {self.user.username}"
