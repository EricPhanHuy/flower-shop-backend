FROM python:3.9-slim

WORKDIR /code

# Install system dependencies for PostgreSQL
RUN apt-get update && apt-get install -y libpq-dev gcc

# Install dependencies
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy project files
COPY . .

# Set environment variables
ENV PYTHONDONTWRITEBYTECODE=1
ENV PYTHONUNBUFFERED=1

# Expose the port the app runs on
EXPOSE 8000

WORKDIR /code/app

# Command to run the application - Updated path to manage.py
# CMD ["python", "manage.py", "runserver", "0.0.0.0:8000"]