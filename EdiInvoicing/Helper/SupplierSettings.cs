using System;
using System.Configuration;

using EdiInvoicing.Models;

namespace EdiInvoicing.Helper
{
    /// <summary> A supplier settings.</summary>
    public class SupplierSettings
    {

        /// <summary> Context for the Bolt Entity Context.</summary>
        private BoltContextEntities boltContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierSettings"/> class.  Constructor.
        /// </summary>
        /// <param name="supplierId">
        /// Identifier for the supplier. 
        /// </param>
        public SupplierSettings(int supplierId) : this(supplierId, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierSettings"/> class. Initializes a new instance of the <see cref="SupplierSettings"/>
        ///   class.  Constructor.
        /// </summary>
        /// <param name="supplierId">
        /// Identifier for the supplier. 
        /// </param>
        /// <param name="boltContext">
        /// Context for the database. 
        /// </param>
        public SupplierSettings(int supplierId, BoltContextEntities boltContext)
        {
            this.boltContext = boltContext ?? new BoltContextEntities();

            this.GetSupplierImportSettings(supplierId);
        }

        /// <summary>
        /// Gets or sets the database settings.
        /// </summary>
        public Supplier_OrderImportSettings DatabaseSettings{ get; set; }

        /// <summary> Gets output file path.</summary>
        /// <returns> The output file path.</returns>
        public string GetOutputFilePath()
        {
            return string.Format(ConfigurationManager.AppSettings["ediPath"], this.DatabaseSettings.SourceName);
        }

        /// <summary> Gets supplier import settings.</summary>
        /// <param name="supplierId"> Identifier for the supplier. </param>
        private void GetSupplierImportSettings(int supplierId)
        {
                this.DatabaseSettings = this.boltContext.Supplier_OrderImportSettings.Find(supplierId);

            if (this.DatabaseSettings == null)
            {
                throw new NullReferenceException("No settings found for Supplier ID " + supplierId);
            }
        }
    }
}
