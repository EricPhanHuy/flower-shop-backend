from django.core.management.base import BaseCommand
from django.db import transaction
from products.models import ProductType, Occasion, Product
from decimal import Decimal
import random


class Command(BaseCommand):
    help = "Seed the database with sample product data"

    def handle(self, *args, **options):
        with transaction.atomic():
            self.stdout.write("Creating product types...")
            product_types = self.create_product_types()

            self.stdout.write("Creating occasions...")
            occasions = self.create_occasions()

            self.stdout.write("Creating products...")
            self.create_products(product_types, occasions)

            self.stdout.write(self.style.SUCCESS("Successfully seeded database!"))

    def create_product_types(self):
        product_types_data = [
            "Bouquets",
            "Single Flowers",
            "Arrangements",
            "Potted Plants",
            "Gift Sets",
            "Seasonal Collections",
            "Premium Flowers",
            "Budget Options",
        ]

        product_types = []
        for name in product_types_data:
            product_type, created = ProductType.objects.get_or_create(name=name)
            product_types.append(product_type)
            if created:
                self.stdout.write(f"Created product type: {name}")

        return product_types

    def create_occasions(self):
        occasions_data = [
            "Wedding",
            "Birthday",
            "Anniversary",
            "Valentine's Day",
            "Mother's Day",
            "Father's Day",
            "Graduation",
            "Sympathy",
            "Congratulations",
            "Get Well",
            "Thank You",
            "Just Because",
            "Corporate",
            "Holiday",
        ]

        occasions = []
        for name in occasions_data:
            occasion, created = Occasion.objects.get_or_create(name=name)
            occasions.append(occasion)
            if created:
                self.stdout.write(f"Created occasion: {name}")

        return occasions

    def create_products(self, product_types, occasions):
        products_data = [
            {
                "name": "Classic Red Rose Bouquet",
                "description": "A stunning arrangement of 12 fresh red roses, perfect for romantic occasions.",
                "type": "Bouquets",
                "occasion": "Valentine's Day",
                "condition": "fresh",
                "average_rating": 4.8,
                "total_reviews": 156,
                "price": 89.99,
                "stock": 25,
                "image_url": "https://images.pexels.com/photos/56866/garden-rose-red-pink-56866.jpeg",
            },
            {
                "name": "Wedding White Lily Collection",
                "description": "Elegant white lilies arranged for the perfect wedding ceremony.",
                "type": "Arrangements",
                "occasion": "Wedding",
                "condition": "fresh",
                "average_rating": 4.9,
                "total_reviews": 89,
                "price": 149.99,
                "stock": 15,
                "image_url": "https://images.pexels.com/photos/85773/pexels-photo-85773.jpeg",
            },
            {
                "name": "Birthday Sunflower Bundle",
                "description": "Bright and cheerful sunflowers to brighten any birthday celebration.",
                "type": "Bouquets",
                "occasion": "Birthday",
                "condition": "fresh",
                "average_rating": 4.6,
                "total_reviews": 203,
                "price": 59.99,
                "stock": 40,
                "image_url": "https://images.pexels.com/photos/46216/sunflower-flowers-bright-yellow-46216.jpeg",
            },
            {
                "name": "Anniversary Pink Roses",
                "description": "Delicate pink roses symbolizing love and appreciation for your anniversary.",
                "type": "Premium Flowers",
                "occasion": "Anniversary",
                "condition": "fresh",
                "average_rating": 4.7,
                "total_reviews": 67,
                "price": 119.99,
                "stock": 20,
                "image_url": "https://images.pexels.com/photos/56866/garden-rose-red-pink-56866.jpeg",
            },
            {
                "name": "Mother's Day Tulip Mix",
                "description": "Colorful tulips in various shades, perfect for celebrating mothers.",
                "type": "Seasonal Collections",
                "occasion": "Mother's Day",
                "condition": "fresh",
                "average_rating": 4.5,
                "total_reviews": 134,
                "price": 79.99,
                "stock": 30,
                "image_url": "https://images.pexels.com/photos/931177/pexels-photo-931177.jpeg",
            },
            {
                "name": "Sympathy White Orchids",
                "description": "Peaceful white orchids for expressing condolences and sympathy.",
                "type": "Arrangements",
                "occasion": "Sympathy",
                "condition": "fresh",
                "average_rating": 4.4,
                "total_reviews": 45,
                "price": 99.99,
                "stock": 12,
                "image_url": "https://images.pexels.com/photos/85773/pexels-photo-85773.jpeg",
            },
            {
                "name": "Graduation Mixed Bouquet",
                "description": "Vibrant mixed flowers to celebrate academic achievements.",
                "type": "Bouquets",
                "occasion": "Graduation",
                "condition": "fresh",
                "average_rating": 4.6,
                "total_reviews": 78,
                "price": 69.99,
                "stock": 35,
                "image_url": "https://images.pexels.com/photos/46216/sunflower-flowers-bright-yellow-46216.jpeg",
            },
            {
                "name": "Get Well Daisies",
                "description": "Cheerful daisies to brighten someone's day during recovery.",
                "type": "Budget Options",
                "occasion": "Get Well",
                "condition": "fresh",
                "average_rating": 4.3,
                "total_reviews": 92,
                "price": 39.99,
                "stock": 50,
                "image_url": "https://images.pexels.com/photos/67857/daisy-flower-spring-marguerite-67857.jpeg",
            },
            {
                "name": "Thank You Carnations",
                "description": "Simple yet elegant carnations to express gratitude.",
                "type": "Single Flowers",
                "occasion": "Thank You",
                "condition": "fresh",
                "average_rating": 4.2,
                "total_reviews": 156,
                "price": 29.99,
                "stock": 60,
                "image_url": "https://images.pexels.com/photos/67857/daisy-flower-spring-marguerite-67857.jpeg",
            },
            {
                "name": "Corporate Plant Collection",
                "description": "Low-maintenance plants perfect for office environments.",
                "type": "Potted Plants",
                "occasion": "Corporate",
                "condition": "fresh",
                "average_rating": 4.4,
                "total_reviews": 34,
                "price": 89.99,
                "stock": 18,
                "image_url": "https://images.pexels.com/photos/757889/pexels-photo-757889.jpeg",
            },
            {
                "name": "Valentine's Day Special",
                "description": "A romantic collection of red and pink roses for the perfect Valentine's Day.",
                "type": "Premium Flowers",
                "occasion": "Valentine's Day",
                "condition": "fresh",
                "average_rating": 4.9,
                "total_reviews": 234,
                "price": 129.99,
                "stock": 30,
                "image_url": "https://images.pexels.com/photos/56866/garden-rose-red-pink-56866.jpeg",
            },
            {
                "name": "Spring Daisy Collection",
                "description": "Fresh spring daisies bringing the beauty of spring to your home.",
                "type": "Seasonal Collections",
                "occasion": "Just Because",
                "condition": "fresh",
                "average_rating": 4.4,
                "total_reviews": 87,
                "price": 49.99,
                "stock": 45,
                "image_url": "https://images.pexels.com/photos/67857/daisy-flower-spring-marguerite-67857.jpeg",
            },
            {
                "name": "Wedding Rose Bouquet",
                "description": "Elegant white and pink roses perfect for your special day.",
                "type": "Bouquets",
                "occasion": "Wedding",
                "condition": "fresh",
                "average_rating": 4.8,
                "total_reviews": 112,
                "price": 179.99,
                "stock": 10,
                "image_url": "https://images.pexels.com/photos/56866/garden-rose-red-pink-56866.jpeg",
            },
            {
                "name": "Congratulations Sunflowers",
                "description": "Bright sunflowers to celebrate any achievement or success.",
                "type": "Bouquets",
                "occasion": "Congratulations",
                "condition": "fresh",
                "average_rating": 4.6,
                "total_reviews": 156,
                "price": 64.99,
                "stock": 35,
                "image_url": "https://images.pexels.com/photos/46216/sunflower-flowers-bright-yellow-46216.jpeg",
            },
            {
                "name": "Father's Day Arrangement",
                "description": "A sophisticated arrangement perfect for celebrating fathers.",
                "type": "Arrangements",
                "occasion": "Father's Day",
                "condition": "fresh",
                "average_rating": 4.5,
                "total_reviews": 78,
                "price": 89.99,
                "stock": 25,
                "image_url": "https://images.pexels.com/photos/85773/pexels-photo-85773.jpeg",
            },
            {
                "name": "Holiday Celebration Mix",
                "description": "Festive flowers perfect for holiday celebrations and gatherings.",
                "type": "Seasonal Collections",
                "occasion": "Holiday",
                "condition": "fresh",
                "average_rating": 4.3,
                "total_reviews": 92,
                "price": 74.99,
                "stock": 40,
                "image_url": "https://images.pexels.com/photos/931177/pexels-photo-931177.jpeg",
            },
            {
                "name": "Single Red Rose",
                "description": "A single perfect red rose for a simple yet meaningful gesture.",
                "type": "Single Flowers",
                "occasion": "Just Because",
                "condition": "fresh",
                "average_rating": 4.2,
                "total_reviews": 203,
                "price": 19.99,
                "stock": 100,
                "image_url": "https://images.pexels.com/photos/56866/garden-rose-red-pink-56866.jpeg",
            },
            {
                "name": "Office Desk Plant",
                "description": "A beautiful potted plant to brighten up any office space.",
                "type": "Potted Plants",
                "occasion": "Corporate",
                "condition": "fresh",
                "average_rating": 4.4,
                "total_reviews": 67,
                "price": 59.99,
                "stock": 30,
                "image_url": "https://images.pexels.com/photos/757889/pexels-photo-757889.jpeg",
            },
            {
                "name": "Birthday Rose Mix",
                "description": "A colorful mix of roses perfect for birthday celebrations.",
                "type": "Bouquets",
                "occasion": "Birthday",
                "condition": "fresh",
                "average_rating": 4.7,
                "total_reviews": 145,
                "price": 79.99,
                "stock": 28,
                "image_url": "https://images.pexels.com/photos/56866/garden-rose-red-pink-56866.jpeg",
            },
            {
                "name": "Anniversary Lily Collection",
                "description": "Elegant lilies to celebrate your special anniversary.",
                "type": "Premium Flowers",
                "occasion": "Anniversary",
                "condition": "fresh",
                "average_rating": 4.8,
                "total_reviews": 89,
                "price": 139.99,
                "stock": 15,
                "image_url": "https://images.pexels.com/photos/85773/pexels-photo-85773.jpeg",
            },
            {
                "name": "Summer Sunflower Bundle",
                "description": "Bright summer sunflowers to bring warmth to any room.",
                "type": "Seasonal Collections",
                "occasion": "Just Because",
                "condition": "fresh",
                "average_rating": 4.5,
                "total_reviews": 123,
                "price": 54.99,
                "stock": 50,
                "image_url": "https://images.pexels.com/photos/46216/sunflower-flowers-bright-yellow-46216.jpeg",
            },
            {
                "name": "Sympathy Daisy Arrangement",
                "description": "Peaceful daisies arranged with care for expressing sympathy.",
                "type": "Arrangements",
                "occasion": "Sympathy",
                "condition": "fresh",
                "average_rating": 4.3,
                "total_reviews": 56,
                "price": 69.99,
                "stock": 20,
                "image_url": "https://images.pexels.com/photos/67857/daisy-flower-spring-marguerite-67857.jpeg",
            },
            {
                "name": "Get Well Rose Mix",
                "description": "A gentle mix of roses to brighten someone's recovery.",
                "type": "Bouquets",
                "occasion": "Get Well",
                "condition": "fresh",
                "average_rating": 4.4,
                "total_reviews": 78,
                "price": 59.99,
                "stock": 35,
                "image_url": "https://images.pexels.com/photos/56866/garden-rose-red-pink-56866.jpeg",
            },
            {
                "name": "Thank You Lily Bouquet",
                "description": "Elegant lilies to express your gratitude and appreciation.",
                "type": "Bouquets",
                "occasion": "Thank You",
                "condition": "fresh",
                "average_rating": 4.6,
                "total_reviews": 134,
                "price": 84.99,
                "stock": 25,
                "image_url": "https://images.pexels.com/photos/85773/pexels-photo-85773.jpeg",
            },
            {
                "name": "Premium Wedding Collection",
                "description": "Our most luxurious wedding flower collection for your special day.",
                "type": "Premium Flowers",
                "occasion": "Wedding",
                "condition": "fresh",
                "average_rating": 4.9,
                "total_reviews": 67,
                "price": 249.99,
                "stock": 8,
                "image_url": "https://images.pexels.com/photos/931177/pexels-photo-931177.jpeg",
            },
            {
                "name": "Budget Sunflower Bundle",
                "description": "Affordable sunflowers that bring joy without breaking the bank.",
                "type": "Budget Options",
                "occasion": "Just Because",
                "condition": "fresh",
                "average_rating": 4.2,
                "total_reviews": 189,
                "price": 34.99,
                "stock": 60,
                "image_url": "https://images.pexels.com/photos/46216/sunflower-flowers-bright-yellow-46216.jpeg",
            },
        ]

        for product_data in products_data:
            product_type = next(
                pt for pt in product_types if pt.name == product_data["type"]
            )
            occasion = next(
                occ for occ in occasions if occ.name == product_data["occasion"]
            )

            product, created = Product.objects.get_or_create(
                name=product_data["name"],
                defaults={
                    "description": product_data["description"],
                    "type": product_type,
                    "occasion": occasion,
                    "condition": product_data["condition"],
                    "average_rating": product_data["average_rating"],
                    "total_reviews": product_data["total_reviews"],
                    "price": product_data["price"],
                    "stock": product_data["stock"],
                    "image_url": product_data["image_url"],
                },
            )

            if created:
                self.stdout.write(f"Created product: {product.name}")
