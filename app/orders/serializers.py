from rest_framework import serializers

from carts.serializers import CartItemSerializer
from carts.models import CartItem
from .models import Order


class OrderSerializer(serializers.ModelSerializer):
    cart_items = serializers.SerializerMethodField()

    class Meta:
        model = Order
        fields = ["id", "user", "total_price", "status", "created_at", "cart_items"]
        read_only_fields = ["id", "created_at", "user", "total_price"]

    def get_cart_items(self, obj):
        cart_items = CartItem.objects.filter(order=obj)
        return CartItemSerializer(cart_items, many=True).data
