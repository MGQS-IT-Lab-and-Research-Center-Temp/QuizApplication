﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuizApplication.Models.Role
{
    public class UpdateRoleViewModel
    {
        public string Id { get; set; }

        [ReadOnly(true)]
        public string ClassName { get; set; }

        [MinLength(5, ErrorMessage = "The minimum length acceptable is 5")]
        [MaxLength(200)]
        public string Description { get; set; }

    }
}
