using System;
using System.Collections.Generic;

namespace EdiInvoicing.Models
{
    /// <summary>
    ///     The edi 810 order data.
    /// </summary>
    public class EdiOrderData810
    {
        /// <summary>
        /// Gets or sets the full order number.
        /// </summary>
        public string FullOrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the source code.
        /// </summary>
        public string SourceCode { get; set; }

        /// <summary>
        /// Gets or sets the Ecometry order entry date.
        /// </summary>
        public DateTime EcometryEntryDate { get; set; }

        /// <summary>
        /// Gets or sets the po number.
        /// </summary>
        public string PoNumber { get; set; }

        /// <summary>
        /// Gets or sets the ship to customer last name.
        /// </summary>
        public string ShipToCustomerLastName { get; set; }

        /// <summary>
        /// Gets or sets the ship to customer middle initial.
        /// </summary>
        public string ShipToCustomerMiddleInitial { get; set; }

        /// <summary>
        /// Gets or sets the ship to customer first name.
        /// </summary>
        public string ShipToCustomerFirstName { get; set; }

        /// <summary>
        /// Gets or sets the ship to customer title.
        /// </summary>
        public string ShipToCustomerTitle { get; set; }

        /// <summary>
        /// Gets or sets the ship to company name.
        /// </summary>
        public string ShipToCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the ship to street.
        /// </summary>
        public string ShipToStreet { get; set; }

        /// <summary>
        /// Gets or sets the ship to city.
        /// </summary>
        public string ShipToCity { get; set; }

        /// <summary>
        /// Gets or sets the ship to state.
        /// </summary>
        public string ShipToState { get; set; }

        /// <summary>
        /// Gets or sets the ship to zipcode.
        /// </summary>
        public string ShipToZipcode { get; set; }

        /// <summary>
        /// Gets or sets the sears po number.
        /// </summary>
        public string SearsPoNumber { get; set; }

        /// <summary> Gets or sets the order lines.</summary>
        /// <value> The order lines.</value>
        public List<EdiOrderLine810> OrderLines { get; set; }
    }
}