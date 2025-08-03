from django.urls import path
from . import views

urlpatterns = [
    path("", views.OrderView.as_view(), name="orders"),
    path("<int:order_id>/", views.OrderView.as_view(), name="order"),
]
