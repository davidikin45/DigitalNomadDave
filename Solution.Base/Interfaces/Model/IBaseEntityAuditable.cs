﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Interfaces.Model
{
    public interface IBaseEntityAuditable : IBaseEntity
    {
        DateTime DateCreated { get; set; }
        string UserCreated { get; set; }
        DateTime? DateModified { get; set; }
        string UserModified { get; set; }
    }
}
