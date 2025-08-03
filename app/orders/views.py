from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import status
from rest_framework.permissions import IsAuthenticated
from orders.serializers import OrderSerializer
from orders.models import Order
from carts.models import CartItem


class OrderView(APIView):
    permission_classes = [IsAuthenticated]

    # Create a new order for a user that takes all items from the cart
    def post(self, request):
        cart_items = CartItem.objects.filter(user=request.user, order=None)
        if not cart_items.exists():
            return Response(
                {"error": "No items in cart"}, status=status.HTTP_400_BAD_REQUEST
            )

        total_price = sum(item.product.price * item.quantity for item in cart_items)
        order = Order.objects.create(
            user=request.user,
            status=Order.StatusChoices.PROCESSING,
            total_price=total_price,
        )

        for item in cart_items:
            item.order = order
            item.save()

        return Response(
            {"message": "Order created successfully"}, status=status.HTTP_201_CREATED
        )

    # Update the status of an order
    def patch(self, request, order_id):
        try:
            order = Order.objects.get(id=order_id, user=request.user)
        except Order.DoesNotExist:
            return Response(
                {"error": "Order not found"}, status=status.HTTP_404_NOT_FOUND
            )

        status_ = request.data.get("status")
        if status_ not in dict(Order.StatusChoices.choices):
            return Response(
                {"error": "Invalid status"}, status=status.HTTP_400_BAD_REQUEST
            )

        order.status = status_
        order.save()
        serializer = OrderSerializer(order)
        return Response(serializer.data)

    # Get all orders for a user
    def get(self, request):
        orders = Order.objects.filter(user=request.user).order_by("-created_at")
        serializer = OrderSerializer(orders, many=True)
        return Response(serializer.data)
