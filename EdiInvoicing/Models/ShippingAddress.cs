namespace EDI.Models
{
    /// <summary>
    /// The shipping address for Ship to or Ship From.
    /// </summary>
    public class ShippingAddress
    {
        /// <summary>
        /// Gets or sets the comany name.
        /// </summary>
        public string ComanyName { get; set; }

        /// <summary>
        /// Gets or sets the address line 1.
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line 2.
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        public string StateCode { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the shipper type code.
        /// </summary>
        public ShipperType ShipperTypeCode { get; set; }
    }
}