import os
import random
import json
import time
from locust import HttpUser, task, between, events
from locust.exception import RescheduleTask

# Configuration
BASE_URL = os.getenv("LOCUST_BASE_URL", "http://localhost:8000")
THINK_TIME_MIN = 1
THINK_TIME_MAX = 5
TEST_TIMEOUT = 30

# Authentication credentials
REGULAR_USER = {"username": "ngtuankhanh", "password": "Ecommerce123"}

ADMIN_USER = {"username": "admin", "password": "florashop123"}

# Product data for realistic testing
PRODUCT_TYPES = ["Bouquets", "Flower Arrangements", "Plants", "Gift Baskets"]
OCCASIONS = ["Valentine's Day", "Birthday", "Anniversary", "Mother's Day", "Wedding"]
CONDITIONS = ["fresh", "aged", "wilted", "chilled"]
SEARCH_TERMS = ["rose", "tulip", "lily", "orchid", "sunflower", "daisy", "carnation"]


# Error handling decorator
def handle_errors(func):
    def wrapper(self, *args, **kwargs):
        try:
            return func(self, *args, **kwargs)
        except RescheduleTask:
            raise
        except Exception as e:
            self.environment.events.request.fire(
                request_type="CUSTOM",
                name=func.__name__,
                response_time=0,
                response_length=0,
                exception=e,
            )
            raise RescheduleTask()

    return wrapper


class BaseFlowerShopUser(HttpUser):
    """Base user class with common functionality"""

    abstract = True
    wait_time = between(THINK_TIME_MIN, THINK_TIME_MAX)

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.auth_token = None
        self.product_ids = list(range(1, 6))  # Products 1-20
        self.cart_item_ids = []

    def on_start(self):
        """Called when a user starts"""
        if hasattr(self, "user_type") and self.user_type == "regular":
            self.register_regular_user()
            self.authenticate_regular_user()
        elif hasattr(self, "user_type") and self.user_type == "admin":
            self.authenticate_admin_user()

    def register_regular_user(self):
        with self.client.post(
            "/api/auth/register/",
            json=REGULAR_USER,
            catch_response=True,
            name="Auth: Regular User Registration",
        ) as response:
            if response.status_code == 201:
                response.success()
            elif (
                response.status_code == 400
                and "username" in response.json()
                and "A user with that username already exists"
                in response.json()["username"][0]
            ):
                response.success()
            else:
                response.failure(f"Registration failed: {response.status_code}")

    def authenticate_regular_user(self):
        """Authenticate as regular user"""
        with self.client.post(
            "/api/auth/get-auth-token/",
            json=REGULAR_USER,
            catch_response=True,
            name="Auth: Regular User Login",
        ) as response:
            if response.status_code == 200:
                data = response.json()
                self.auth_token = data.get("token")
                response.success()
            else:
                response.failure(f"Authentication failed: {response.status_code}")

    def authenticate_admin_user(self):
        """Authenticate as admin user"""
        with self.client.post(
            "/api/auth/admin/",
            json=ADMIN_USER,
            catch_response=True,
            name="Auth: Admin Login",
        ) as response:
            if response.status_code == 200:
                data = response.json()
                self.auth_token = data.get("token")
                response.success()
            else:
                response.failure(f"Admin authentication failed: {response.status_code}")

    def get_auth_headers(self):
        """Get authentication headers if token exists"""
        if self.auth_token:
            return {"Authorization": f"Token {self.auth_token}"}
        return {}

    def make_request(self, method, endpoint, **kwargs):
        """Make HTTP request with error handling"""
        headers = kwargs.pop("headers", {})
        auth_headers = self.get_auth_headers()
        headers.update(auth_headers)

        start_time = time.time()
        try:
            response = getattr(self.client, method.lower())(
                endpoint, headers=headers, timeout=TEST_TIMEOUT, **kwargs
            )
            response_time = (time.time() - start_time) * 1000

            # Log request success/failure
            if response.status_code >= 400:
                events.request.fire(
                    request_type=method.upper(),
                    name=endpoint,
                    response_time=response_time,
                    response_length=len(response.content),
                    exception=f"HTTP {response.status_code}",
                )
            else:
                events.request.fire(
                    request_type=method.upper(),
                    name=endpoint,
                    response_time=response_time,
                    response_length=len(response.content),
                )

            return response
        except Exception as e:
            response_time = (time.time() - start_time) * 1000
            events.request.fire(
                request_type=method.upper(),
                name=endpoint,
                response_time=response_time,
                response_length=0,
                exception=str(e),
            )
            raise RescheduleTask()


class AnonymousUser(BaseFlowerShopUser):
    """User who only browses without authentication"""

    user_type = "anonymous"

    @task(35)
    @handle_errors
    def browse_products(self):
        """Browse products with pagination"""
        page = 1
        self.make_request(
            "GET", f"/api/products/products/?page={page}", name="Browse: Products Page"
        )

    @task(15)
    @handle_errors
    def get_product_types(self):
        """Get product types"""
        self.make_request("GET", "/api/products/types/", name="Browse: Product Types")

    @task(10)
    @handle_errors
    def get_occasions(self):
        """Get occasions"""
        self.make_request("GET", "/api/products/occasions/", name="Browse: Occasions")

    @task(10)
    @handle_errors
    def get_featured_products(self):
        """Get featured products"""
        self.make_request(
            "GET", "/api/products/products/featured/", name="Browse: Featured Products"
        )

    @task(10)
    @handle_errors
    def get_fresh_products(self):
        """Get fresh products"""
        self.make_request(
            "GET", "/api/products/products/fresh/", name="Browse: Fresh Products"
        )

    @task(10)
    @handle_errors
    def filter_products_by_type(self):
        """Filter products by type"""
        product_type = random.choice(PRODUCT_TYPES)
        self.make_request(
            "GET",
            f"/api/products/products/?type__name={product_type}",
            name="Browse: Filter by Type",
        )

    @task(10)
    @handle_errors
    def filter_products_by_occasion(self):
        """Filter products by occasion"""
        occasion = random.choice(OCCASIONS)
        self.make_request(
            "GET",
            f"/api/products/products/?occasion__name={occasion}",
            name="Browse: Filter by Occasion",
        )

    @task(10)
    @handle_errors
    def search_products(self):
        """Search products by name"""
        search_term = random.choice(SEARCH_TERMS)
        self.make_request(
            "GET",
            f"/api/products/products/?name__icontains={search_term}",
            name="Browse: Search Products",
        )

    @task(5)
    @handle_errors
    def filter_by_price_range(self):
        """Filter products by price range"""
        max_price = random.randint(50, 200)
        min_price = random.randint(10, max_price - 10)
        self.make_request(
            "GET",
            f"/api/products/products/?price__lt={max_price}&price__gt={min_price}",
            name="Browse: Filter by Price",
        )

    @task(5)
    @handle_errors
    def filter_by_condition(self):
        """Filter products by condition"""
        condition = random.choice(CONDITIONS)
        self.make_request(
            "GET",
            f"/api/products/products/?condition={condition}",
            name="Browse: Filter by Condition",
        )

    @task(5)
    @handle_errors
    def get_specific_product(self):
        """Get specific product"""
        product_id = random.choice(self.product_ids)
        self.make_request(
            "GET",
            f"/api/products/products/{product_id}/",
            name="Browse: Specific Product",
        )
