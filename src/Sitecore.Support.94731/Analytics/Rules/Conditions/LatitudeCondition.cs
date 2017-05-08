namespace Sitecore.Support.Analytics.Rules.Conditions
{
    using Sitecore.Analytics;
    using Sitecore.Rules.Conditions;
    using Sitecore.Rules;

    public class LatitudeCondition<T> : OperatorCondition<T> where T : RuleContext
    {
        protected override bool Execute(T ruleContext)
        {
            if (Tracker.IsActive == false)
            {
                Tracker.StartTracking();
            }

            double latitude = this.Latitude;

            switch (base.GetOperator())
            {
                case ConditionOperator.Equal:
                    return (latitude == this.Value);

                case ConditionOperator.GreaterThanOrEqual:
                    return (latitude >= this.Value);

                case ConditionOperator.GreaterThan:
                    return (latitude > this.Value);

                case ConditionOperator.LessThanOrEqual:
                    return (latitude <= this.Value);

                case ConditionOperator.LessThan:
                    return (latitude < this.Value);

                case ConditionOperator.NotEqual:
                    return !(latitude == this.Value);
            }
            return false;
        }

        public double Latitude
        {
            get
            {
                if (Tracker.Current.Session.Interaction.HasGeoIpData && Tracker.Current.Session.Interaction.GeoData.Latitude.HasValue)
                {
                    return Tracker.Current.Session.Interaction.GeoData.Latitude.GetValueOrDefault();
                }
                return double.NaN;
            }
        }

        public double Value { get; set; }
    }
}