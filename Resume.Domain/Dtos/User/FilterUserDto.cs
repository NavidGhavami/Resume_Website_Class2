﻿using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Dtos.User
{
    public class FilterUserDto
    {
        #region Properties

        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsBlock { get; set; }
        public string? Avatar { get; set; }
        public string CreateDate { get; set; }


        #endregion
    }
}