from rest_framework import serializers
from .models import TimePricingRule


class TimePricingRuleSerializer(serializers.ModelSerializer):
    class Meta:
        model = TimePricingRule
        fields = ["id", "start_time", "end_time", "time_discount"]
        read_only_fields = ["id"]
