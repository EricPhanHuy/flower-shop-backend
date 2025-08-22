from django.db import models
from django.utils import timezone


class TimePricingRule(models.Model):
    start_time = models.DateTimeField(default=timezone.now)
    end_time = models.DateTimeField(default="2026-01-01T00:00:00Z")
    time_discount = models.DecimalField(max_digits=10, decimal_places=2)

    class Meta:
        verbose_name = "Time Pricing Rule"
        verbose_name_plural = "Time Pricing Rules"
        ordering = ["start_time"]

    def __str__(self):
        return f"Rule {self.id} - {self.time_discount}% discount"
