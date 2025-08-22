from rest_framework import serializers
from .models import Product, ProductType, Occasion
from pricing_rules.serializers import TimePricingRuleSerializer


class ProductTypeSerializer(serializers.ModelSerializer):
    class Meta:
        model = ProductType
        fields = ["id", "name"]


class OccasionSerializer(serializers.ModelSerializer):
    class Meta:
        model = Occasion
        fields = ["id", "name"]


class ProductSerializer(serializers.ModelSerializer):
    type = ProductTypeSerializer(read_only=True)
    occasion = OccasionSerializer(read_only=True)
    pricing_rule = TimePricingRuleSerializer()

    class Meta:
        model = Product
        fields = "__all__"
        read_only_fields = ["id"]
