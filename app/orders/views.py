from rest_framework import viewsets, permissions, status
from rest_framework.decorators import action
from rest_framework.response import Response
from orders.serializers import OrderSerializer
from orders.models import Order
from carts.models import CartItem
from django.utils import timezone


class OrderViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows orders to be viewed or edited.
    """

    serializer_class = OrderSerializer
    permission_classes = [permissions.IsAuthenticated]

    def get_queryset(self):
        """
        Only allow users to see their own orders
        """
        return Order.objects.filter(user=self.request.user).order_by("-created_at")

    def perform_create(self, serializer):
        """
        Create a new order for a user that takes all items from the cart
        """
        cart_items = CartItem.objects.filter(user=self.request.user, order=None)
        if not cart_items.exists():
            raise Exception("No items in cart")

        # For fallback if needed total_price = sum(item.product.price * item.quantity for item in cart_items)
        # Need help write the below
        total_price = sum(
            (
                item.product.price
                * (
                    1 - item.product.condition_discount / 100
                )  # Apply condition discount as percentage
                * (
                    1 - (item.product.pricing_rule.time_discount / 100)
                    if item.product.pricing_rule is not None
                    and item.product.pricing_rule.start_time <= timezone.now()
                    and item.product.pricing_rule.end_time >= timezone.now()
                    else 1
                )  # Apply time discount as percentage only if rule is active
            )
            * item.quantity
            for item in cart_items
        )
        order = serializer.save(
            user=self.request.user,
            status=Order.StatusChoices.PROCESSING,
            total_price=total_price,
        )

        for item in cart_items:
            item.order = order
            item.save()

    @action(detail=True, methods=["patch"])
    def update_status(self, request, pk=None):
        """
        Custom action to update order status
        """
        order = self.get_object()
        status_ = request.data.get("status")

        if status_ not in dict(Order.StatusChoices.choices):
            return Response(
                {"error": "Invalid status"}, status=status.HTTP_400_BAD_REQUEST
            )

        order.status = status_
        order.save()
        serializer = self.get_serializer(order)
        return Response(serializer.data)

    def create(self, request, *args, **kwargs):
        """
        Override create to handle cart-based order creation
        """
        try:
            return super().create(request, *args, **kwargs)
        except Exception as e:
            return Response({"error": str(e)}, status=status.HTTP_400_BAD_REQUEST)
