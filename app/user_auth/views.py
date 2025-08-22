# users/views.py
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import status
from rest_framework.authtoken.models import Token
from django.contrib.auth.models import User
from .serializers import RegisterSerializer


class RegisterView(APIView):
    def post(self, request):
        serializer = RegisterSerializer(data=request.data)
        if serializer.is_valid():
            user = serializer.save()
            token, created = Token.objects.get_or_create(user=user)
            return Response({"token": token.key}, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


class AdminView(APIView):
    def post(self, request):
        # Extract username and password from request data
        username = request.data.get("username")
        password = request.data.get("password")

        # Validate that both username and password are provided
        if not username or not password:
            return Response(
                {"error": "Username and password are required"},
                status=status.HTTP_400_BAD_REQUEST,
            )

        # Check against hardcoded admin credentials
        if username == "admin" and password == "florashop123":
            try:
                # Get or create admin user
                admin_user = User.objects.get(username="admin")

                # Generate or get token for admin user
                token, created = Token.objects.get_or_create(user=admin_user)

                return Response({"token": token.key}, status=status.HTTP_200_OK)
            except User.DoesNotExist:
                return Response(
                    {"error": "Admin user not found"},
                    status=status.HTTP_500_INTERNAL_SERVER_ERROR,
                )
        else:
            return Response(
                {"error": "Invalid admin credentials"},
                status=status.HTTP_401_UNAUTHORIZED,
            )
