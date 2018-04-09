﻿using Project.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.UserProfileDomain.Models {

    public class Interest : ObjectWithState {

        public int Id { get; set; }

        public string Title { get; set; }
    }
}