from django.urls import path
from . import views

urlpatterns = [
    path("types/", views.ProductTypesView.as_view(), name="product-types"),
    path("occasions/", views.OccasionsView.as_view(), name="occasions"),
    path("<int:pk>/", views.ProductDetailView.as_view(), name="product-detail"),
    path("featured/", views.FeaturedProductsView.as_view(), name="featured-products"),
    path("fresh/", views.FreshProductsView.as_view(), name="fresh-products"),
    path("", views.ProductListView.as_view(), name="products"),
]
