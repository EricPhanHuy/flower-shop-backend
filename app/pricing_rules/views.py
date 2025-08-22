from rest_framework import viewsets, permissions
from .models import TimePricingRule
from .serializers import TimePricingRuleSerializer


class TimePricingRuleViewSet(viewsets.ModelViewSet):
    """
    API endpoint that allows time pricing rules to be viewed or edited.
    """

    queryset = TimePricingRule.objects.all().order_by("start_time")
    serializer_class = TimePricingRuleSerializer
    permission_classes = [permissions.IsAuthenticated]

    def get_queryset(self):
        """
        Optionally restricts the returned pricing rules to a given user,
        by filtering against a `username` query parameter in the URL.
        """
        queryset = TimePricingRule.objects.all()
        # Add any custom filtering logic here if needed
        return queryset
