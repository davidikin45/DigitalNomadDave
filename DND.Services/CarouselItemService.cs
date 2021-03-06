﻿using DND.Core.Interfaces.Services;
using DND.Core.Models;
using Solution.Base.Implementation.Services;
using Solution.Base.Implementation.Validation;
using Solution.Base.Interfaces.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Solution.Base.Extensions;
using AutoMapper;
using DND.Services.Skyscanner.Model;
using DND.Core.DTOs;
using DND.Services.SearchEngines;
using System.Threading;
using DND.Core.Interfaces.Persistance;
using System.Linq;
using System.Linq.Expressions;
using Solution.Base.Helpers;

namespace DND.Services
{
    public class CarouselItemService : BaseEntityService<IApplicationDbContext, CarouselItem, CarouselItemDTO>, ICarouselItemService
    {
        public CarouselItemService(IBaseUnitOfWorkScopeFactory baseUnitOfWorkScopeFactory, IMapper mapper)
        : base(baseUnitOfWorkScopeFactory, mapper)
        {

        }
     
    }
}