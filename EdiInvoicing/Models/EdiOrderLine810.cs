using System;

namespace EdiInvoicing.Models
{
    /// <summary>
    /// The edi 810 order line data.
    /// </summary>
    public class EdiOrderLine810
    {
        /// <summary>
        /// Gets or sets the product number.
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        /// Gets or sets the carrier id.
        /// </summary>
        public string CarrierId { get; set; }

        /// <summary>
        /// Gets or sets the ship date.
        /// </summary>
        public DateTime ShipDate { get; set; }

        /// <summary>
        /// Gets or sets the arrive date.
        /// </summary>
        public DateTime ArriveDate { get; set; }

        /// <summary>
        /// Gets or sets the tracking number.
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the dw sku.
        /// </summary>
        public string DwSku { get; set; }

        /// <summary>
        /// Gets or sets the exact price amt.
        /// </summary>
        public string ExactPriceAmt { get; set; }

        /// <summary>
        /// Gets or sets the total before discount amount.
        /// </summary>
        public string TotalBeforeDiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the order item type legacy code.
        /// </summary>
        public string OrderItemTypeLegacyCode { get; set; }

        /// <summary>
        /// Gets or sets the sales order ship number.
        /// </summary>
        public string SalesOrderShipNumber { get; set; }

        /// <summary>
        /// Gets or sets the sales order ship to number.
        /// </summary>
        public string SalesOrderShipToNumber { get; set; }

        /// <summary>
        /// Gets or sets the ship confirmation date.
        /// </summary>
        public DateTime ShipConfirmationDate { get; set; }

        /// <summary>
        /// Gets or sets the associated line number.
        /// </summary>
        public int AssociatedLineNumber { get; set; }

        /// <summary>
        /// Gets or sets the ship time creation date.
        /// </summary>
        public DateTime ShipTimeCreationDate { get; set; }

        public string ProductDescription { get; set; }
    }
}