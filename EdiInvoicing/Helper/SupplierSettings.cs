using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using EDI.Models;

namespace EDI.Helper
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
