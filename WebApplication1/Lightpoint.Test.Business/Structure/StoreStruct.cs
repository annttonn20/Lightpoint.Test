﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lightpoint.Test.Business.Structure
{
    public struct StoreStruct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string WorkingHour { get; set; }
        public List<int> ProductId { get; set; }
    }
}