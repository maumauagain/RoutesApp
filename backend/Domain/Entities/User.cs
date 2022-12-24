﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
