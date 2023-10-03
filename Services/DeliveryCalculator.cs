namespace BookstoreAPI.Services;
    public class DeliveryCalculator
    {
        private const double EarthRadiusKm = 6371;
        private const double BaseFeePerKm = 1.0;

        public double CalculateDeliveryFee(double destinationLat, double destinationLon, string deliveryMethod)
        {
            double baseFee = 500.0;

            // Adjust fees based on delivery method
            if (deliveryMethod == "Pick Up Station")
            {
                baseFee += 200.0; // Set a fixed fee for PickUpStation delivery method
            }
            else if (deliveryMethod == "Door Delivery")
            {
                baseFee += 1000.0; // Set a fixed fee for DoorDelivery delivery method
            }
            else
            {
                throw new ArgumentException("Unsupported delivery method");
            }

            double deliveryDistance = CalculateDistance(destinationLat, destinationLon);
            double deliveryFee = baseFee + (deliveryDistance * BaseFeePerKm);

            return deliveryFee;
        }

        private static double CalculateDistance(double destinationLat, double destinationLon)
        {
            // Haversine formula to calculate distance using destination coordinates
            double dLat = DegreeToRadian(destinationLat);
            double dLon = DegreeToRadian(destinationLon);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreeToRadian(destinationLat)) * Math.Cos(DegreeToRadian(destinationLat)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = EarthRadiusKm * c;

            return distance;
        }

        private static double DegreeToRadian(double degree)
        {
            return degree * (Math.PI / 180);
        }
    }
