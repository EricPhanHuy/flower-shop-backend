from rest_framework import viewsets, permissions, filters
from rest_framework.decorators import action
from rest_framework.pagination import PageNumberPagination
from rest_framework.response import Response
from .models import Occasion, Product, ProductFilter, ProductType
from .serializers import ProductTypeSerializer, OccasionSerializer, ProductSerializer
from django_filters.rest_framework import DjangoFilterBackend


class ProductTypeViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows product types to be viewed or edited.
    """

    queryset = ProductType.objects.all()
    serializer_class = ProductTypeSerializer
    permission_classes = [permissions.IsAuthenticatedOrReadOnly]
    filter_backends = [filters.SearchFilter]
    search_fields = ["name"]


class OccasionViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows occasions to be viewed or edited.
    """

    queryset = Occasion.objects.all()
    serializer_class = OccasionSerializer
    permission_classes = [permissions.IsAuthenticatedOrReadOnly]
    filter_backends = [filters.SearchFilter]
    search_fields = ["name"]


class ProductPagination(PageNumberPagination):
    page_size = 10
    page_size_query_param = "page_size"
    max_page_size = 100


class ProductViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows products to be viewed or edited.
    """

    queryset = Product.objects.all()
    serializer_class = ProductSerializer
    permission_classes = [permissions.IsAuthenticatedOrReadOnly]
    filter_backends = (
        DjangoFilterBackend,
        filters.OrderingFilter,
        filters.SearchFilter,
    )
    filterset_class = ProductFilter
    pagination_class = ProductPagination
    ordering_fields = ["price", "name", "average_rating", "created_at"]
    search_fields = ["name", "description"]

    @action(detail=False, methods=["get"])
    def featured(self, request):
        """
        Custom action to get featured products
        """
        queryset = Product.objects.order_by("-total_reviews")[:3]
        serializer = self.get_serializer(queryset, many=True)
        return Response(serializer.data)

    @action(detail=False, methods=["get"])
    def fresh(self, request):
        """
        Custom action to get fresh products
        """
        queryset = Product.objects.filter(condition=Product.ConditionChoices.FRESH)[:4]
        serializer = self.get_serializer(queryset, many=True)
        return Response(serializer.data)
