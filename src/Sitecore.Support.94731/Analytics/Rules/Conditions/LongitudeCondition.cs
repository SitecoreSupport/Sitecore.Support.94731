namespace Sitecore.Support.Analytics.Rules.Conditions
{
    using Sitecore.Analytics;
    using Sitecore.Rules;
    using Sitecore.Rules.Conditions;

    public class LongitudeCondition<T> : OperatorCondition<T> where T : RuleContext
    {
        protected override bool Execute(T ruleContext)
        {
            if (Tracker.IsActive == false)
            {
                Tracker.StartTracking();
            }

            double longitude = this.Longitude;

            switch (base.GetOperator())
            {
                case ConditionOperator.Equal:
                    return (longitude == this.Value);

                case ConditionOperator.GreaterThanOrEqual:
                    return (longitude >= this.Value);

                case ConditionOperator.GreaterThan:
                    return (longitude > this.Value);

                case ConditionOperator.LessThanOrEqual:
                    return (longitude <= this.Value);

                case ConditionOperator.LessThan:
                    return (longitude < this.Value);

                case ConditionOperator.NotEqual:
                    return !(longitude == this.Value);
            }
            return false;
        }

        public double Longitude
        {
            get
            {
                if (Tracker.Current.Session.Interaction.HasGeoIpData && Tracker.Current.Session.Interaction.GeoData.Longitude.HasValue)
                {
                    return Tracker.Current.Session.Interaction.GeoData.Longitude.GetValueOrDefault();
                }
                return double.NaN;
            }
        }

        public double Value { get; set; }
    }
}