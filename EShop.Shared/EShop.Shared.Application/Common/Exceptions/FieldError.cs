﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.Application.Common.Exceptions;

public class FieldError
{
    public string Field { get; set; }
    public string Message { get; set; }
}
