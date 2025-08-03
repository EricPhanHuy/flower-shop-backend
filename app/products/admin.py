from django.contrib import admin
from .models import Product, ProductType, Occasion, Review, ProductFilter

# Register your models here.

admin.site.register(Product)
admin.site.register(ProductType)
admin.site.register(Occasion)
admin.site.register(Review)
