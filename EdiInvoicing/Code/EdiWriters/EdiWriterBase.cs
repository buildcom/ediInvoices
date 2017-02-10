using System.ComponentModel;
using System.Data.Entity;

using EdiInvoicing.Helper;
using EdiInvoicing.Models;

namespace EDI.Code.EdiWriters
{
    /// <summary> An edi writer base.</summary>
    public abstract class EdiWriterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdiWriterBase"/> class.  Constructor.
        /// </summary>
        /// <param name="markShipConfirm">
        /// true to mark ship confirm.
        /// </param>
        /// <param name="supplierSettings">
        /// The supplier settings.
        /// </param>
        public EdiWriterBase(bool markShipConfirm, SupplierSettings supplierSettings)
            : this(markShipConfirm, supplierSettings, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdiWriterBase"/> class. Initializes a new instance of the
        ///     <see cref="EdiWriterBase"/>
        ///     class.  Constructor.
        /// </summary>
        /// <param name="markShipConfirm">
        /// true to mark ship confirm.
        /// </param>
        /// <param name="supplierSettings">
        /// The supplier settings.
        /// </param>
        /// <param name="ecomedateDbContext">
        /// Context for the ecomedate database.
        /// </param>
        public EdiWriterBase(
            bool markShipConfirm,
            SupplierSettings supplierSettings,
            DbContext ecomedateDbContext)
        {
            this.MarkShipConfirm = markShipConfirm;
            this.Settings = supplierSettings;
            this.EcomedateDbContext = ecomedateDbContext ?? new EcomedateContextEntities();
        }

        /// <summary> Gets or sets a context for the ecomedate database.</summary>
        /// <value> The ecomedate database context.</value>
        internal DbContext EcomedateDbContext { get; set; }

        /// <summary> Gets or sets a value indicating whether the mark ship confirm.</summary>
        /// <value> true if mark ship confirm, false if not.</value>
        internal bool MarkShipConfirm { get; set; }

        /// <summary> Gets or sets options for controlling the operation.</summary>
        /// <value> The settings.</value>
        internal SupplierSettings Settings { get; set; }

        /// <summary> Process the edi.</summary>
        /// <returns> true if it succeeds, false if it fails.</returns>
        public abstract bool ProcessEdi();
    }
}