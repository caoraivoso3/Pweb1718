using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace SpacesForChildren.Models {
    public enum EApprovation {
        [Description("Acceite")]
        Accepted,
        [Description("Aguardando Aprovação")]
        Awaiting,
        [Description("Recusado")]
        Refused
    }
}