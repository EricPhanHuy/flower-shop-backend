from rest_framework.generics import ListAPIView
from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework.permissions import IsAuthenticated
from .models import Occasion, Product, ProductFilter, ProductType
from .serializers import ProductTypeSerializer, OccasionSerializer, ProductSerializer
from django_filters import rest_framework as filters


class ProductTypesView(APIView):
    """API endpoint for listing all product types"""

    def get(self, request):
        product_types = ProductType.objects.all()
        serializer = ProductTypeSerializer(product_types, many=True)
        return Response(serializer.data)


class OccasionsView(APIView):
    def get(self, request):
        occasions = Occasion.objects.all()
        serializer = OccasionSerializer(occasions, many=True)
        return Response(serializer.data)


class ProductListView(ListAPIView):
    """API endpoint for listing products with filtering options"""

    queryset = Product.objects.all()
    serializer_class = ProductSerializer
    filter_backends = (filters.DjangoFilterBackend,)
    filterset_class = ProductFilter


class FeaturedProductsView(ListAPIView):
    """API endpoint for listing featured products"""

    queryset = Product.objects.order_by("-total_reviews")[:3]
    serializer_class = ProductSerializer


class FreshProductsView(ListAPIView):
    """API endpoint for listing fresh products"""

    queryset = Product.objects.filter(condition=Product.ConditionChoices.FRESH)[:4]
    serializer_class = ProductSerializer


class ProductDetailView(APIView):
    def get(self, request, pk):
        product = Product.objects.get(pk=pk)
        serializer = ProductSerializer(product)
        return Response(serializer.data)
