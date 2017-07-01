using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using Solution.Base.Interfaces.Model;
using Solution.Base.Extensions;
using System.Linq.Expressions;
using Solution.Base.ModelMetadata;
using System.ComponentModel;
using System.Web.Mvc;

namespace Solution.Base.Implementation.Model
{
    public abstract class BaseEntity<T> : BaseObjectValidatable, IBaseEntity<T>
    {
        [ReadOnlyHiddenInput(ShowForCreate = true, ShowForEdit = true), Display(Order = 0)]
        public virtual T Id { get; set; }

        [ReadOnlyHiddenInput(ShowForCreate = true, ShowForEdit = true), Display(Order = 0)]
        object IBaseEntity.Id
        {
            get { return this.Id; }
            set { this.Id = (T)value;  }
        }

        //[ReadOnly(true), Display(Order = 0), HiddenInput()]
        //public virtual T Id { get; set; }

        //[ReadOnly(true), Display(Order = 0), HiddenInput()]
        //object IBaseEntity.Id
        //{
        //    get { return this.Id; }
        //    set { this.Id = (T)value; }
        //}

    }
}
