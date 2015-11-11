﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.ViewModels.Select
{
    public class UpdateSelectViewModel
    {
        public UpdateSelectViewModel()
        {
            Options = new List<SelectOptionViewModel>
                {
                    new SelectOptionViewModel()
                };
            Types = new List<SelectListItem>();
            Parents = new List<SelectListItem>();
            ParentOptions = new List<SelectListItem>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IList<SelectOptionViewModel> Options { get; set; }
        public IList<SelectListItem> Types { get; set; }
        public int ParentId { get; set; }
        public int ParentOptionId { get; set; }
        public IList<SelectListItem> Parents { get; set; }
        public IList<SelectListItem> ParentOptions { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}