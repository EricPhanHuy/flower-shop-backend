# FlowerShop API Load Testing

This Locust load testing setup is designed to test the FlowerShop e-commerce API with realistic user behavior patterns.

## Features

- **Browse-heavy scenario**: 70% browsing, 20% cart operations, 10% orders
- **Multiple user types**: Anonymous, authenticated regular users, and admin users
- **Realistic authentication**: Automatic token management using `on_start`
- **Comprehensive API coverage**: All endpoints from `request.http` are implemented
- **Error handling**: Graceful handling of HTTP errors and timeouts
- **Dynamic data**: Random product IDs, search terms, and filters
- **Configurable**: Environment variables and configuration file support

## User Classes

### AnonymousUser (60% of users)

- Browses products without authentication
- Accesses public endpoints
- Performs product searches and filters

### RegularUser (20% of users)

- Authenticates as `ngtuankhanh/Ecommerce123`
- Full browsing capabilities
- Cart operations (add, update, delete items)
- Order management (create, update status)

### AdminUser (20% of users)

- Authenticates as `admin/florashop123`
- Pricing rule management
- Product data management
- Error testing scenarios

## Setup

1. **Install dependencies**:

```bash
pip install locust
```

2. **Configure environment variables (optional)**:

```bash
export LOCUST_BASE_URL=http://localhost:8000
```

3. **Start the Django server**:

```bash
python manage.py runserver
```

4. **Run the load test**:

### Web UI (recommended)

```bash
locust -f locustfile.py --config locust.conf
```

Then open http://localhost:8089 in your browser

### Command line only

```bash
locust -f locustfile.py --headless -u 10 -r 2 -t 10m
```

## Configuration

### Environment Variables

- `LOCUST_BASE_URL`: Base URL of the API (default: http://localhost:8000)

### Configuration File

Edit `locust.conf` to customize:

- Number of users (`num_users`)
- Spawn rate (`spawn_rate`)
- Test duration (`run_time`)
- User class weights
- Output directories

## Authentication

The system automatically handles authentication:

- **Regular users**: `ngtuankhanh/Ecommerce123`
- **Admin users**: `admin/florashop123`
- Tokens are automatically retrieved and stored for subsequent requests

## Task Distribution

### Browsing Tasks (70%)

- Product browsing with pagination
- Product type and occasion filtering
- Product search by name
- Price range filtering
- Featured and fresh products
- Specific product details

### Cart Operations (20%)

- Get cart items
- Add items to cart
- Update cart item quantities
- Delete cart items
- Partial cart updates

### Order Processing (10%)

- Get user orders
- Create new orders
- Update order status

### Admin Tasks (20% of admin users)

- Pricing rule CRUD operations
- Product data management
- Error scenario testing

## Monitoring

The load test provides:

- Real-time statistics in the web UI
- CSV reports for analysis
- Custom event logging
- Request success/failure tracking
- Response time monitoring

## Customization

### Adding New Endpoints

1. Add the task method to the appropriate user class
2. Use the `make_request()` helper method for consistent error handling
3. Apply the `@handle_errors` decorator for automatic error logging

### Modifying User Behavior

- Adjust task weights in the user class definitions
- Modify think time ranges in the base class
- Change user class distribution in `locust.conf`

### Data Customization

- Update product IDs, types, and search terms in the constants section
- Modify authentication credentials as needed
- Adjust random value ranges for realistic testing

## Troubleshooting

### Common Issues

- **Authentication failures**: Ensure Django server is running and credentials are correct
- **Connection timeouts**: Check `request_timeout` setting and network connectivity
- **High error rates**: Review API logs and adjust test parameters if needed

### Debug Mode

Enable debug logging:

```bash
locust -f locustfile.py --loglevel DEBUG
```

## Performance Tips

1. Start with a small number of users and gradually increase
2. Monitor server resources during testing
3. Use the web UI to adjust spawn rates in real-time
4. Analyze CSV reports for performance bottlenecks
5. Consider running tests during different times of day

## API Coverage

This load test covers all major endpoints from your `request.http` file:

- ✅ Authentication endpoints
- ✅ Product discovery and filtering
- ✅ Cart management operations
- ✅ Order processing workflows
- ✅ Admin pricing rule management
- ✅ Error scenario testing
- ✅ Health check endpoints
