using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DND.Core.Models;
using Solution.Base.Interfaces.Automapper;
using DND.Core.Enums;
using System;
using AutoMapper;
using Solution.Base.Implementation.Models;
using System.Collections.Generic;
using DND.Core.DTOs;

namespace DND.Core.ViewModels
{
	public class LocationForm : BaseObjectValidatable, IMapTo<LocationRequestDTO>
    {
        [Required]
        public string Locale { get; set; }
        [Required]
        public string[] Market { get; set; }
        [Required]
        public string Currency { get; set; }
    
        public string Id { get; set; }

        public override IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            yield break;
        }
    }
}