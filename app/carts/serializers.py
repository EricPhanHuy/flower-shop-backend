from rest_framework import serializers

from products.serializers import ProductSerializer
from .models import CartItem


class CartItemSerializer(serializers.ModelSerializer):
    product = ProductSerializer(read_only=True)
    product_id = serializers.IntegerField(write_only=True)
    order_id = serializers.IntegerField(write_only=True, required=False)

    class Meta:
        model = CartItem
        fields = ["id", "product", "product_id", "quantity", "order_id"]
        read_only_fields = ["id", "product"]
