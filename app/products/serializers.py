from rest_framework import serializers
from .models import Product, ProductType, Occasion


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

    class Meta:
        model = Product
        fields = "__all__"
