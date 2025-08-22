from datetime import datetime
from django.db import models
from django_filters import rest_framework as filters
from django.utils import timezone


class ProductType(models.Model):
    name = models.CharField(max_length=255)


class Occasion(models.Model):
    name = models.CharField(max_length=255)


class Product(models.Model):
    class ConditionChoices(models.TextChoices):
        FRESH = "fresh", "Fresh"
        AGED = "aged", "Aged"
        WILTED = "wilted", "Wilted"
        CHILLED = "chilled", "Chilled"

    name = models.CharField(max_length=255)
    description = models.TextField(blank=True, null=True)
    type = models.ForeignKey(
        ProductType, on_delete=models.SET_NULL, null=True, blank=True
    )
    occasion = models.ForeignKey(
        Occasion, on_delete=models.SET_NULL, null=True, blank=True
    )
    condition = models.CharField(
        max_length=255, choices=ConditionChoices.choices, default=ConditionChoices.FRESH
    )
    average_rating = models.DecimalField(max_digits=2, decimal_places=1)
    total_reviews = models.PositiveIntegerField(default=0)
    price = models.DecimalField(max_digits=10, decimal_places=2)
    condition_discount = models.DecimalField(
        max_digits=10, decimal_places=2, default=0.00
    )
    pricing_rule = models.ForeignKey(
        "pricing_rules.TimePricingRule",
        on_delete=models.SET_NULL,
        null=True,
        blank=True,
    )
    stock = models.PositiveIntegerField(default=0)
    image_url = models.URLField(blank=True, null=True)

    def save(self, *args, **kwargs):
        # Apply default discounts based on condition
        condition_discounts = {
            self.ConditionChoices.FRESH: 0.00,
            self.ConditionChoices.AGED: 0.25,
            self.ConditionChoices.WILTED: 0.50,
            self.ConditionChoices.CHILLED: 0.00,
        }

        if self.condition_discount == 0:  # Only set if discount not manually given
            self.condition_discount = condition_discounts.get(self.condition, 0.00)

        super().save(*args, **kwargs)

    def __str__(self):
        return self.name

    class Meta:
        verbose_name = "Product"
        verbose_name_plural = "Products"
        ordering = ["name"]


class ProductFilter(filters.FilterSet):
    class Meta:
        model = Product
        fields = {
            "type__name": ["exact", "in"],
            "occasion__name": ["exact", "in"],
            "price": ["lt", "gt"],
            "condition": ["exact", "in"],
            "name": ["icontains"],
        }


class Review(models.Model):
    product = models.ForeignKey(Product, on_delete=models.CASCADE)
    rating = models.DecimalField(max_digits=1, decimal_places=1)
    comment = models.TextField(blank=True, null=True)
    created_at = models.DateTimeField(auto_now_add=True)
