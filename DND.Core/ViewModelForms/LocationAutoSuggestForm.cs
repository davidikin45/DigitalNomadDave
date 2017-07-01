using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DND.Core.Model;
using Solution.Base.Interfaces.Automapper;
using DND.Core.Enums;
using System;
using AutoMapper;
using Solution.Base.Implementation.Model;
using System.Collections.Generic;
using DND.Core.DTOs;

namespace DND.Core.ViewModels
{
	public class LocationAutoSuggestForm : BaseObjectValidatable, IMapTo<LocationAutoSuggestRequestDTO>
    {
        [Required]
        public string Locale { get; set; }
        [Required]
        public string[] Market { get; set; }
        [Required]
        public string Currency { get; set; }
    
        public string Search { get; set; }

        public override IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            yield break;
        }
    }
}